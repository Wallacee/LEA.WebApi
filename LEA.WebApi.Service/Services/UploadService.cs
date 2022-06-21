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
            string folderName = configuration.GetSection("ApiConstant").GetSection("UploadFolderFile").Value;
            string filePath = Path.Combine(folderName, fileName);
            using FileStream fileStream = new(filePath, FileMode.Open);
            using StreamReader streamReader = new(fileStream, Encoding.GetEncoding(0));
            string line = streamReader.ReadLine();
            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                string[] fields = line.Split(",");

                Referee referee = refereeRepository.FindByName(fields[11]);
                if (referee == null)
                {
                    referee = new()
                    {
                        Name = fields[11]
                    };

                    refereeRepository.Save(referee);
                }

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

                Team homeTeam = teamRepository.FindByName(fields[3]);
                if (homeTeam == null)
                {
                    homeTeam = new()
                    {
                        Name = fields[3],
                        LeagueId = league.Id
                    };

                    teamRepository.Save(homeTeam);
                }

                Team awayTeam = teamRepository.FindByName(fields[4]);
                if (awayTeam == null)
                {
                    awayTeam = new()
                    {
                        Name = fields[4],
                        LeagueId = league.Id
                    };
                    teamRepository.Save(awayTeam);
                }

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
                matchStatisticsRepository.Create(homeMatchStatistics);
                
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
                matchStatisticsRepository.Create(awayMatchStatistics);

                string[] dateShedule = fields[1].Split("/");
                string[] timeShedule = fields[2].Split(":");
                Match match = new()
                {
                    Schedule = new DateTime(
                    ParseInt(dateShedule[2]),
                    ParseInt(dateShedule[1]),
                    ParseInt(dateShedule[0]),
                    ParseInt(timeShedule[0]),
                    ParseInt(timeShedule[1]),
                    0),
                    HomeTeamId = homeTeam.Id,
                    AwayTeamId = awayTeam.Id,
                    HomeStatisticsId = homeMatchStatistics.Id,
                    AwayStatisticsId = awayMatchStatistics.Id,
                    RefereeId = referee.Id
                };
                matchRepository.Save(match);
            }
            return "show";
        }

        private string FindLeagueName(string v)
        {
            switch (v)
            {
                case "E0":
                    return "Premier League";
                default:
                    return "Undefined";
            }
        }

        private int ParseInt(string value)
        {
            return int.Parse(value);
        }

        private Country FindCountry(string value)
        {
            switch (value)
            {
                case "E0":
                    return Country.England;
                default:
                    return Country.Undefined;
            }
        }

        private short ParseShort(string value)
        {
            return short.Parse(value);
        }

        private Scoreboard FindResult(string goals, string rivalGoals)
        {
            if (goals.Equals(rivalGoals))
                return Scoreboard.Draw;
            return short.Parse(goals) > short.Parse(rivalGoals) ? Scoreboard.Win : Scoreboard.Lost;
        }

        private short FindDivision(string division)
        {
            switch (division)
            {
                case "E0":
                    return 1;
                default:
                    return -1;
            }
        }
    }
}

