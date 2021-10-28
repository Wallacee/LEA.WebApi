using LEA.WebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LEA.WebApi.Dal
{
    public class Context : DbContext
    {
        public Context() : base() { }
        public Context(DbContextOptions<Context> dbContextOptions) : base(dbContextOptions) { }
        public DbSet<MatchStatistics> MatchesStatistics { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<League> Leagues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.GlobalConfiguration();

            modelBuilder
                .Entity<League>()
                .HasMany<Team>(l => l.Teams)
                .WithOne(t => t.League)
                .HasForeignKey(t => t.LeagueId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Team>()
                .HasMany<MatchStatistics>(t => t.MatchStatsList)
                .WithOne(ms => ms.Team)
                .HasForeignKey(ms => ms.TeamId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Match>()
                .HasOne(m => m.Home)
                .WithOne()
                .HasForeignKey<Match>(m => m.HomeId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Match>()
                .HasOne(m => m.Away)
                .WithOne()
                .HasForeignKey<Match>(m => m.AwayId).IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Match>()
                .HasOne<Referee>(m => m.Referee)
                .WithMany(r => r.Matches)
                .HasForeignKey(r => r.RefereeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
