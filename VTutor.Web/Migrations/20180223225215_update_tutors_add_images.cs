using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VTutor.Web.Migrations
{
    public partial class update_tutors_add_images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tutors",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Tutors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Tutors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Tutors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hobbies",
                table: "Tutors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "Tutors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Tutors",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TutorImageId",
                table: "Tutors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImageData = table.Column<byte[]>(nullable: true),
                    TutorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tutors_TutorImageId",
                table: "Tutors",
                column: "TutorImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_TutorId",
                table: "Image",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tutors_Image_TutorImageId",
                table: "Tutors",
                column: "TutorImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutors_Image_TutorImageId",
                table: "Tutors");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Tutors_TutorImageId",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "Hobbies",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Tutors");

            migrationBuilder.DropColumn(
                name: "TutorImageId",
                table: "Tutors");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Tutors",
                newName: "Name");
        }
    }
}
