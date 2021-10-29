using LEA.WebApi.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace LEA.WebApi.Dal.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        public MatchRepository(Context context) : base(context) { }
    }
}
