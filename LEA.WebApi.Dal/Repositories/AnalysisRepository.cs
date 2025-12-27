using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace LEA.WebApi.Dal.Repositories
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly Context context;
        private const double HalfLifeDays = 180.0;
        public Context Context => context;

        public AnalysisRepository(Context context)
        {
            this.context = context;
        }
        #region General_Analysis_Data
        public List<Team> GetTeamAgainstHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.Teams,
                                                        match => match.AwayTeamId,
                                                        awayTeam => awayTeam.Id,
                                                        (match, awayTeam) => new
                                                        { match, awayTeam })
                .Where(_match => _match.match.HomeTeamId == homeTeamId)
                .Take(amountGame)
                .OrderByDescending(_match => _match.match.Id)
                .Select(team => new Team()
                {
                    Id = team.awayTeam.Id,
                    Name = team.awayTeam.Name,
                    LeagueId = team.awayTeam.LeagueId
                }).ToList();
        }
        public List<Team> GetTeamAgainstAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.Teams,
                                                        match => match.HomeTeamId,
                                                        homeTeam => homeTeam.Id,
                                                        (match, homeTeam) => new
                                                        { match, homeTeam })
                .Where(_match => _match.match.AwayTeamId == awayTeamId)
                .Take(amountGame)
                .OrderByDescending(_match => _match.match.Id)
                .Select(team => new Team()
                {
                    Id = team.homeTeam.Id,
                    Name = team.homeTeam.Name,
                    LeagueId = team.homeTeam.LeagueId
                })
                .ToList();
        }

        public List<DateTime> GetScheduleHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Where(match => match.HomeTeamId == homeTeamId)
                                  .Take(amountGame)
                                  .OrderByDescending(match => match.Id)
                                  .Select(match => match.Schedule)
                                  .ToList();
        }
        public List<DateTime> GetScheduleAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Where(match => match.AwayTeamId == awayTeamId)
                                  .Take(amountGame)
                                  .OrderByDescending(match => match.Id)
                                  .Select(match => match.Schedule)
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

        public List<Match> GetAllMatchesByLeague(int idLeague)
        {
            return Context.Matches
                .Join(Context.MatchesStatistics, match => match.HomeStatisticsId, matchStatisticHome => matchStatisticHome.Id, (match, matchStatisticHome) => new { match, matchStatisticHome })
                .Join(Context.MatchesStatistics, match => match.match.AwayStatisticsId, matchStatisticAway => matchStatisticAway.Id, (match, matchStatisticAway) => new { match, matchStatisticAway })
                .Where(matches => matches.match.match.LeagueId == idLeague)
                .Select(matches => new Match()
                {
                    Schedule = matches.match.match.Schedule,
                    HomeStatistics = new MatchStatistics() { GoalsFullTime = matches.match.matchStatisticHome.GoalsFullTime, ResultFullTime = matches.match.matchStatisticHome.ResultFullTime },
                    AwayStatistics = new MatchStatistics() { GoalsFullTime = matches.matchStatisticAway.GoalsFullTime, ResultFullTime = matches.matchStatisticAway.ResultFullTime },
                    HomeTeamId = matches.match.match.HomeTeamId,
                    AwayTeamId = matches.match.match.AwayTeamId,
                }).ToList();


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

        #region Half_Time_Goals
        public List<short> GetMadeGoalsHalfTimeHome(int hometeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == hometeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.GoalsHalfTime)
                                        .ToList();

        }

        public List<short> GetMadeGoalsHalfTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(awayTeam => awayTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(awayStatistics => awayStatistics.matchStatisticAway.GoalsHalfTime)
                                        .ToList();

        }

        public List<short> GetTakenGoalsHalfTimeHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == homeTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticAway.GoalsHalfTime)
                                        .ToList();

        }

        public List<short> GetTakenGoalsHalfTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.GoalsHalfTime)
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

        #region Yellow_Full_Time_region
        public List<short> GetMadeYellowFullTimeHome(int hometeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == hometeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.Yellow)
                                        .ToList();

        }

        public List<short> GetMadeYellowFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(awayTeam => awayTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(awayStatistics => awayStatistics.matchStatisticAway.Yellow)
                                        .ToList();

        }

        public List<short> GetTakenYellowFullTimeHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == homeTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticAway.Yellow)
                                        .ToList();

        }

        public List<short> GetTakenYellowFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.Yellow)
                                        .ToList();

        }

        #endregion

        #region Red_Full_Time_region
        public List<short> GetMadeRedFullTimeHome(int hometeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == hometeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.Red)
                                        .ToList();

        }

        public List<short> GetMadeRedFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(awayTeam => awayTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(awayStatistics => awayStatistics.matchStatisticAway.Red)
                                        .ToList();

        }

        public List<short> GetTakenRedFullTimeHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == homeTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticAway.Red)
                                        .ToList();

        }

        public List<short> GetTakenRedFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.Red)
                                        .ToList();

        }

        #endregion

        #region Shots_Full_Time_region
        public List<short> GetMadeShotsFullTimeHome(int hometeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == hometeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.Shots)
                                        .ToList();

        }

        public List<short> GetMadeShotsFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(awayTeam => awayTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(awayStatistics => awayStatistics.matchStatisticAway.Shots)
                                        .ToList();

        }

        public List<short> GetTakenShotsFullTimeHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == homeTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticAway.Shots)
                                        .ToList();

        }

        public List<short> GetTakenShotsFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.Shots)
                                        .ToList();

        }

        #endregion

        #region ShotsOnTarget_Full_Time_region
        public List<short> GetMadeShotsOnTargetFullTimeHome(int hometeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == hometeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.ShotsOnTarget)
                                        .ToList();

        }

        public List<short> GetMadeShotsOnTargetFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(awayTeam => awayTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(awayStatistics => awayStatistics.matchStatisticAway.ShotsOnTarget)
                                        .ToList();

        }

        public List<short> GetTakenShotsOnTargetFullTimeHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == homeTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticAway.ShotsOnTarget)
                                        .ToList();

        }

        public List<short> GetTakenShotsOnTargetFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.ShotsOnTarget)
                                        .ToList();

        }

        #endregion

        public LeagueAverages GetLeagueAverages(int leagueId, DateTime matchDate)
        {
            var matches = Context.Matches
                .Where(m => m.LeagueId == leagueId && m.Schedule < matchDate);

            return new LeagueAverages
            {
                Goals = matches.Average(m =>
                    (double)m.HomeStatistics.GoalsFullTime +
                    (double)m.AwayStatistics.GoalsFullTime),

                Shots = matches.Average(m =>
                    (double)m.HomeStatistics.Shots +
                    (double)m.AwayStatistics.Shots),

                ShotsOnTarget = matches.Average(m =>
                    (double)m.HomeStatistics.ShotsOnTarget +
                    (double)m.AwayStatistics.ShotsOnTarget),

                Corners = matches.Average(m =>
                    (double)m.HomeStatistics.Corners +
                    (double)m.AwayStatistics.Corners),

                Fouls = matches.Average(m =>
                    (double)m.HomeStatistics.FoulsCommitted +
                    (double)m.AwayStatistics.FoulsCommitted),

                Yellow= matches.Average(m =>
                    (double)m.HomeStatistics.Yellow +
                    (double)m.AwayStatistics.Yellow),

                Red= matches.Average(m =>
                    (double)m.HomeStatistics.Red +
                    (double)m.AwayStatistics.Red)
            };
        }

        // =========================
        // FORÇA DO TIME
        // =========================
        public TeamStrengthProfile GetTeamStrength(int teamId, int leagueId, DateTime matchDate)
        {
            var leagueAvg = GetLeagueAverages(leagueId, matchDate);

            var homeMatches = Context.Matches
                .Where(m => m.HomeTeamId == teamId && m.Schedule < matchDate);

            var awayMatches = Context.Matches
                .Where(m => m.AwayTeamId == teamId && m.Schedule < matchDate);

            return new TeamStrengthProfile
            {
                Home = new Strength
                {
                    Attack = homeMatches.Average(m => m.HomeStatistics.GoalsFullTime)
                             / (leagueAvg.Goals / 2),

                    Defense = homeMatches.Average(m => m.AwayStatistics.GoalsFullTime)
                              / (leagueAvg.Goals / 2)
                },
                Away = new Strength
                {
                    Attack = awayMatches.Average(m => m.AwayStatistics.GoalsFullTime)
                             / (leagueAvg.Goals / 2),

                    Defense = awayMatches.Average(m => m.HomeStatistics.GoalsFullTime)
                              / (leagueAvg.Goals / 2)
                }
            };

        }
        public TeamProfile GetTeamProfile(
          int teamId,
          int leagueId,
          DateTime matchDate,
          bool isHome)
        {
            List<(MatchStatistics stats, MatchStatistics oppStats, double daysAgo)> data;

            if (isHome)
            {
                data = Context.Matches
                    .Where(m =>
                        m.LeagueId == leagueId &&
                        m.HomeTeamId == teamId &&
                        m.Schedule < matchDate)
                    .Select(m => new
                    {
                        Stats = m.HomeStatistics,
                        OppStats = m.AwayStatistics,
                        DaysAgo = (matchDate - m.Schedule).TotalDays
                    })
                    .AsEnumerable()
                    .Select(x => (x.Stats, x.OppStats, x.DaysAgo))
                    .ToList();
            }
            else
            {
                data = Context.Matches
                    .Where(m =>
                        m.LeagueId == leagueId &&
                        m.AwayTeamId == teamId &&
                        m.Schedule < matchDate)
                    .Select(m => new
                    {
                        Stats = m.AwayStatistics,
                        OppStats = m.HomeStatistics,
                        DaysAgo = (matchDate - m.Schedule).TotalDays
                    })
                    .AsEnumerable()
                    .Select(x => (x.Stats, x.OppStats, x.DaysAgo))
                    .ToList();
            }

            if (!data.Any())
                return new TeamProfile();

            // -------------------------------
            // Time-decay (ponderação temporal)
            // -------------------------------
            double Weight(double daysAgo) =>
                Math.Exp(-daysAgo / 180.0);

            double totalWeight = data.Sum(x => Weight(x.daysAgo));

            double WAvg(Func<MatchStatistics, double> selector) =>
                data.Sum(x => selector(x.stats) * Weight(x.daysAgo)) / totalWeight;

            double WAvgOpp(Func<MatchStatistics, double> selector) =>
                data.Sum(x => selector(x.oppStats) * Weight(x.daysAgo)) / totalWeight;

            // -------------------------------
            // MÉDIAS ABSOLUTAS DO TIME
            // -------------------------------
            double goalsFor = WAvg(s => s.GoalsFullTime);
            double goalsAgainst = WAvgOpp(s => s.GoalsFullTime);

            // -------------------------------
            // MÉDIA DA LIGA (POR TIME)
            // -------------------------------
            double leagueAvgGoals =
                data.Sum(x =>
                    (x.stats.GoalsFullTime + x.oppStats.GoalsFullTime)
                    * Weight(x.daysAgo)) / (2.0 * totalWeight);

            leagueAvgGoals = Math.Max(leagueAvgGoals, 0.1);

            return new TeamProfile
            {
                // FORÇAS RELATIVAS (≈ 0.6 – 1.6)
                AttackStrength = goalsFor / leagueAvgGoals,
                DefenseStrength = goalsAgainst / leagueAvgGoals,

                // MÉDIAS ABSOLUTAS (usadas depois com modulação)
                Shots = WAvg(s => s.Shots),
                ShotsOnTarget = WAvg(s => s.ShotsOnTarget),
                Corners = WAvg(s => s.Corners),
                Fouls = WAvg(s => s.FoulsCommitted),
                YellowCards = WAvg(s => s.Yellow),
                RedCards = WAvg(s => s.Red)
            };
        }

        public LeagueCalibration GetLeagueCalibration(
    int leagueId,
    DateTime matchDate)
        {
            var matches = Context.Matches
                .Where(m =>
                    m.LeagueId == leagueId &&
                    m.Schedule < matchDate)
                .Select(m => new
                {
                    Home = m.HomeStatistics,
                    Away = m.AwayStatistics,
                    DaysAgo = (matchDate - m.Schedule).TotalDays
                })
                .AsEnumerable()
                .ToList();

            if (!matches.Any())
                return new LeagueCalibration();

            double Weight(double daysAgo) =>
                Math.Exp(-daysAgo / 180.0);

            double totalWeight = matches.Sum(x => Weight(x.DaysAgo));

            double Avg(Func<MatchStatistics, double> selector) =>
                matches.Sum(x =>
                    (selector(x.Home) + selector(x.Away)) *
                    Weight(x.DaysAgo)
                ) / (2.0 * totalWeight);

            return new LeagueCalibration
            {
                Goals = Avg(s => s.GoalsFullTime),
                Shots = Avg(s => s.Shots),
                ShotsOnTarget = Avg(s => s.ShotsOnTarget),
                Corners = Avg(s => s.Corners),
                Fouls = Avg(s => s.FoulsCommitted),
                YellowCards = Avg(s => s.Yellow),
                RedCards = Avg(s => s.Red)
            };
        }



    }
}

