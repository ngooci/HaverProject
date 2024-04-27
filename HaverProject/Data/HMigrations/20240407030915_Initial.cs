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
                name: "emailAddresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailAddresses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ItemMarkeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 255, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMarkeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemProblems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 255, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemProblems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 255, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SapNos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 255, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SapNos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrRecInsps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 255, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrRecInsps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 255, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UseAsIss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", maxLength: 255, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseAsIss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ncrs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NCRNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PONumber = table.Column<long>(type: "INTEGER", nullable: true),
                    SupplierOrRecInspID = table.Column<int>(type: "INTEGER", nullable: false),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: true),
                    QuantityReceived = table.Column<int>(type: "INTEGER", nullable: true),
                    QuantityDefected = table.Column<int>(type: "INTEGER", nullable: true),
                    SapId = table.Column<string>(type: "TEXT", nullable: true),
                    SapNoId = table.Column<int>(type: "INTEGER", nullable: true),
                    DescriptionItemID = table.Column<string>(type: "TEXT", nullable: true),
                    ItemProblemId = table.Column<int>(type: "INTEGER", nullable: true),
                    DescriptionDefect = table.Column<string>(type: "TEXT", nullable: true),
                    ItemMarkedID = table.Column<int>(type: "INTEGER", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    RepresentativesName = table.Column<string>(type: "TEXT", nullable: true),
                    UseAsIsId = table.Column<int>(type: "INTEGER", nullable: true),
                    CustomerYes = table.Column<bool>(type: "INTEGER", nullable: false),
                    CustomerNO = table.Column<bool>(type: "INTEGER", nullable: false),
                    Disposition = table.Column<string>(type: "TEXT", nullable: true),
                    DrawingYes = table.Column<bool>(type: "INTEGER", nullable: false),
                    DrawingNo = table.Column<bool>(type: "INTEGER", nullable: false),
                    OriginalRev = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedRev = table.Column<string>(type: "TEXT", nullable: true),
                    NameOfEngineer = table.Column<string>(type: "TEXT", nullable: true),
                    RevisingDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Engineer = table.Column<string>(type: "TEXT", nullable: true),
                    EngineerDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    reviewyes = table.Column<bool>(type: "INTEGER", nullable: false),
                    reviewno = table.Column<bool>(type: "INTEGER", nullable: false),
                    PreliminaryDecision = table.Column<string>(type: "TEXT", nullable: true),
                    CARYes = table.Column<bool>(type: "INTEGER", nullable: false),
                    CARNo = table.Column<bool>(type: "INTEGER", nullable: false),
                    FollowupYes = table.Column<bool>(type: "INTEGER", nullable: false),
                    FollowupNo = table.Column<bool>(type: "INTEGER", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OperatingManagerName = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Video = table.Column<string>(type: "TEXT", nullable: true),
                    ncrEmail = table.Column<int>(type: "INTEGER", nullable: true),
                    reinyes = table.Column<bool>(type: "INTEGER", nullable: false),
                    reino = table.Column<bool>(type: "INTEGER", nullable: false),
                    ncrclosenyes = table.Column<bool>(type: "INTEGER", nullable: false),
                    ncrcloseno = table.Column<bool>(type: "INTEGER", nullable: false),
                    newNcrnno = table.Column<int>(type: "INTEGER", nullable: true),
                    InspectorName = table.Column<string>(type: "TEXT", nullable: true),
                    reviewDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NcrClosed = table.Column<string>(type: "TEXT", nullable: true),
                    Qualitydepartment = table.Column<string>(type: "TEXT", nullable: true),
                    finalDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    VoidReason = table.Column<string>(type: "TEXT", nullable: true),
                    ReviewId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ncrs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ncrs_ItemMarkeds_ItemMarkedID",
                        column: x => x.ItemMarkedID,
                        principalTable: "ItemMarkeds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ncrs_ItemProblems_ItemProblemId",
                        column: x => x.ItemProblemId,
                        principalTable: "ItemProblems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ncrs_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ncrs_SapNos_SapNoId",
                        column: x => x.SapNoId,
                        principalTable: "SapNos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ncrs_SupplierOrRecInsps_SupplierOrRecInspID",
                        column: x => x.SupplierOrRecInspID,
                        principalTable: "SupplierOrRecInsps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ncrs_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ncrs_UseAsIss_UseAsIsId",
                        column: x => x.UseAsIsId,
                        principalTable: "UseAsIss",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "nCRComments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NCRId = table.Column<int>(type: "INTEGER", nullable: false),
                    CommentText = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nCRComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_nCRComments_Ncrs_NCRId",
                        column: x => x.NCRId,
                        principalTable: "Ncrs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NCRPhotos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: true),
                    MimeType = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    NCRID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NCRPhotos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NCRPhotos_Ncrs_NCRID",
                        column: x => x.NCRID,
                        principalTable: "Ncrs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_nCRComments_NCRId",
                table: "nCRComments",
                column: "NCRId");

            migrationBuilder.CreateIndex(
                name: "IX_NCRPhotos_NCRID",
                table: "NCRPhotos",
                column: "NCRID");

            migrationBuilder.CreateIndex(
                name: "IX_Ncrs_ItemMarkedID",
                table: "Ncrs",
                column: "ItemMarkedID");

            migrationBuilder.CreateIndex(
                name: "IX_Ncrs_ItemProblemId",
                table: "Ncrs",
                column: "ItemProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ncrs_ReviewId",
                table: "Ncrs",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Ncrs_SapNoId",
                table: "Ncrs",
                column: "SapNoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ncrs_SupplierId",
                table: "Ncrs",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Ncrs_SupplierOrRecInspID",
                table: "Ncrs",
                column: "SupplierOrRecInspID");

            migrationBuilder.CreateIndex(
                name: "IX_Ncrs_UseAsIsId",
                table: "Ncrs",
                column: "UseAsIsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emailAddresses");

            migrationBuilder.DropTable(
                name: "nCRComments");

            migrationBuilder.DropTable(
                name: "NCRPhotos");

            migrationBuilder.DropTable(
                name: "Ncrs");

            migrationBuilder.DropTable(
                name: "ItemMarkeds");

            migrationBuilder.DropTable(
                name: "ItemProblems");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "SapNos");

            migrationBuilder.DropTable(
                name: "SupplierOrRecInsps");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "UseAsIss");
        }
    }
}
