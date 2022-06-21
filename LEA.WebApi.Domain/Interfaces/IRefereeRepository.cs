using LEA.WebApi.Domain.Models;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface IRefereeRepository:IRepository<Referee>
    {
        void Save(Referee referee);
        Referee FindByName(string name);
        Referee FindById(int id);
    }
}
