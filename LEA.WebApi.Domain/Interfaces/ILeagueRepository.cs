using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface ILeagueRepository:IRepository<League>
    {
        League FindById(int id);
        League FindByName(string name);
        void Save(League league);
    }
}
