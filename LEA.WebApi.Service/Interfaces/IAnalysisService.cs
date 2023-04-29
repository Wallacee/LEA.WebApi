using LEA.WebApi.Service.ViewModel;
using System.Collections.Generic;
using System;
using LEA.WebApi.Domain.Models;

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



    }

}
