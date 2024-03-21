using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class CreateTaskDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskAssignTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskCreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDetails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskDetails");
        }
    }
}
