using LEA.WebApi.Domain.Enuns;
using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;
using LEA.WebApi.Service.Interfaces;
using LEA.WebApi.Service.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LEA.WebApi.Service.Services
{
    public class UploadService : IUploadService
    {
        #region database access region
        private readonly IConfiguration configuration;
        private readonly ILeagueRepository leagueRepository;
        private readonly IMatchRepository matchRepository;
        private readonly ITeamRepository teamRepository;
        private readonly IRefereeRepository refereeRepository;
        private readonly IMatchStatisticsRepository matchStatisticsRepository;
        public ILeagueRepository LeagueRepository => leagueRepository;

        public IMatchRepository MatchRepository => matchRepository;

        public ITeamRepository TeamRepository => teamRepository;

        public IRefereeRepository RefereeRepository => refereeRepository;

        public IConfiguration Configuration => configuration;

        public IMatchStatisticsRepository MatchStatisticsRepository => matchStatisticsRepository;
        #endregion
        public UploadService(ILeagueRepository leagueRepository,
            IMatchRepository matchRepository,
            ITeamRepository teamRepository,
            IRefereeRepository refereeRepository,
            IConfiguration configuration, IMatchStatisticsRepository matchStatisticsRepository)
        {
            this.leagueRepository = leagueRepository;
            this.matchRepository = matchRepository;
            this.teamRepository = teamRepository;
            this.refereeRepository = refereeRepository;
            this.configuration = configuration;
            this.matchStatisticsRepository = matchStatisticsRepository;
        }
        public List<UpdateMatchesReportViewModel> UpdateDatabaseByCSVFile(string fileName)
        {
            string folderName = Configuration.GetSection("ApiConstant").GetSection("UploadFolderFile").Value;
            string filePath = Path.Combine(folderName, fileName);
            using FileStream fileStream = new(filePath, FileMode.Open);
            using StreamReader streamReader = new(fileStream, Encoding.GetEncoding(0));
            string line = streamReader.ReadLine();
            List<UpdateMatchesReportViewModel> updateMatchesReportViewModel = new();
            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                string[] fields = line.Split(",");
                string[] dateShedule = fields[1].Split("/");
                string[] timeShedule = fields[2].Split(":");
                DateTime schedule = MatchScheduleLoad(dateShedule, timeShedule);
                League league = LeagueLoad(fields);
                Team homeTeam = HomeTeamLoad(fields, league);
                Team awayTeam = AwayTeamLoad(fields, league);
                bool saved = false;
                if (MatchRepository.FindByScheduleDateHomeAway(schedule, homeTeam.Name, awayTeam.Name) == null)
                {
                    Referee referee = null;

                    if (league.Coutry == Country.England)
                        referee = RefereeLoad(fields);
                    MatchStatistics homeMatchStatistics = HomeMatchStatisticsLoad(fields, league.Coutry);
                    MatchStatistics awayMatchStatistics = AwayMatchStatisticsLoad(fields, league.Coutry);
                    if (league.Coutry == Country.England)
                        MatchLoad(schedule, referee, homeTeam, awayTeam, homeMatchStatistics, awayMatchStatistics);
                    else
                        MatchLoad(schedule, null, homeTeam, awayTeam, homeMatchStatistics, awayMatchStatistics);
                    saved = true;
                }
                updateMatchesReportViewModel.Add(
                    new UpdateMatchesReportViewModel()
                    {
                        FileName = fileName,
                        League = league.Name,
                        Schedule = schedule.ToString(),
                        Home = homeTeam.Name,
                        Away = awayTeam.Name,
                        Creation = DateTime.Now.ToString(),
                        Saved = saved
                    });
            }
            
            return updateMatchesReportViewModel;
        }
        #region method record
        private void MatchLoad(DateTime schedule, Referee referee, Team homeTeam, Team awayTeam, MatchStatistics homeMatchStatistics, MatchStatistics awayMatchStatistics)
        {
            Match match = new()
            {
                Schedule = schedule,
                HomeTeamId = homeTeam.Id,
                AwayTeamId = awayTeam.Id,
                HomeStatisticsId = homeMatchStatistics.Id,
                AwayStatisticsId = awayMatchStatistics.Id,
                RefereeId = referee?.Id
            };
            MatchRepository.Save(match);
        }
        private MatchStatistics AwayMatchStatisticsLoad(string[] fields, Country country)
        {
            int setupCellUpload = country == Country.England ? 0 : 1;
            MatchStatistics awayMatchStatistics = new()
            {
                GoalsHalfTime = ParseShort(fields[9]),
                GoalsFullTime = ParseShort(fields[6]),
                ResultHalfTime = FindResult(goals: fields[9], rivalGoals: fields[8]),
                ResultFullTime = FindResult(goals: fields[6], rivalGoals: fields[5]),
                Shots = ParseShort(fields[13 - setupCellUpload]),
                ShotsOnTarget = ParseShort(fields[15 - setupCellUpload]),
                Corners = ParseShort(fields[19 - setupCellUpload]),
                FoulsCommitted = ParseShort(fields[17 - setupCellUpload]),
                Yellow = ParseShort(fields[21 - setupCellUpload]),
                Red = ParseShort(fields[23 - setupCellUpload])

            };
            MatchStatisticsRepository.Create(awayMatchStatistics);
            return awayMatchStatistics;
        }
        private MatchStatistics HomeMatchStatisticsLoad(string[] fields, Country country)
        {
            int setupCellUpload = country == Country.England ? 0 : 1;
            MatchStatistics homeMatchStatistics = new()
            {
                GoalsHalfTime = ParseShort(fields[8]),
                GoalsFullTime = ParseShort(fields[5]),
                ResultHalfTime = FindResult(goals: fields[8], rivalGoals: fields[9]),
                ResultFullTime = FindResult(goals: fields[5], rivalGoals: fields[6]),
                Shots = ParseShort(fields[12 - setupCellUpload]),
                ShotsOnTarget = ParseShort(fields[14 - setupCellUpload]),
                Corners = ParseShort(fields[18 - setupCellUpload]),
                FoulsCommitted = ParseShort(fields[16 - setupCellUpload]),
                Yellow = ParseShort(fields[20 - setupCellUpload]),
                Red = ParseShort(fields[22 - setupCellUpload])
            };
            MatchStatisticsRepository.Create(homeMatchStatistics);
            return homeMatchStatistics;
        }
        private Team AwayTeamLoad(string[] fields, League league)
        {
            Team awayTeam = TeamRepository.FindByName(fields[4]);
            if (awayTeam == null)
            {
                awayTeam = new()
                {
                    Name = fields[4],
                    LeagueId = league.Id
                };
                TeamRepository.Save(awayTeam);
            }

            return awayTeam;
        }
        private Team HomeTeamLoad(string[] fields, League league)
        {
            Team homeTeam = TeamRepository.FindByName(fields[3]);
            if (homeTeam == null)
            {
                homeTeam = new()
                {
                    Name = fields[3],
                    LeagueId = league.Id
                };

                TeamRepository.Save(homeTeam);
            }

            return homeTeam;
        }
        private League LeagueLoad(string[] fields)
        {
            League league = LeagueRepository.FindByName(FindLeagueName(fields[0]));
            if (league == null)
            {
                league = new()
                {
                    Division = FindDivision(fields[0]),
                    Coutry = FindCountry(fields[0]),
                    Name = FindLeagueName(fields[0])
                };
                LeagueRepository.Save(league);
            }

            return league;
        }
        private Referee RefereeLoad(string[] fields)
        {
            Referee referee = RefereeRepository.FindByName(fields[11]);
            if (referee == null)
            {
                referee = new()
                {
                    Name = fields[11]
                };

                RefereeRepository.Save(referee);
            }

            return referee;
        }
        #endregion
        #region aux build method region
        private DateTime MatchScheduleLoad(string[] dateShedule, string[] timeShedule)
        {
            return new(
                                ParseInt(dateShedule[2]),
                                ParseInt(dateShedule[1]),
                                ParseInt(dateShedule[0]),
                                ParseInt(timeShedule[0]),
                                ParseInt(timeShedule[1]),
                                0);
        }
        private string FindLeagueName(string v)
        {
            return v switch
            {
                "E0" => "Premier League",
                "SP1" => "La Liga",
                "D1" => "Bundesliga",
                "I1" => "Serie A",
                "F1" => "Ligue 1",
                "N1" => "Eredivise",
                "P1" => "Liga 1",
                _ => "Undefined",
            };
        }

        private int ParseInt(string value)
        {
            return int.Parse(value);
        }

        private Country FindCountry(string value)
        {
            return value switch
            {
                "E0" => Country.England,
                "SP1" => Country.Spain,
                "D1" => Country.Germany,
                "I1" => Country.Italy,
                "F1" => Country.France,
                "N1" => Country.Netherlands,
                "P1" => Country.Portugal,
                _ => Country.Undefined,
            };
        }

        private Scoreboard FindResult(string goals, string rivalGoals)
        {
            if (goals.Equals(rivalGoals))
                return Scoreboard.Draw;
            return ParseShort(goals) > ParseShort(rivalGoals) ? Scoreboard.Win : Scoreboard.Lost;
        }

        private short ParseShort(string value)
        {
            return short.Parse(value);
        }

        private short FindDivision(string division)
        {
            return division switch
            {
                "E0" => 1,
                "SP1" => 1,
                "D1" => 1,
                "I1" => 1,
                "F1" => 1,
                "N1" => 1,
                "P1" => 1,
                _ => -1,
            };
        }
        #endregion
    }
}

