using LEA.WebApi.Domain.Models;
using System.Collections.Generic;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface IAnalysisRepository
    {
        List<string> GetTeamNameAgainstHome(int homeTeamId, int amountGame);
        List<string> GetTeamNameAgainstAway(int awayTeamId, int amountGame);
        List<short> GetMadeGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetMadeGoalsFullTimeAway(int awayTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeAway(int awayTeam, int amountGame);
        List<string> GetScheduleHome(int homeTeamId, int amountGame);
        List<string> GetScheduleAway(int awayTeamId, int amountGame);
        public List<League> GetAllLeagues();
        public List<Team> GetAllLeagueTeams(int idLeague);
        short GetAwayAmountTeamMatch(int idAwayTeam);
        short GetHomeAmountTeamMatch(int idHomeTeam);
        List<short> GetMadeCornersFullTimeHome(int hometeamId, int amountGame);
        List<short> GetMadeCornersFullTimeAway(int awayTeamId, int amountGame);
        List<short> GetTakenCornersFullTimeHome(int homeTeamId, int amountGame);
        List<short> GetTakenCornersFullTimeAway(int awayTeamId, int amountGame);


    }
}
