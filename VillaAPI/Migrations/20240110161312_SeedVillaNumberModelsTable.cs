using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaNumberModelsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 11, 13, 12, 626, DateTimeKind.Local).AddTicks(2782));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 11, 13, 12, 626, DateTimeKind.Local).AddTicks(2841));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 11, 13, 12, 626, DateTimeKind.Local).AddTicks(2843));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 11, 13, 12, 626, DateTimeKind.Local).AddTicks(2846));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 11, 13, 12, 626, DateTimeKind.Local).AddTicks(2848));

            migrationBuilder.InsertData(
                table: "villaNumbers",
                columns: new[] { "VillaNo", "CreatedDate", "SpecialDetails", "UpdatedDate" },
                values: new object[] { 101, new DateTime(2024, 1, 10, 11, 13, 12, 626, DateTimeKind.Local).AddTicks(3000), "has lyft facility. Convineint for Handicapped users", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "villaNumbers",
                keyColumn: "VillaNo",
                keyValue: 101);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 10, 17, 26, 78, DateTimeKind.Local).AddTicks(9115));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 10, 17, 26, 78, DateTimeKind.Local).AddTicks(9172));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 10, 17, 26, 78, DateTimeKind.Local).AddTicks(9175));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 10, 17, 26, 78, DateTimeKind.Local).AddTicks(9178));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 10, 17, 26, 78, DateTimeKind.Local).AddTicks(9181));
        }
    }
}
