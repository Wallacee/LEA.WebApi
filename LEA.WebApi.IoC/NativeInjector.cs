using LEA.WebApi.Dal;
using LEA.WebApi.Dal.Repositories;
using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Service.Interfaces;
using LEA.WebApi.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace LEA.WebApi.IoC
{
    public static class NativeInjector
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region repository
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IRefereeRepository, RefereeRepository>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IMatchStatisticsRepository, MatchStatisticsRepository>();
            services.AddScoped<IAnalysisRepository, AnalysisRepository>();


            #endregion

            #region service
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IAnalysisService, AnalysisService>();
            #endregion
        }

    }
}
