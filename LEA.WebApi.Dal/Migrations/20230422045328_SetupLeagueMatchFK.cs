using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LEA.WebApi.Dal.Migrations
{
    public partial class SetupLeagueMatchFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Leagues_LeagueId",
                table: "Matches");

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
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 659, DateTimeKind.Local).AddTicks(510));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6591),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 659, DateTimeKind.Local).AddTicks(289));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchesStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6401),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 658, DateTimeKind.Local).AddTicks(9890));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(5783),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 658, DateTimeKind.Local).AddTicks(8804));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 91, DateTimeKind.Local).AddTicks(9813),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 649, DateTimeKind.Local).AddTicks(5949));

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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 659, DateTimeKind.Local).AddTicks(510),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 659, DateTimeKind.Local).AddTicks(289),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6591));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchesStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 658, DateTimeKind.Local).AddTicks(9890),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(6401));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 658, DateTimeKind.Local).AddTicks(8804),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 96, DateTimeKind.Local).AddTicks(5783));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 4, 22, 1, 20, 23, 649, DateTimeKind.Local).AddTicks(5949),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 4, 22, 1, 53, 28, 91, DateTimeKind.Local).AddTicks(9813));

            migrationBuilder.CreateIndex(
                name: "IX_Matches_LeagueId",
                table: "Matches",
                column: "LeagueId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Leagues_LeagueId",
                table: "Matches",
                column: "LeagueId",
                principalTable: "Leagues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
