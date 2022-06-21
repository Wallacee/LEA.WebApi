using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        Team FindById(int id);
        Team FindByName(string name);
        void Save(Team team);
    }
}
