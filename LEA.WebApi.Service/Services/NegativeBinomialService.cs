using LEA.WebApi.Service.Interfaces;
using MathNet.Numerics.Distributions;
using System;

namespace LEA.WebApi.Service.Services
{
    public class NegativeBinomialService : INegativeBinomialService
    {
        public double ProbabilityOver(double mean, double dispersion, double line)
        {
            if (mean <= 0)
                return 0.0;

            // Se não há overdispersion, cai para Poisson
            if (dispersion <= mean)
            {
                int k = (int)System.Math.Floor(line);
                return 1.0 - Poisson.CDF(mean, k);
            }

            double r = (mean * mean) / (dispersion - mean);
            double p = r / (r + mean);

            if (double.IsNaN(r) || double.IsNaN(p) || p <= 0 || p >= 1)
                return 0.0;

            int x = (int)System.Math.Floor(line);

            // MathNet: CDF(r, p, x) = P(X <= x)
            return 1.0 - NegativeBinomial.CDF(r, p, x);
        }

        public double Expected(double mean)
        {
            return mean;
        }
    }
}
