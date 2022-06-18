using ExcelDataReader;
using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Service.Interfaces;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;
        private readonly ILeagueRepository leagueRepository;
        private readonly IMatchRepository matchRepository;
        private readonly ITeamRepository teamRepository;
        private readonly IRefereeRepository refereeRepository;
        public ILeagueRepository LeagueRepository => leagueRepository;

        public IMatchRepository MatchRepository => matchRepository;

        public ITeamRepository TeamRepository => teamRepository;

        public IRefereeRepository RefereeRepository => refereeRepository;

        public IConfiguration Configuration => configuration;

        public UploadService(ILeagueRepository leagueRepository, IMatchRepository matchRepository, ITeamRepository teamRepository, IRefereeRepository refereeRepository
, IConfiguration configuration)
        {
            this.leagueRepository = leagueRepository;
            this.matchRepository = matchRepository;
            this.teamRepository = teamRepository;
            this.refereeRepository = refereeRepository;
            this.configuration = configuration;
        }



        public string UpdateDatabaseByFileCSV(string fileName)
        {
            string folderName = configuration.GetSection("ApiConstant").GetSection("UploadFolderFile").Value;
            string filePath = Path.Combine(folderName, fileName);
            using (FileStream fileStream = new(filePath, FileMode.Open))
            using (StreamReader streamReader = new(fileStream, Encoding.GetEncoding(0)))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    Console.WriteLine(line);
                }
            }
            return "show";
        }

    }
}
