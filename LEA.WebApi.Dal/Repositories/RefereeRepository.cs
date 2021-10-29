using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Dal.Repositories
{
    public class RefereeRepository : Repository<Referee>, IRefereeRepository
    {
        public RefereeRepository(Context context) : base(context) { }
    }
}
