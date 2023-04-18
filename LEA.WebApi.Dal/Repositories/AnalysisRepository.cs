﻿using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LEA.WebApi.Dal.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly Context context;

        public Context Context => context;

        public AnalysisRepository(Context context)
        {
            this.context = context;
        }
        #region General_Analysis_Data
        public List<string> GetTeamNameAgainstHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.Teams,
                                                        match => match.AwayTeamId,
                                                        awayTeam => awayTeam.Id,
                                                        (match, awayTeam) => new
                                                        { match, awayTeam })
                .Where(_match => _match.match.HomeTeamId == homeTeamId)
                .Take(amountGame)
                .OrderByDescending(_match => _match.match.Id)
                .Select(_awayTeam => _awayTeam.awayTeam.Name)
                .ToList();
        }
        public List<string> GetTeamNameAgainstAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.Teams,
                                                        match => match.HomeTeamId,
                                                        homeTeam => homeTeam.Id,
                                                        (match, homeTeam) => new
                                                        { match, homeTeam })
                .Where(_match => _match.match.AwayTeamId == awayTeamId)
                .Take(amountGame)
                .OrderByDescending(_match => _match.match.Id)
                .Select(_awayTeam => _awayTeam.homeTeam.Name)
                .ToList();
        }

        public List<string> GetScheduleHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Where(match => match.HomeTeamId == homeTeamId)
                                  .Take(amountGame)
                                  .OrderByDescending(match => match.Id)
                                  .Select(match => match.Schedule.ToString("dd/MM/yy HH:mm ddd"))
                                  .ToList();
        }
        public List<string> GetScheduleAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Where(match => match.AwayTeamId == awayTeamId)
                                  .Take(amountGame)
                                  .OrderByDescending(match => match.Id)
                                  .Select(match => match.Schedule.ToString("dd/MM/yy HH:mm ddd"))
                                  .ToList();
        }

        public List<League> GetAllLeagues()
        {
            return Context.Leagues.ToList();
        }
        public List<Team> GetAllLeagueTeams(int idLeague)
        {
            return Context.Teams.Where(team => team.LeagueId == idLeague).ToList();
        }

        public short GetHomeAmountTeamMatch(int idHomeTeam)
        {
            return (short)Context.Matches.Where(teamMatch => teamMatch.HomeTeamId == idHomeTeam).Count();
        }
        public short GetAwayAmountTeamMatch(int idAwayTeam)
        {
            return (short)Context.Matches.Where(teamMatch => teamMatch.HomeTeamId == idAwayTeam).Count();
        }
        #endregion

        #region Full_Time_Goals
        public List<short> GetMadeGoalsFullTimeHome(int hometeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == hometeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.GoalsFullTime)
                                        .ToList();

        }

        public List<short> GetMadeGoalsFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(awayTeam => awayTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(awayStatistics => awayStatistics.matchStatisticAway.GoalsFullTime)
                                        .ToList();

        }

        public List<short> GetTakenGoalsFullTimeHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == homeTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticAway.GoalsFullTime)
                                        .ToList();

        }

        public List<short> GetTakenGoalsFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.GoalsFullTime)
                                        .ToList();

        }
        #endregion

        #region Corner_Full_Time_region
        public List<short> GetMadeCornersFullTimeHome(int hometeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == hometeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.Corners)
                                        .ToList();

        }

        public List<short> GetMadeCornersFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(awayTeam => awayTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(awayStatistics => awayStatistics.matchStatisticAway.Corners)
                                        .ToList();

        }

        public List<short> GetTakenCornersFullTimeHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == homeTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticAway.Corners)
                                        .ToList();

        }

        public List<short> GetTakenCornersFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.Corners)
                                        .ToList();

        }
        #endregion

    }
}
