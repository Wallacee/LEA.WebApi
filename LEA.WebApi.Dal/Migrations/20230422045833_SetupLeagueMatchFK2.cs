using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LEA.WebApi.Dal.Migrations
{
    public partial class SetupLeagueMatchFK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 53, DateTimeKind.Local).AddTicks(8702),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 53, DateTimeKind.Local).AddTicks(8456),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6591));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchesStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 53, DateTimeKind.Local).AddTicks(8243),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6401));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 53, DateTimeKind.Local).AddTicks(7592),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(5783));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 48, DateTimeKind.Local).AddTicks(1701),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 91, DateTimeKind.Local).AddTicks(9813));

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                column: "LeagueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6732),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 53, DateTimeKind.Local).AddTicks(8702));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 53, DateTimeKind.Local).AddTicks(8456));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchesStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6401),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 53, DateTimeKind.Local).AddTicks(8243));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(5783),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 53, DateTimeKind.Local).AddTicks(7592));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 91, DateTimeKind.Local).AddTicks(9813),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 58, 33, 48, DateTimeKind.Local).AddTicks(1701));

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                column: "LeagueId",
                unique: true);
        }
    }
}
