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
                var connectionString = configuration.GetConnectionString("DefaultConnectionSQLServer");
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
                .HasIndex(n => n.Name)
                .IsUnique();

            modelBuilder
                .Entity<Match>()
                .HasOne<Team>(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Match>()
                .HasOne<Team>(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Match>()
                .HasOne<Referee>(m => m.Referee)
                .WithMany(r => r.Matches)
                .HasForeignKey(r => r.RefereeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Match>()
                .HasOne(m => m.HomeStatistics)
                .WithOne()
                .HasForeignKey<Match>(m => m.HomeStatisticsId).IsRequired()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<Match>()
                .HasOne(m => m.AwayStatistics)
                .WithOne()
                .HasForeignKey<Match>(m => m.AwayStatisticsId).IsRequired()
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                        .Entity<Match>()
                        .HasOne<League>(m => m.League)
                        .WithMany(l => l.Matches)
                        .HasForeignKey(l => l.LeagueId)
                        .IsRequired(false)
                        .OnDelete(DeleteBehavior.NoAction);
        }


    }
}
