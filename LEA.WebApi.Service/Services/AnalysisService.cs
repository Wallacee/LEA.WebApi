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



        public AnalysisViewModel MatchGoalsFullTime(int homeTeamId, int awayTeamId, int statisticId, int matchCount)
        {
            AnalysisViewModel analysisViewModel = new()
            {
                MadeByHome = AnalysisRepository.GetMadeGoalsFullTimeHome(homeTeamId, matchCount),
                TakenByHome = analysisRepository.GetTakenGoalsFullTimeHome(homeTeamId, matchCount),
                MadeByAway = AnalysisRepository.GetMadeGoalsFullTimeAway(awayTeamId, matchCount),
                TakenByAway = analysisRepository.GetTakenGoalsFullTimeAway(awayTeamId, matchCount)
            };
            return analysisViewModel;
        }
    }
}
