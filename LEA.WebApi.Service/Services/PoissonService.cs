using LEA.WebApi.Service.Interfaces;
using MathNet.Numerics.Distributions;

namespace LEA.WebApi.Service.Services
{
    public class PoissonService : IPoissonService
    {
        public double ProbabilityOver(double lambda, double line)
        {
            var dist = new Poisson(lambda);
            return 1.0 - dist.CumulativeDistribution((int)line);
        }

        public double ProbabilityBothScore(double lambdaHome, double lambdaAway)
        {
            var home = new Poisson(lambdaHome);
            var away = new Poisson(lambdaAway);

            return (1 - home.Probability(0)) *
                   (1 - away.Probability(0));
        }
    }
}
