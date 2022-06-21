using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface IMatchRepository : IRepository<Match>
    {
        void Save(Match match);
    }
}
