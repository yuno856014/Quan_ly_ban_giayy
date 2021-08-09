using Microsoft.EntityFrameworkCore.Migrations;

namespace SneakerStoree.Migrations
{
    public partial class CreateDBSneaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradeMarks",
                columns: table => new
                {
                    TradeMarkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TradeMarkName = table.Column<string>(maxLength: 250, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeMarks", x => x.TradeMarkId);
                });

            migrationBuilder.CreateTable(
                name: "Sneakers",
                columns: table => new
                {
                    SneakerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SneakerName = table.Column<string>(maxLength: 500, nullable: false),
                    PublishYear = table.Column<int>(nullable: false),
                    Photo = table.Column<string>(maxLength: 300, nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Information = table.Column<string>(maxLength: 3000, nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TradeMarkId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sneakers", x => x.SneakerId);
                    table.ForeignKey(
                        name: "FK_Sneakers_TradeMarks_TradeMarkId",
                        column: x => x.TradeMarkId,
                        principalTable: "TradeMarks",
                        principalColumn: "TradeMarkId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TradeMarks",
                columns: new[] { "TradeMarkId", "IsDeleted", "TradeMarkName" },
                values: new object[,]
                {
                    { 1, false, "Converse" },
                    { 2, false, "Nike" },
                    { 3, false, "Adidas" },
                    { 4, false, "Vans" }
                });

            migrationBuilder.InsertData(
                table: "Sneakers",
                columns: new[] { "SneakerId", "Information", "IsDeleted", "Photo", "Price", "PublishYear", "Quantity", "Size", "SneakerName", "TradeMarkId" },
                values: new object[] { 2, "Giày Đẹp", false, "images/converse-w.jpg", 715000, 2019, 10, 42, "Converse 1970s Chuck Taylor 2", 1 });

            migrationBuilder.InsertData(
                table: "Sneakers",
                columns: new[] { "SneakerId", "Information", "IsDeleted", "Photo", "Price", "PublishYear", "Quantity", "Size", "SneakerName", "TradeMarkId" },
                values: new object[] { 1, "Giày Đẹp", false, "images/vans_b-w.jpg", 1350000, 2019, 10, 42, "Vans Old Skool Black White", 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Sneakers_TradeMarkId",
                table: "Sneakers",
                column: "TradeMarkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sneakers");

            migrationBuilder.DropTable(
                name: "TradeMarks");
        }
    }
}
