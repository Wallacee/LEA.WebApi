using LEA.WebApi.Domain.Enuns;
using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;
using LEA.WebApi.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
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
        public string UpdateDatabaseByCSVFile(string fileName)
        {
            string folderName = Configuration.GetSection("ApiConstant").GetSection("UploadFolderFile").Value;
            string filePath = Path.Combine(folderName, fileName);
            using FileStream fileStream = new(filePath, FileMode.Open);
            using StreamReader streamReader = new(fileStream, Encoding.GetEncoding(0));
            string line = streamReader.ReadLine();
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
                if (MatchRepository.FindByScheduleDateHomeAway(schedule, homeTeam.Name, awayTeam.Name) == null)
                {
                    Referee referee = RefereeLoad(fields);
                    MatchStatistics homeMatchStatistics = HomeMatchStatisticsLoad(fields);
                    MatchStatistics awayMatchStatistics = AwayMatchStatisticsLoad(fields);
                    MatchLoad(schedule, referee, homeTeam, awayTeam, homeMatchStatistics, awayMatchStatistics);
                }
            }
            return "show";
        }

        
        #region method reord
        private void MatchLoad(DateTime schedule, Referee referee, Team homeTeam, Team awayTeam, MatchStatistics homeMatchStatistics, MatchStatistics awayMatchStatistics)
        {
            Match match = new()
            {
                Schedule = schedule,
                HomeTeamId = homeTeam.Id,
                AwayTeamId = awayTeam.Id,
                HomeStatisticsId = homeMatchStatistics.Id,
                AwayStatisticsId = awayMatchStatistics.Id,
                RefereeId = referee.Id
            };
            MatchRepository.Save(match);
        }

        private MatchStatistics AwayMatchStatisticsLoad(string[] fields)
        {
            MatchStatistics awayMatchStatistics = new()
            {
                GoalsHalfTime = ParseShort(fields[9]),
                GoalsFullTime = ParseShort(fields[6]),
                ResultHalfTime = FindResult(goals: fields[9], rivalGoals: fields[8]),
                ResultFullTime = FindResult(goals: fields[6], rivalGoals: fields[5]),
                Shots = ParseShort(fields[13]),
                ShotsOnTarget = ParseShort(fields[15]),
                Corners = ParseShort(fields[19]),
                FoulsCommitted = ParseShort(fields[17]),
                Yellow = ParseShort(fields[21]),
                Red = ParseShort(fields[23])

            };
            MatchStatisticsRepository.Create(awayMatchStatistics);
            return awayMatchStatistics;
        }

        private MatchStatistics HomeMatchStatisticsLoad(string[] fields)
        {
            MatchStatistics homeMatchStatistics = new()
            {
                GoalsHalfTime = ParseShort(fields[8]),
                GoalsFullTime = ParseShort(fields[5]),
                ResultHalfTime = FindResult(goals: fields[8], rivalGoals: fields[9]),
                ResultFullTime = FindResult(goals: fields[5], rivalGoals: fields[6]),
                Shots = ParseShort(fields[12]),
                ShotsOnTarget = ParseShort(fields[14]),
                Corners = ParseShort(fields[18]),
                FoulsCommitted = ParseShort(fields[16]),
                Yellow = ParseShort(fields[20]),
                Red = ParseShort(fields[22])
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
                _ => -1,
            };
        }
        #endregion
    }
}

