using System.Collections.Generic;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface IAnalysisRepository
    {
        List<short> GetMadeGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetMadeGoalsFullTimeAway(int awayTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeAway(int awayTeam, int amountGame);

    }
}
