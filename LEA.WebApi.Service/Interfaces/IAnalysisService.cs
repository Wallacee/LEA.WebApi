using LEA.WebApi.Service.ViewModel;

namespace LEA.WebApi.Service.Interfaces
{
    public interface IAnalysisService
    {
        AnalysisViewModel MatchGoalsFullTime(int homeTeamId, int awayTeamId, int statisticId, int matchCount);
    }
}
