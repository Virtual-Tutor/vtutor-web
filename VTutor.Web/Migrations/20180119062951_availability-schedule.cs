using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VTutor.Web.Migrations
{
    public partial class availabilityschedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "ScheduleBlocks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "ScheduleBlocks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TutorId",
                table: "ScheduleBlocks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleBlocks_TutorId",
                table: "ScheduleBlocks",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleBlocks_Tutors_TutorId",
                table: "ScheduleBlocks",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleBlocks_Tutors_TutorId",
                table: "ScheduleBlocks");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleBlocks_TutorId",
                table: "ScheduleBlocks");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ScheduleBlocks");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "ScheduleBlocks");

            migrationBuilder.DropColumn(
                name: "TutorId",
                table: "ScheduleBlocks");
        }
    }
}
