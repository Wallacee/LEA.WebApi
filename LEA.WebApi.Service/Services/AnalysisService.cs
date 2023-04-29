using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Service.Interfaces;
using LEA.WebApi.Service.ViewModel;
using System.Collections.Generic;
using System;
using System.Linq;
using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Service.Services
{
    public class AnalysisService : IAnalysisService
    {
        #region database_access_region

        private readonly IAnalysisRepository analysisRepository;

        public IAnalysisRepository AnalysisRepository => analysisRepository;

        #endregion

        public AnalysisService(IAnalysisRepository analysisRepository)
        {
            this.analysisRepository = analysisRepository;
        }

        public GeneralDataViewModel GeneralMatchData(int homeTeamId, int awayTeamId, int matchCount)
        {
            List<Team> home = AnalysisRepository.GetTeamAgainstHome(homeTeamId, matchCount);
            List<Team> away = AnalysisRepository.GetTeamAgainstAway(awayTeamId, matchCount);

            List<int> againstHomeId = new();
            List<string> againstHomeName = new();
            List<int> againstAwayId = new();
            List<string> againstAwayName = new();
            List<int> positiontHome = new();
            List<int> positionAway = new();
            List<int> positionAgainstHome = new();
            List<int> positionAgainstAway = new();

            List<DateTime> scheduleHome = AnalysisRepository.GetScheduleHome(homeTeamId, matchCount);
            List<DateTime> scheduleAway = AnalysisRepository.GetScheduleHome(awayTeamId, matchCount);

            List<Match> matchList = AnalysisRepository.GetAllMatchesByLeague(home[0].LeagueId);

            for (int i = 0; i < home.Count; i++)
            {
                againstHomeId.Add(home[i].Id);
                againstHomeName.Add(home[i].Name);
                againstAwayId.Add(away[i].Id);
                againstAwayName.Add(away[i].Name);

                List<TableLeagueViewModel> tableLeagueViewModels = this.MakeLeagueTable(scheduleHome[i], matchList);
                positiontHome.Add(tableLeagueViewModels.FindIndex(t => t.IdTeam == homeTeamId) + 1);
                positionAgainstHome.Add(tableLeagueViewModels.FindIndex(t => t.IdTeam == againstHomeId[i]) + 1);

                tableLeagueViewModels = this.MakeLeagueTable(scheduleAway[i], matchList);
                positionAway.Add(tableLeagueViewModels.FindIndex(t => t.IdTeam == awayTeamId) + 1);
                positionAgainstAway.Add(tableLeagueViewModels.FindIndex(t => t.IdTeam == againstAwayId[i]) + 1);

            }

            return new GeneralDataViewModel()
            {
                NameTeamAgainstHome = againstHomeName,
                NameTeamAgainstAway = againstAwayName,
                ScheduleHome = scheduleHome.ConvertAll(x => x.ToString("dd/MM/yy ddd")),
                ScheduleAway = scheduleAway.ConvertAll(x => x.ToString("dd/MM/yy ddd")),
                PositionHome = positiontHome,
                PositionAgainstHome = positionAgainstHome,
                PositionAway = positionAway,
                PositionAgainstAway = positionAgainstAway
            };
        }

        public LeaguesViewModel Leagues()
        {

            return new LeaguesViewModel()
            {
                Leagues = AnalysisRepository.GetAllLeagues()
                                            .Select(l => new LeagueViewModel()
                                            {
                                                Id = l.Id,
                                                Name = l.Name,
                                                Division = l.Division,
                                                ShieldUrl = l.Shield
                                            })
                                            .ToList()
            };
        }

        public TeamsViewModel Teams(int idLeague)
        {
            return new TeamsViewModel()
            {
                Teams = AnalysisRepository.GetAllLeagueTeams(idLeague)
                                          .Select(l => new TeamViewModel()
                                          {
                                              Id = l.Id,
                                              LeagueId = l.LeagueId,
                                              Name = l.Name,
                                              ShieldUrl = l.Shield
                                          })
                                          .OrderBy(team => team.Name)
                                          .ToList()
            };
        }

        public AnalysisViewModel MatchGoalsFullTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return new AnalysisViewModel()
            {
                MadeByHome = AnalysisRepository.GetMadeGoalsFullTimeHome(homeTeamId, matchCount),
                TakenByHome = analysisRepository.GetTakenGoalsFullTimeHome(homeTeamId, matchCount),
                MadeByAway = AnalysisRepository.GetMadeGoalsFullTimeAway(awayTeamId, matchCount),
                TakenByAway = analysisRepository.GetTakenGoalsFullTimeAway(awayTeamId, matchCount),
                StatisticKind = Domain.Enuns.StatisticKind.GoalsFullTime
            };
        }

        public AnalysisViewModel MatchGoalsHalfTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return new AnalysisViewModel()
            {
                MadeByHome = AnalysisRepository.GetMadeGoalsHalfTimeHome(homeTeamId, matchCount),
                TakenByHome = analysisRepository.GetTakenGoalsHalfTimeHome(homeTeamId, matchCount),
                MadeByAway = AnalysisRepository.GetMadeGoalsHalfTimeAway(awayTeamId, matchCount),
                TakenByAway = analysisRepository.GetTakenGoalsHalfTimeAway(awayTeamId, matchCount),
                StatisticKind = Domain.Enuns.StatisticKind.GoalsHalfTime
            };
        }

        public AnalysisViewModel MatchCornersFullTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return new AnalysisViewModel()
            {
                MadeByHome = AnalysisRepository.GetMadeCornersFullTimeHome(homeTeamId, matchCount),
                TakenByHome = analysisRepository.GetTakenCornersFullTimeHome(homeTeamId, matchCount),
                MadeByAway = AnalysisRepository.GetMadeCornersFullTimeAway(awayTeamId, matchCount),
                TakenByAway = analysisRepository.GetTakenCornersFullTimeAway(awayTeamId, matchCount),
                StatisticKind = Domain.Enuns.StatisticKind.Corners
            };
        }

        public short GetPossibleMatchAmount(int homeTeamId, int awayTeamId)
        {
            short homeAmount = AnalysisRepository.GetHomeAmountTeamMatch(homeTeamId);
            short awayAmount = AnalysisRepository.GetAwayAmountTeamMatch(awayTeamId);
            if (homeAmount == awayAmount)
                return homeAmount;

            return homeAmount > awayAmount ? awayAmount : homeAmount;
        }


        public List<TableLeagueViewModel> MakeLeagueTable(DateTime schedule, List<Match> seasonMatches)
        {
            List<Match> validMatches = seasonMatches.Where(s => s.Schedule <= schedule).ToList();
            List<TableLeagueViewModel> tableLeagueViewModel = new();
            foreach (var match in validMatches)
            {
                if (!tableLeagueViewModel.Any(t => t.IdTeam == match.HomeTeamId))
                {
                    tableLeagueViewModel.Add(new TableLeagueViewModel()
                    {
                        IdTeam = match.HomeTeamId,
                        Points = GetPointsValue(match.HomeStatistics),
                        GoalsPro = match.HomeStatistics.GoalsFullTime,
                        GoalDifference = match.HomeStatistics.GoalsFullTime - match.AwayStatistics.GoalsFullTime
                    });
                }
                else
                {
                    TableLeagueViewModel teamTable = tableLeagueViewModel.Where(t => t.IdTeam == match.HomeTeamId).FirstOrDefault();

                    teamTable.Points += GetPointsValue(match.HomeStatistics);
                    teamTable.GoalsPro += match.HomeStatistics.GoalsFullTime;
                    teamTable.GoalDifference += match.HomeStatistics.GoalsFullTime - match.AwayStatistics.GoalsFullTime;
                }

                if (!tableLeagueViewModel.Any(t => t.IdTeam == match.AwayTeamId))
                {
                    tableLeagueViewModel.Add(new TableLeagueViewModel()
                    {
                        IdTeam = match.AwayTeamId,
                        Points = GetPointsValue(match.AwayStatistics),
                        GoalsPro = match.AwayStatistics.GoalsFullTime,
                        GoalDifference = match.AwayStatistics.GoalsFullTime - match.HomeStatistics.GoalsFullTime
                    });
                }
                else
                {
                    TableLeagueViewModel teamTable = tableLeagueViewModel.Where(t => t.IdTeam == match.AwayTeamId).FirstOrDefault();

                    teamTable.Points += GetPointsValue(match.AwayStatistics);
                    teamTable.GoalsPro += match.AwayStatistics.GoalsFullTime;
                    teamTable.GoalDifference += match.AwayStatistics.GoalsFullTime - match.HomeStatistics.GoalsFullTime;
                }

            }

            return tableLeagueViewModel.OrderByDescending(t => t.Points).ThenByDescending(t => t.GoalDifference).ThenByDescending(t => t.GoalsPro).ToList();

        }

        private int GetPointsValue(MatchStatistics matchStatistics)
        {
            if (matchStatistics.ResultFullTime == Domain.Enuns.Scoreboard.Draw)
                return 1;

            return matchStatistics.ResultFullTime == Domain.Enuns.Scoreboard.Win ? 3 : 0;
        }

        //public List<Match> GetAllMatchesBySchedule(DateTime dateTime, int idLegue)
        //{

        //}

    }
}
