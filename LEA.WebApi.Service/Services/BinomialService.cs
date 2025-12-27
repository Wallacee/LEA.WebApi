using LEA.WebApi.Service.Interfaces;

namespace LEA.WebApi.Service.Services
{
    public class BinomialService : IBinomialService
    {
        public double Expected(int trials, double probability)
        {
            return trials * probability;
        }
    }
}
