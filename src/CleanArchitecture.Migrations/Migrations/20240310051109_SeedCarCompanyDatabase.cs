using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class SeedCarCompanyDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "CarCompanies",
            columns: new[] { "Id", "CarManufactureName" },
            values: new object[] { Guid.NewGuid(), "Maruti Suzuki" });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "CarManufactureName" },
                values: new object[] { Guid.NewGuid(), "Hyundai" });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "CarManufactureName" },
                values: new object[] { Guid.NewGuid(), "Tata Motors" });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "CarManufactureName" },
                values: new object[] { Guid.NewGuid(), "Mahindra" });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "CarManufactureName" },
                values: new object[] { Guid.NewGuid(), "Skoda" });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "CarManufactureName" },
                values: new object[] { Guid.NewGuid(), "Volkswagen" });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "CarManufactureName" },
                values: new object[] { Guid.NewGuid(), "Renault" });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "CarManufactureName" },
                values: new object[] { Guid.NewGuid(), "Toyota" });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "CarManufactureName" },
                values: new object[] { Guid.NewGuid(), "Nissan" });

            migrationBuilder.InsertData(
                table: "CarCompanies",
                columns: new[] { "Id", "CarManufactureName" },
                values: new object[] { Guid.NewGuid(), "Kia" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CarCompanies");
        }
    }
}
