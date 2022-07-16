using LEA.WebApi.Domain.Models;
using System;

namespace LEA.WebApi.Domain.Interfaces
{
    public interface IMatchRepository : IRepository<Match>
    {
        void Save(Match match);
        Match FindByScheduleDateHomeAway(DateTime scheduleDate,string homeName, string awayName);
        
    }
}
