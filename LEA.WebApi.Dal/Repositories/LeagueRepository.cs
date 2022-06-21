using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Dal.Repositories
{
    public class LeagueRepository : Repository<League>, ILeagueRepository
    {
        public LeagueRepository(Context context) : base(context) { }

        public League FindById(int id)
        {
            return Find(l => l.Id == id);
        }

        public League FindByName(string name)
        {
            return Find(l => l.Name == name);
        }

        public void Save(League league)
        {
            Create(league);
        }
    }
}
