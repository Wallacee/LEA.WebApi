using System;

namespace LEA.WebApi.Service.Interfaces
{
    public interface IBinomialService
    {
        double Expected(int trials, double probability);
    }
}
