using LEA.WebApi.Domain.Models;
using System;
using System.Collections.Generic;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface IAnalysisRepository
    {
        List<Team> GetTeamAgainstHome(int homeTeamId, int amountGame);
        List<Team> GetTeamAgainstAway(int awayTeamId, int amountGame);
        List<Match> GetAllMatchesByLeague(int idLeague);
        
        List<DateTime> GetScheduleHome(int homeTeamId, int amountGame);
        List<DateTime> GetScheduleAway(int awayTeamId, int amountGame);
        
        public List<League> GetAllLeagues();
        public List<Team> GetAllLeagueTeams(int idLeague);
        
        short GetHomeAmountTeamMatch(int idHomeTeam);
        short GetAwayAmountTeamMatch(int idAwayTeam);

        List<short> GetMadeGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetMadeGoalsFullTimeAway(int awayTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeAway(int awayTeam, int amountGame);

        List<short> GetMadeCornersFullTimeHome(int hometeamId, int amountGame);
        List<short> GetMadeCornersFullTimeAway(int awayTeamId, int amountGame);
        List<short> GetTakenCornersFullTimeHome(int homeTeamId, int amountGame);
        List<short> GetTakenCornersFullTimeAway(int awayTeamId, int amountGame);

        List<short> GetTakenGoalsHalfTimeAway(int awayTeamId, int amountGame);
        List<short> GetTakenGoalsHalfTimeHome(int homeTeamId, int amountGame);
        List<short> GetMadeGoalsHalfTimeAway(int awayTeamId, int amountGame);
        List<short> GetMadeGoalsHalfTimeHome(int hometeamId, int amountGame);

        List<short> GetMadeYellowFullTimeHome(int hometeamId, int amountGame);
        List<short> GetMadeYellowFullTimeAway(int awayTeamId, int amountGame);
        List<short> GetTakenYellowFullTimeHome(int homeTeamId, int amountGame);
        List<short> GetTakenYellowFullTimeAway(int awayTeamId, int amountGame);

        List<short> GetMadeRedFullTimeHome(int hometeamId, int amountGame);
        List<short> GetMadeRedFullTimeAway(int awayTeamId, int amountGame);
        List<short> GetTakenRedFullTimeHome(int homeTeamId, int amountGame);
        List<short> GetTakenRedFullTimeAway(int awayTeamId, int amountGame);

        List<short> GetMadeShotsFullTimeHome(int hometeamId, int amountGame);
        List<short> GetMadeShotsFullTimeAway(int awayTeamId, int amountGame);
        List<short> GetTakenShotsFullTimeHome(int homeTeamId, int amountGame);
        List<short> GetTakenShotsFullTimeAway(int awayTeamId, int amountGame);

        List<short> GetMadeShotsOnTargetFullTimeHome(int hometeamId, int amountGame);
        List<short> GetMadeShotsOnTargetFullTimeAway(int awayTeamId, int amountGame);
        List<short> GetTakenShotsOnTargetFullTimeHome(int homeTeamId, int amountGame);
        List<short> GetTakenShotsOnTargetFullTimeAway(int awayTeamId, int amountGame);

    }
}
