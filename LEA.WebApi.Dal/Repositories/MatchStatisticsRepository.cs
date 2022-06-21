using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Dal.Repositories
{
    public class MatchStatisticsRepository : Repository<MatchStatistics>, IMatchStatisticsRepository
    {
        public MatchStatisticsRepository(Context context) : base(context) { }
        public void Save(MatchStatistics matchStatistics)
        {
            Create(matchStatistics);
        }
    }
}
