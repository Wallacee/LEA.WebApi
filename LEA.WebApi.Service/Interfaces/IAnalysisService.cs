using LEA.WebApi.Service.ViewModel;

namespace LEA.WebApi.Service.Interfaces
{
    public interface IAnalysisService
    {
        GeneralDataViewModel GeneralMatchData(int homeTeamId, int awayTeamId, int matchCount);
        LeaguesViewModel Leagues();
        TeamsViewModel Teams(int idLeague);
        short GetPossibleMatchAmount(int homeTeamId, int awayTeamId);
        AnalysisViewModel MatchGoalsFullTime(int homeTeamId, int awayTeamId, int matchCount);
        AnalysisViewModel MatchCornersFullTime(int homeTeamId, int awayTeamId, int matchCount);


    }

}
