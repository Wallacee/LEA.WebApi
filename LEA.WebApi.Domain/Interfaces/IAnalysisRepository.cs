using System.Collections.Generic;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface IAnalysisRepository
    {
        List<string> GetTeamNameAgainstHome(int homeTeamId, int amountGame);
        List<string> GetTeamNameAgainstAway(int awayTeamId, int amountGame);
        List<short> GetMadeGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetMadeGoalsFullTimeAway(int awayTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeHome(int homeTeam, int amountGame);
        List<short> GetTakenGoalsFullTimeAway(int awayTeam, int amountGame);

    }
}
