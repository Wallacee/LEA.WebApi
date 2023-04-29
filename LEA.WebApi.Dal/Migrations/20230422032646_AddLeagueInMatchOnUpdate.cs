using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LEA.WebApi.Dal.Migrations
{
    public partial class AddLeagueInMatchOnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 725, DateTimeKind.Local).AddTicks(1763),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7987));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 725, DateTimeKind.Local).AddTicks(1537),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchesStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 725, DateTimeKind.Local).AddTicks(1338),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 725, DateTimeKind.Local).AddTicks(762),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(4628));

            migrationBuilder.AddColumn<int>(
                name: "LeagueId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 718, DateTimeKind.Local).AddTicks(4211),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 869, DateTimeKind.Local).AddTicks(419));

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                column: "LeagueId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Leagues_LeagueId",
                table: "Matches",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Leagues_LeagueId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "LeagueId",
                table: "Matches");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7987),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 725, DateTimeKind.Local).AddTicks(1763));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 725, DateTimeKind.Local).AddTicks(1537));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchesStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(6485),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 725, DateTimeKind.Local).AddTicks(1338));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(4628),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 725, DateTimeKind.Local).AddTicks(762));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 869, DateTimeKind.Local).AddTicks(419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 0, 26, 45, 718, DateTimeKind.Local).AddTicks(4211));
        }
    }
}
