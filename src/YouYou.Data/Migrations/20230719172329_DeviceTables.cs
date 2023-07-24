using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouYou.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeviceTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "PhysicalPersons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "varchar(45)", nullable: false),
                    Code = table.Column<string>(type: "varchar(256)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeviceHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BindingDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UnbindingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceHistories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeviceHistories_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeviceHistories_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHistories_DeviceId",
                table: "DeviceHistories",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHistories_PersonId",
                table: "DeviceHistories",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHistories_UserId",
                table: "DeviceHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_PersonId",
                table: "Devices",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceHistories");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "PhysicalPersons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
