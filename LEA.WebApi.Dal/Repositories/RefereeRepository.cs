using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Dal.Repositories
{
    public class RefereeRepository : Repository<Referee>, IRefereeRepository
    {
        public RefereeRepository(Context context) : base(context) { }

        public Referee FindById(int id)
        {
            return Find(r => r.Id == id);
        }

        public Referee FindByName(string name)
        {
            return Find(r => r.Name == name);
        }

        public void Save(Referee referee)
        {
            Create(referee);
        }
    }
}
