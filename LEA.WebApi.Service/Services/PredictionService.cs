using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;
using LEA.WebApi.Service.Interfaces;
using LEA.WebApi.Service.ViewModel;
using System;

namespace LEA.WebApi.Service.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly IAnalysisRepository _repo;
        private readonly IPoissonService _poisson;

        private const double HOME_ADVANTAGE = 1.10;

        public PredictionService(
            IAnalysisRepository repo,
            IPoissonService poisson)
        {
            _repo = repo;
            _poisson = poisson;
        }

        public MatchPredictionResult Predict(PredictionRequestViewModel request)
        {
            var league = _repo.GetLeagueCalibration(
                request.LeagueId,
                request.MatchDate);

            var home = _repo.GetTeamProfile(
                request.HomeTeamId,
                request.LeagueId,
                request.MatchDate,
                true);

            var away = _repo.GetTeamProfile(
                request.AwayTeamId,
                request.LeagueId,
                request.MatchDate,
                false);

            double goalsHome =
                league.Goals *
                home.AttackStrength *
                away.DefenseStrength *
                1.10;

            double goalsAway =
                league.Goals *
                away.AttackStrength *
                home.DefenseStrength;

            double intensityHome = goalsHome / league.Goals;
            double intensityAway = goalsAway / league.Goals;

            double Scale(double baseValue, double intensity) =>
                Math.Max(0, baseValue * intensity);

            return new MatchPredictionResult
            {
                Goals = new PredictionPair(goalsHome, goalsAway),

                Shots = new PredictionPair(
                    Scale(league.Shots, intensityHome),
                    Scale(league.Shots, intensityAway)),

                ShotsOnTarget = new PredictionPair(
                    Scale(league.ShotsOnTarget, intensityHome),
                    Scale(league.ShotsOnTarget, intensityAway)),

                Corners = new PredictionPair(
                    Scale(league.Corners, intensityHome),
                    Scale(league.Corners, intensityAway)),

                Fouls = new PredictionPair(
                    Scale(league.Fouls, intensityAway),
                    Scale(league.Fouls, intensityHome)),

                YellowCards = new PredictionPair(
                    Scale(league.YellowCards, intensityAway),
                    Scale(league.YellowCards, intensityHome)),

                RedCards = new PredictionPair(
                    league.RedCards,
                    league.RedCards),

                Over25Probability =
                    _poisson.ProbabilityOver(goalsHome + goalsAway, 2.5),

                BttsProbability =
                    _poisson.ProbabilityBothScore(goalsHome, goalsAway)
            };
        }

    }

}
