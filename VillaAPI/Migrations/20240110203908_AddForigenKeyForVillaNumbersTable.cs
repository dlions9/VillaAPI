using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForigenKeyForVillaNumbersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VNumber",
                table: "villaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 15, 39, 7, 831, DateTimeKind.Local).AddTicks(3938));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 15, 39, 7, 831, DateTimeKind.Local).AddTicks(3996));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 15, 39, 7, 831, DateTimeKind.Local).AddTicks(3999));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 15, 39, 7, 831, DateTimeKind.Local).AddTicks(4002));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 15, 39, 7, 831, DateTimeKind.Local).AddTicks(4005));

            migrationBuilder.UpdateData(
                table: "villaNumbers",
                keyColumn: "VillaNo",
                keyValue: 101,
                columns: new[] { "CreatedDate", "VNumber" },
                values: new object[] { new DateTime(2024, 1, 10, 15, 39, 7, 831, DateTimeKind.Local).AddTicks(4163), 0 });

            migrationBuilder.CreateIndex(
                name: "IX_villaNumbers_VNumber",
                table: "villaNumbers",
                column: "VNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_villaNumbers_Villas_VNumber",
                table: "villaNumbers",
                column: "VNumber",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_villaNumbers_Villas_VNumber",
                table: "villaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_villaNumbers_VNumber",
                table: "villaNumbers");

            migrationBuilder.DropColumn(
                name: "VNumber",
                table: "villaNumbers");

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

            migrationBuilder.UpdateData(
                table: "villaNumbers",
                keyColumn: "VillaNo",
                keyValue: 101,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 11, 13, 12, 626, DateTimeKind.Local).AddTicks(3000));
        }
    }
}
