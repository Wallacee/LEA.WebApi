using LEA.WebApi.Dal.Repositories;
using LEA.WebApi.Domain.Interfaces;
using LEA.WebApi.Service.Interfaces;
using LEA.WebApi.Service.Services;
using Microsoft.Extensions.DependencyInjection;

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
            #endregion

            #region service
            services.AddScoped<IUploadService, UploadService>();
            #endregion
        }

    }
}
