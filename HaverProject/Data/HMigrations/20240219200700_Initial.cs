using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HaverProject.Data.HMigrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ncrs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NCRNumber = table.Column<string>(type: "TEXT", nullable: false),
                    PONumber = table.Column<long>(type: "INTEGER", nullable: false),
                    SupplierOrRecInsp = table.Column<bool>(type: "INTEGER", nullable: false),
                    WIP = table.Column<bool>(type: "INTEGER", nullable: false),
                    Supplier = table.Column<string>(type: "TEXT", nullable: false),
                    QuantityReceived = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantityDefected = table.Column<int>(type: "INTEGER", nullable: false),
                    DescriptionItem = table.Column<string>(type: "TEXT", nullable: false),
                    DescriptionDefect = table.Column<string>(type: "TEXT", nullable: false),
                    Yes = table.Column<bool>(type: "INTEGER", nullable: false),
                    No = table.Column<bool>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    RepresentativesName = table.Column<string>(type: "TEXT", nullable: false),
                    UseAsIs = table.Column<bool>(type: "INTEGER", nullable: false),
                    Repair = table.Column<bool>(type: "INTEGER", nullable: false),
                    Rework = table.Column<bool>(type: "INTEGER", nullable: false),
                    Scrap = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomerYes = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomerNO = table.Column<bool>(type: "INTEGER", nullable: false),
                    Disposition = table.Column<string>(type: "TEXT", nullable: false),
                    DrawingYes = table.Column<bool>(type: "INTEGER", nullable: false),
                    DrawingNo = table.Column<bool>(type: "INTEGER", nullable: false),
                    OriginalRev = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedRev = table.Column<string>(type: "TEXT", nullable: false),
                    NameOfEngineer = table.Column<string>(type: "TEXT", nullable: false),
                    RevisingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Engineer = table.Column<string>(type: "TEXT", nullable: false),
                    EngineerDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReturntoSupplier = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReworkInHouse = table.Column<bool>(type: "INTEGER", nullable: false),
                    ScrapinHouse = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeferforHBC = table.Column<bool>(type: "INTEGER", nullable: false),
                    PreliminaryDecision = table.Column<string>(type: "TEXT", nullable: false),
                    CARYes = table.Column<bool>(type: "INTEGER", nullable: false),
                    CARNo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FollowupYes = table.Column<bool>(type: "INTEGER", nullable: false),
                    FollowupNo = table.Column<bool>(type: "INTEGER", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    OperatingManagerName = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ncrs", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ncrs");
        }
    }
}
