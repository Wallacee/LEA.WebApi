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


        [HttpGet, DisableRequestSizeLimit]
        [Route("GeneralMatchData")]
        public IActionResult GeneralMatchData(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.GeneralMatchData(homeTeamId, awayTeamId, matchCount));
        }

        [HttpGet]
        [Route("Leagues")]
        public IActionResult Leagues()
        {
            return StatusCode(200, AnalysisService.Leagues());
        }

        [HttpGet]
        [Route("Teams")]
        public IActionResult Teams(int idLeague)
        {
            return StatusCode(200, AnalysisService.Teams(idLeague));
        }


        [HttpGet]
        [Route("PossibleMatchAmount")]
        public IActionResult PossibleMatchAmount(int homeTeamId, int awayTeamId)
        {
            return StatusCode(200, AnalysisService.GetPossibleMatchAmount(homeTeamId, awayTeamId));
        }

        [HttpGet]
        [Route("MatchGoalsHalfTime")]
        public IActionResult MatchGoalsFullTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.MatchGoalsHalfTime(homeTeamId, awayTeamId, matchCount));
        }

        [HttpGet]
        [Route("MatchGoalsFullTime")]
        public IActionResult MatchGoalsHalfTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.MatchGoalsFullTime(homeTeamId, awayTeamId, matchCount));
        }

        [HttpGet]
        [Route("MatchCornersFullTime")]
        public IActionResult MatchCornersFullTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.MatchCornersFullTime(homeTeamId, awayTeamId, matchCount));
        }

        [HttpGet]
        [Route("MatchYellowFullTime")]
        public IActionResult MatchYellowFullTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.MatchYellowFullTime (homeTeamId, awayTeamId, matchCount));
        }

        [HttpGet]
        [Route("MatchRedFullTime")]
        public IActionResult MatchRedFullTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.MatchRedFullTime(homeTeamId, awayTeamId, matchCount));
        }

        [HttpGet]
        [Route("MatchShostsFullTime")]
        public IActionResult MatchShotsFullTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.MatchShotsFullTime(homeTeamId, awayTeamId, matchCount));
        }

        [HttpGet]
        [Route("MatchShotsOnTargetFullTime")]
        public IActionResult MatchShotsOnTargetFullTime(int homeTeamId, int awayTeamId, int matchCount)
        {
            return StatusCode(200, AnalysisService.MatchShotsOnTargetFullTime(homeTeamId, awayTeamId, matchCount));
        }
    }
}
