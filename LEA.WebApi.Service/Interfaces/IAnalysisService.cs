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
        AnalysisViewModel MatchGoalsHalfTime(int homeTeamId, int awayTeamId, int matchCount);
        AnalysisViewModel MatchYellowFullTime(int homeTeamId, int awayTeamId, int matchCount);
        AnalysisViewModel MatchRedFullTime(int homeTeamId, int awayTeamId, int matchCount);
        AnalysisViewModel MatchShotsFullTime(int homeTeamId, int awayTeamId, int matchCount);
        AnalysisViewModel MatchShotsOnTargetFullTime(int homeTeamId, int awayTeamId, int matchCount);



    }

}
