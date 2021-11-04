using ExcelDataReader;
using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEA.WebApi.Service.Services
{
    public class UploadService : IUploadService
    {
        private readonly ILeagueRepository leagueRepository;
        private readonly IMatchRepository matchRepository;
        private readonly ITeamRepository teamRepository;
        private readonly IRefereeRepository refereeRepository;

        public UploadService(
            ILeagueRepository leagueRepository,
            IMatchRepository matchRepository,
            ITeamRepository teamRepository,
            IRefereeRepository refereeRepository
            )
        {
            this.leagueRepository = leagueRepository;
            this.matchRepository = matchRepository;
            this.teamRepository = teamRepository;
            this.refereeRepository = refereeRepository;
        }
        public string Upload(FileStream file)
        {
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(file);
            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(3).ToString());
            }
            return "show";
        }

    }
}
