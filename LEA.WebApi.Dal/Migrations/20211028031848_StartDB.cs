using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LEA.WebApi.Dal.Migrations
{
    public partial class StartDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Coutry = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<short>(type: "smallint", nullable: false),
                    Shield = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 400, DateTimeKind.Local).AddTicks(5106))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Referees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(7959))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Shield = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(8168))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchsStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    ResultFullTime = table.Column<int>(type: "int", nullable: false),
                    ResultHalfTime = table.Column<int>(type: "int", nullable: false),
                    GoalsFullTime = table.Column<short>(type: "smallint", nullable: false),
                    GoalsHalfTime = table.Column<short>(type: "smallint", nullable: false),
                    Shots = table.Column<short>(type: "smallint", nullable: false),
                    ShotsOnTarget = table.Column<short>(type: "smallint", nullable: false),
                    Woodwork = table.Column<short>(type: "smallint", nullable: false),
                    Corners = table.Column<short>(type: "smallint", nullable: false),
                    Fouls = table.Column<short>(type: "smallint", nullable: false),
                    Offside = table.Column<short>(type: "smallint", nullable: false),
                    Yellow = table.Column<short>(type: "smallint", nullable: false),
                    Red = table.Column<short>(type: "smallint", nullable: false),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(7649))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchsStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchsStatistics_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Schedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeId = table.Column<int>(type: "int", nullable: false),
                    AwayId = table.Column<int>(type: "int", nullable: false),
                    RefereeId = table.Column<int>(type: "int", nullable: false),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(6725))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_MatchsStatistics_AwayId",
                        column: x => x.AwayId,
                        principalTable: "MatchsStatistics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_MatchsStatistics_HomeId",
                        column: x => x.HomeId,
                        principalTable: "MatchsStatistics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Referees_RefereeId",
                        column: x => x.RefereeId,
                        principalTable: "Referees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_AwayId",
                table: "Matches",
                column: "AwayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_HomeId",
                table: "Matches",
                column: "HomeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_RefereeId",
                table: "Matches",
                column: "RefereeId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchsStatistics_MatchId",
                table: "MatchsStatistics",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchsStatistics_TeamId",
                table: "MatchsStatistics",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchsStatistics_Matches_MatchId",
                table: "MatchsStatistics",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchsStatistics_AwayId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchsStatistics_HomeId",
                table: "Matches");

            migrationBuilder.DropTable(
                name: "MatchsStatistics");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Referees");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
