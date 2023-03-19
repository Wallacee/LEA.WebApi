using LEA.WebApi.Domain.Interfaces;
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

    }
}
