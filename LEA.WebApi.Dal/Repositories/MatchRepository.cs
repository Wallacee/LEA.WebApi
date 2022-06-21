using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Dal.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        public MatchRepository(Context context) : base(context) { }

        public void Save(Match match)
        {
            Create(match);
        }
    }
}
