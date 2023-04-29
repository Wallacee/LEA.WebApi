using LEA.WebApi.Domain.Models;
using System;
using System.Collections.Generic;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface IAnalysisRepository
    {
        List<Team> GetTeamAgainstHome(int homeTeamId, int amountGame);
        List<Team> GetTeamAgainstAway(int awayTeamId, int amountGame);
        List<short> GetMadeGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetMadeGoalsFullTimeAway(int awayTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeAway(int awayTeam, int amountGame);
        List<DateTime> GetScheduleHome(int homeTeamId, int amountGame);
        List<DateTime> GetScheduleAway(int awayTeamId, int amountGame);
        public List<League> GetAllLeagues();
        public List<Team> GetAllLeagueTeams(int idLeague);
        short GetAwayAmountTeamMatch(int idAwayTeam);
        short GetHomeAmountTeamMatch(int idHomeTeam);
        List<short> GetMadeCornersFullTimeHome(int hometeamId, int amountGame);
        List<short> GetMadeCornersFullTimeAway(int awayTeamId, int amountGame);
        List<short> GetTakenCornersFullTimeHome(int homeTeamId, int amountGame);
        List<short> GetTakenCornersFullTimeAway(int awayTeamId, int amountGame);
        List<Match> GetAllMatchesByLeague(int idLeague);

        List<short> GetTakenGoalsHalfTimeAway(int awayTeamId, int amountGame);
        List<short> GetTakenGoalsHalfTimeHome(int homeTeamId, int amountGame);
        List<short> GetMadeGoalsHalfTimeAway(int awayTeamId, int amountGame);
        List<short> GetMadeGoalsHalfTimeHome(int hometeamId, int amountGame);




    }
}
