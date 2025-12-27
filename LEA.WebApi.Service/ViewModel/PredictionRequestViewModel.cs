using System;

namespace LEA.WebApi.Service.ViewModel
{
    public class PredictionRequestViewModel
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }

        public int LeagueId { get; set; }
        public DateTime MatchDate { get; set; }
    }
}
