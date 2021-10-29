using LEA.WebApi.Dal.Repositories;
using LEA.WebApi.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace LEA.WebApi.IoC
{
    public static class NativeInjector
    {
        public static void RegisterService(IServiceCollection services)
        {
            #region
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IRefereeRepository, RefereeRepository>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            #endregion
        }

    }
}
