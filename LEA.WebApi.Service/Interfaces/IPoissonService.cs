namespace LEA.WebApi.Service.Interfaces
{
    public interface IPoissonService
    {
        double ProbabilityOver(double lambda, double line);
        double ProbabilityBothScore(double lambdaHome, double lambdaAway);
    }
}
