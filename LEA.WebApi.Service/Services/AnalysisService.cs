using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Service.Interfaces;
using LEA.WebApi.Service.ViewModel;
using System.Linq;

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
            return new GeneralDataViewModel()
            {
                NameTeamAgainstHome = AnalysisRepository.GetTeamNameAgainstHome(homeTeamId, matchCount),
                NameTeamAgainstAway = AnalysisRepository.GetTeamNameAgainstAway(awayTeamId, matchCount),
                ScheduleHome = AnalysisRepository.GetScheduleHome(homeTeamId, matchCount),
                ScheduleAwyay = analysisRepository.GetScheduleAway(awayTeamId, matchCount),

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
    }
}
