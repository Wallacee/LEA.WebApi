using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;
using LEA.WebApi.Infra.Models;
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
                .Select(team=>new Team() 
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

        #region Full_Stats_Match
        public List<EstatisticasPartida> GetAllStats(int teamId, int amountGame, bool isHome)
        {
            var query = Context.Matches
                .Where(match => isHome ? match.HomeTeamId == teamId : match.AwayTeamId == teamId)
                .Join(Context.MatchesStatistics,
                    match => isHome ? match.HomeStatisticsId : match.AwayStatisticsId,
                    matchStatistic => matchStatistic.Id,
                    (match, stats) => new { match, stats })
                .OrderByDescending(x => x.match.Id)
                .Take(amountGame)
                .Select(x => new EstatisticasPartida
                {
                    GolsPrimeiroTempo = x.stats.GoalsHalfTime,
                    GolsSegundoTempo = (short)(x.stats.GoalsFullTime - x.stats.GoalsHalfTime),
                    ChutesTotais = x.stats.Shots,
                    ChutesNoAlvo = x.stats.ShotsOnTarget,
                    Escanteios = x.stats.Corners,
                    Faltas = x.stats.FoulsCommitted,
                    CartoesAmarelos = x.stats.Yellow,
                    CartoesVermelhos = x.stats.Red,
                    Adversario = isHome ? x.match.AwayTeam.Name : x.match.HomeTeam.Name,
                    JogoEmCasa = isHome
                });

            return query.ToList();
        }

        public MediasCampeonato GetChampionshipAverage(bool isHome)
        {
            var result = Context.Matches
                .Join(Context.MatchesStatistics,
                    match => isHome ? match.HomeStatisticsId : match.AwayStatisticsId,
                    matchStatistic => matchStatistic.Id,
                    (match, stats) => new
                    {
                        stats.GoalsFullTime,
                        stats.GoalsHalfTime,
                        stats.Shots,
                        stats.ShotsOnTarget,
                        stats.Corners,
                        stats.FoulsCommitted,
                        stats.Yellow,
                        stats.Red
                    })
                .OrderByDescending(x => x.GoalsFullTime)
                .GroupBy(_ => 1)
                .Select(g => new MediasCampeonato
                {
                    MediaGolsPorPartida = Math.Round(g.Average(x => (double)x.GoalsFullTime), 2),
                    MediaGolsPrimeiroTempoPorPartida = Math.Round(g.Average(x => (double)x.GoalsHalfTime), 2),
                    MediaChutesPorPartida = Math.Round(g.Average(x => (double)x.Shots), 2),
                    MediaChutesNoAlvoPorPartida = Math.Round(g.Average(x => (double)x.ShotsOnTarget), 2),
                    MediaEscanteiosPorPartida = Math.Round(g.Average(x => (double)x.Corners), 2),
                    MediaFaltasPorPartida = Math.Round(g.Average(x => (double)x.FoulsCommitted), 2),
                    MediaCartoesAmarelosPorPartida = Math.Round(g.Average(x => (double)x.Yellow), 2),
                    MediaCartoesVermelhosPorPartida = Math.Round(g.Average(x => (double)x.Red), 2)
                })
                .FirstOrDefault();

            return result ?? new MediasCampeonato();
        }

        public MediasCampeonato GetChampionshipAverage()
        {
            var homeStats = Context.Matches
                .Join(Context.MatchesStatistics,
                    match => match.HomeStatisticsId,
                    stats => stats.Id,
                    (match, stats) => new { stats });

            var awayStats = Context.Matches
                .Join(Context.MatchesStatistics,
                    match => match.AwayStatisticsId,
                    stats => stats.Id,
                    (match, stats) => new { stats });

            var result = homeStats.Concat(awayStats)
                .Select(x => new
                {
                    x.stats.GoalsFullTime,
                    x.stats.GoalsHalfTime,
                    x.stats.Shots,
                    x.stats.ShotsOnTarget,
                    x.stats.Corners,
                    x.stats.FoulsCommitted,
                    x.stats.Yellow,
                    x.stats.Red
                })
                .OrderByDescending(x => x.GoalsFullTime)
                .GroupBy(_ => 1)
                .Select(g => new MediasCampeonato
                {
                    MediaGolsPorPartida = Math.Round(g.Average(x => (double)x.GoalsFullTime), 2),
                    MediaGolsPrimeiroTempoPorPartida = Math.Round(g.Average(x => (double)x.GoalsHalfTime), 2),
                    MediaChutesPorPartida = Math.Round(g.Average(x => (double)x.Shots), 2),
                    MediaChutesNoAlvoPorPartida = Math.Round(g.Average(x => (double)x.ShotsOnTarget), 2),
                    MediaEscanteiosPorPartida = Math.Round(g.Average(x => (double)x.Corners), 2),
                    MediaFaltasPorPartida = Math.Round(g.Average(x => (double)x.FoulsCommitted), 2),
                    MediaCartoesAmarelosPorPartida = Math.Round(g.Average(x => (double)x.Yellow), 2),
                    MediaCartoesVermelhosPorPartida = Math.Round(g.Average(x => (double)x.Red), 2)
                })
                .FirstOrDefault();

            return result ?? new MediasCampeonato();
        }

        #endregion

        #region Full_Time_Goals

        public List<Match> GetGoalsFullTimeAllMatchesByLeague(int idLeague)
        {
            return Context.Matches
                .Join(Context.MatchesStatistics, match => match.HomeStatisticsId, matchStatisticHome => matchStatisticHome.Id, (match, matchStatisticHome) => new { match, matchStatisticHome })
                .Join(Context.MatchesStatistics, match => match.match.AwayStatisticsId, matchStatisticAway => matchStatisticAway.Id, (match, matchStatisticAway) => new { match, matchStatisticAway })
                .Where(matches => matches.match.match.LeagueId == idLeague)
                .Select(matches => new Match()
                {
                    Schedule = matches.match.match.Schedule,
                    HomeStatistics = new MatchStatistics() { GoalsFullTime = matches.match.matchStatisticHome.GoalsFullTime },
                    AwayStatistics = new MatchStatistics() { GoalsFullTime = matches.matchStatisticAway.GoalsFullTime },
                    HomeTeamId = matches.match.match.HomeTeamId,
                    AwayTeamId = matches.match.match.AwayTeamId,
                }).ToList();
        }
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
        public List<Match> GetGoalsHalfTimeAllMatchesByLeague(int idLeague)
        {
            return Context.Matches
                .Join(Context.MatchesStatistics, match => match.HomeStatisticsId, matchStatisticHome => matchStatisticHome.Id, (match, matchStatisticHome) => new { match, matchStatisticHome })
                .Join(Context.MatchesStatistics, match => match.match.AwayStatisticsId, matchStatisticAway => matchStatisticAway.Id, (match, matchStatisticAway) => new { match, matchStatisticAway })
                .Where(matches => matches.match.match.LeagueId == idLeague)
                .Select(matches => new Match()
                {
                    Schedule = matches.match.match.Schedule,
                    HomeStatistics = new MatchStatistics() { GoalsHalfTime = matches.match.matchStatisticHome.GoalsHalfTime },
                    AwayStatistics = new MatchStatistics() { GoalsHalfTime = matches.matchStatisticAway.GoalsHalfTime },
                    HomeTeamId = matches.match.match.HomeTeamId,
                    AwayTeamId = matches.match.match.AwayTeamId,
                }).ToList();
        }
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

        #region Fouls
        public List<short> GetMadeFoulsFullTimeHome(int hometeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == hometeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.FoulsCommitted)
                                        .ToList();

        }

        public List<short> GetMadeFoulsFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(awayTeam => awayTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(awayStatistics => awayStatistics.matchStatisticAway.FoulsCommitted)
                                        .ToList();

        }

        public List<short> GetTakenFoulsFullTimeHome(int homeTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.AwayStatisticsId,
                                                        matchStatisticAway => matchStatisticAway.Id,
                                                        (match, matchStatisticAway) => new
                                                        { match, matchStatisticAway })
                                        .Where(homeTeam => homeTeam.match.HomeTeamId == homeTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticAway.FoulsCommitted)
                                        .ToList();

        }

        public List<short> GetTakenFoulsFullTimeAway(int awayTeamId, int amountGame)
        {
            return Context.Matches.Join(Context.MatchesStatistics,
                                                        match => match.HomeStatisticsId,
                                                        matchStatisticHome => matchStatisticHome.Id,
                                                        (match, matchStatisticHome) => new
                                                        { match, matchStatisticHome })
                                        .Where(homeTeam => homeTeam.match.AwayTeamId == awayTeamId)
                                        .Take(amountGame)
                                        .OrderByDescending(_match => _match.match.Id)
                                        .Select(homeStatistics => homeStatistics.matchStatisticHome.FoulsCommitted)
                                        .ToList();

        }

        #endregion

    }
}
