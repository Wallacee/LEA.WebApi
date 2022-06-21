using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Dal.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(Context context) : base(context) { }

        public Team FindById(int id)
        {
            return Find(t => t.Id == id);
        }

        public Team FindByName(string name)
        {
            return Find(t => t.Name == name);
        }

        public void Save(Team team)
        {
            Create(team);
        }
    }
}
