﻿// <auto-generated />
using System;
using LEA.WebApi.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LEA.WebApi.Dal.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LEA.WebApi.Domain.Models.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Coutry")
                        .HasColumnType("int");

                    b.Property<DateTime>("Creation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 7, 19, 0, 32, 13, 869, DateTimeKind.Local).AddTicks(419));

                    b.Property<short>("Division")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Shield")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("LEA.WebApi.Domain.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AwayStatisticsId")
                        .HasColumnType("int");

                    b.Property<int>("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Creation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(4628));

                    b.Property<int>("HomeStatisticsId")
                        .HasColumnType("int");

                    b.Property<int>("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<int?>("RefereeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Schedule")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AwayStatisticsId")
                        .IsUnique();

                    b.HasIndex("AwayTeamId");

                    b.HasIndex("HomeStatisticsId")
                        .IsUnique();

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("RefereeId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("LEA.WebApi.Domain.Models.MatchStatistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("Corners")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("Creation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(6485));

                    b.Property<short>("FoulsCommitted")
                        .HasColumnType("smallint");

                    b.Property<short>("GoalsFullTime")
                        .HasColumnType("smallint");

                    b.Property<short>("GoalsHalfTime")
                        .HasColumnType("smallint");

                    b.Property<short>("Red")
                        .HasColumnType("smallint");

                    b.Property<int>("ResultFullTime")
                        .HasColumnType("int");

                    b.Property<int>("ResultHalfTime")
                        .HasColumnType("int");

                    b.Property<short>("Shots")
                        .HasColumnType("smallint");

                    b.Property<short>("ShotsOnTarget")
                        .HasColumnType("smallint");

                    b.Property<short>("Yellow")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("MatchesStatistics");
                });

            modelBuilder.Entity("LEA.WebApi.Domain.Models.Referee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Creation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7183));

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Referees");
                });

            modelBuilder.Entity("LEA.WebApi.Domain.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Creation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7987));

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Shield")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("LEA.WebApi.Domain.Models.Match", b =>
                {
                    b.HasOne("LEA.WebApi.Domain.Models.MatchStatistics", "AwayStatistics")
                        .WithOne()
                        .HasForeignKey("LEA.WebApi.Domain.Models.Match", "AwayStatisticsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LEA.WebApi.Domain.Models.Team", "AwayTeam")
                        .WithMany("AwayMatches")
                        .HasForeignKey("AwayTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LEA.WebApi.Domain.Models.MatchStatistics", "HomeStatistics")
                        .WithOne()
                        .HasForeignKey("LEA.WebApi.Domain.Models.Match", "HomeStatisticsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LEA.WebApi.Domain.Models.Team", "HomeTeam")
                        .WithMany("HomeMatches")
                        .HasForeignKey("HomeTeamId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LEA.WebApi.Domain.Models.Referee", "Referee")
                        .WithMany("Matches")
                        .HasForeignKey("RefereeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("AwayStatistics");

                    b.Navigation("AwayTeam");

                    b.Navigation("HomeStatistics");

                    b.Navigation("HomeTeam");

                    b.Navigation("Referee");
                });

            modelBuilder.Entity("LEA.WebApi.Domain.Models.Team", b =>
                {
                    b.HasOne("LEA.WebApi.Domain.Models.League", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");
                });

            modelBuilder.Entity("LEA.WebApi.Domain.Models.League", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("LEA.WebApi.Domain.Models.Referee", b =>
                {
                    b.Navigation("Matches");
                });

            modelBuilder.Entity("LEA.WebApi.Domain.Models.Team", b =>
                {
                    b.Navigation("AwayMatches");

                    b.Navigation("HomeMatches");
                });
#pragma warning restore 612, 618
        }
    }
}
