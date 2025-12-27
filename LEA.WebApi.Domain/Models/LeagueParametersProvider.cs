using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEA.WebApi.Domain.Models
{
    public static class LeagueParametersProvider
{
    public static LeagueModelParameters Get(int leagueId)
    {
        return leagueId switch
        {
            1 => new LeagueModelParameters // Premier League
            {
                HomeAdvantage = 1.10,
                GoalIntensity = 1.05,
                ShotIntensity = 1.10,
                ShotAccuracy = 1.00,
                CornerRate = 1.05,
                FoulRate = 0.95,
                CardSeverity = 0.90
            },

            2 => new LeagueModelParameters // La Liga
            {
                HomeAdvantage = 1.06,
                GoalIntensity = 0.95,
                ShotIntensity = 0.95,
                ShotAccuracy = 1.05,
                CornerRate = 0.90,
                FoulRate = 1.10,
                CardSeverity = 1.05
            },

            3 => new LeagueModelParameters // Ligue 1
            {
                HomeAdvantage = 1.04,
                GoalIntensity = 0.92,
                ShotIntensity = 0.90,
                ShotAccuracy = 0.95,
                CornerRate = 0.90,
                FoulRate = 1.05,
                CardSeverity = 1.10
            },

            4 => new LeagueModelParameters // Bundesliga
            {
                HomeAdvantage = 1.08,
                GoalIntensity = 1.15,
                ShotIntensity = 1.20,
                ShotAccuracy = 0.95,
                CornerRate = 1.10,
                FoulRate = 0.90,
                CardSeverity = 0.85
            },
            5 => new LeagueModelParameters // Serie A
            {
                HomeAdvantage = 1.05,
                GoalIntensity = 0.90,
                ShotIntensity = 0.85,
                ShotAccuracy = 1.00,
                CornerRate = 0.85,
                FoulRate = 1.15,
                CardSeverity = 1.20
            }

           ,

            _ => new LeagueModelParameters
            {
                HomeAdvantage = 1.05,
                GoalIntensity = 1.00,
                ShotIntensity = 1.00,
                ShotAccuracy = 1.00,
                CornerRate = 1.00,
                FoulRate = 1.00,
                CardSeverity = 1.00
            }
        };
    }
}

}
