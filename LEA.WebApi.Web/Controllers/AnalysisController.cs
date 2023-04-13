using LEA.WebApi.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LEA.WebApi.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnalysisController : Controller
    {
        #region data_access_region
        private readonly IAnalysisService analysisService;
        public IAnalysisService AnalysisService => analysisService;

        #endregion
        public AnalysisController(IAnalysisService analysisService)
        {
            this.analysisService = analysisService;
        }


        [HttpPost, DisableRequestSizeLimit]
        [Route("GeneralMatchData")]
        public IActionResult GeneralMatchData(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.GeneralMatchData(homeTeamId, awayTeamId, matchCount));
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("MatchGoalsFullTime")]
        public IActionResult MatchGoalsFullTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.MatchGoalsFullTime(homeTeamId, awayTeamId, matchCount));
        }



    }
}
