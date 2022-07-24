using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LEA.WebApi.Dal.Migrations
{
    public partial class SetupNullableRefereeIdMatch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Referees_RefereeId",
                table: "Matches");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7987),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 392, DateTimeKind.Local).AddTicks(4299));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7183),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 392, DateTimeKind.Local).AddTicks(3965));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchesStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(6485),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 392, DateTimeKind.Local).AddTicks(3550));

            migrationBuilder.AlterColumn<int>(
                name: "RefereeId",
                table: "Matches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(4628),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 392, DateTimeKind.Local).AddTicks(1957));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 869, DateTimeKind.Local).AddTicks(419),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 381, DateTimeKind.Local).AddTicks(635));

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Referees_RefereeId",
                table: "Matches",
                column: "RefereeId",
                principalTable: "Referees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Referees_RefereeId",
                table: "Matches");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Teams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 392, DateTimeKind.Local).AddTicks(4299),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7987));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Referees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 392, DateTimeKind.Local).AddTicks(3965),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(7183));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "MatchesStatistics",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 392, DateTimeKind.Local).AddTicks(3550),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(6485));

            migrationBuilder.AlterColumn<int>(
                name: "RefereeId",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Matches",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 392, DateTimeKind.Local).AddTicks(1957),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 887, DateTimeKind.Local).AddTicks(4628));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Creation",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 21, 1, 16, 47, 381, DateTimeKind.Local).AddTicks(635),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 7, 19, 0, 32, 13, 869, DateTimeKind.Local).AddTicks(419));

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Referees_RefereeId",
                table: "Matches",
                column: "RefereeId",
                principalTable: "Referees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
