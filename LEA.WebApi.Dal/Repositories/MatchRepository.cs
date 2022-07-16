using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;
using System;

namespace LEA.WebApi.Dal.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        public MatchRepository(Context context) : base(context) { }

        public Match FindByScheduleDateHomeAway(DateTime scheduleDate, string homeName, string awayName)
        {
            return Find(match =>
            match.Schedule == scheduleDate &&
            match.HomeTeam.Name == homeName &&
            match.AwayTeam.Name == awayName);
        }

        public void Save(Match match)
        {
            Create(match);
        }

    }
}
