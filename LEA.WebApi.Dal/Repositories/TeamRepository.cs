using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Dal.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(Context context) : base(context) { }
    }
}
