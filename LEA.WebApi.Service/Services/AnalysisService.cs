using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Service.Interfaces;
using LEA.WebApi.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
