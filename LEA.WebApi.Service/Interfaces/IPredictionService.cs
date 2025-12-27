using LEA.WebApi.Domain.Models;
using LEA.WebApi.Service.ViewModel;

namespace LEA.WebApi.Service.Interfaces
{
    public interface IPredictionService
    {
        MatchPredictionResult Predict(PredictionRequestViewModel request);
    }
}
