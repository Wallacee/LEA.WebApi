using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface IMatchStatisticsRepository : IRepository<MatchStatistics>
    {
        void Save(MatchStatistics matchStatistics);
    }
}
