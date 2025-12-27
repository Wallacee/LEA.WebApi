namespace LEA.WebApi.Service.Interfaces
{
    public interface INegativeBinomialService
    {
        double ProbabilityOver(double mean, double dispersion, double line);
    }
}
