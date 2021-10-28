using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LEA.WebApi.Dal.Migrations
{
    public partial class SetupMatchStatisticsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchsStatistics_AwayId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchsStatistics_HomeId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchsStatistics_Matches_MatchId",
                table: "MatchsStatistics");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchsStatistics_Teams_TeamId",
                table: "MatchsStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchsStatistics",
                table: "MatchsStatistics");

            migrationBuilder.RenameTable(
                name: "MatchsStatistics",
                newName: "MatchesStatistics");

            migrationBuilder.RenameIndex(
                name: "IX_MatchsStatistics_TeamId",
                table: "MatchesStatistics",
                newName: "IX_MatchesStatistics_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchsStatistics_MatchId",
                table: "MatchesStatistics",
                newName: "IX_MatchesStatistics_MatchId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 968, DateTimeKind.Local).AddTicks(6801),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(8168));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 968, DateTimeKind.Local).AddTicks(6617),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(7959));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 968, DateTimeKind.Local).AddTicks(5580),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(6725));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 963, DateTimeKind.Local).AddTicks(3515),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 400, DateTimeKind.Local).AddTicks(5106));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchesStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 968, DateTimeKind.Local).AddTicks(6345),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(7649));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchesStatistics",
                table: "MatchesStatistics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchesStatistics_AwayId",
                table: "Matches",
                column: "AwayId",
                principalTable: "MatchesStatistics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchesStatistics_HomeId",
                table: "Matches",
                column: "HomeId",
                principalTable: "MatchesStatistics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchesStatistics_Matches_MatchId",
                table: "MatchesStatistics",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchesStatistics_Teams_TeamId",
                table: "MatchesStatistics",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchesStatistics_AwayId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_MatchesStatistics_HomeId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchesStatistics_Matches_MatchId",
                table: "MatchesStatistics");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchesStatistics_Teams_TeamId",
                table: "MatchesStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchesStatistics",
                table: "MatchesStatistics");

            migrationBuilder.RenameTable(
                name: "MatchesStatistics",
                newName: "MatchsStatistics");

            migrationBuilder.RenameIndex(
                name: "IX_MatchesStatistics_TeamId",
                table: "MatchsStatistics",
                newName: "IX_MatchsStatistics_TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchesStatistics_MatchId",
                table: "MatchsStatistics",
                newName: "IX_MatchsStatistics_MatchId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(8168),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 968, DateTimeKind.Local).AddTicks(6801));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(7959),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 968, DateTimeKind.Local).AddTicks(6617));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(6725),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 968, DateTimeKind.Local).AddTicks(5580));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 400, DateTimeKind.Local).AddTicks(5106),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 963, DateTimeKind.Local).AddTicks(3515));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchsStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2021, 10, 28, 0, 18, 47, 409, DateTimeKind.Local).AddTicks(7649),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2021, 10, 28, 0, 25, 7, 968, DateTimeKind.Local).AddTicks(6345));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchsStatistics",
                table: "MatchsStatistics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchsStatistics_AwayId",
                table: "Matches",
                column: "AwayId",
                principalTable: "MatchsStatistics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_MatchsStatistics_HomeId",
                table: "Matches",
                column: "HomeId",
                principalTable: "MatchsStatistics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchsStatistics_Matches_MatchId",
                table: "MatchsStatistics",
                column: "MatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchsStatistics_Teams_TeamId",
                table: "MatchsStatistics",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
