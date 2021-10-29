using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Dal.Repositories
{
    public class LeagueRepository : Repository<League>, ILeagueRepository
    {
        public LeagueRepository(Context context) : base(context) { }
    }
}
