using Microsoft.EntityFrameworkCore.Migrations;

namespace CollegeWebsite.DataAccess.Migrations
{
    public partial class InitialMigrationAddedStaticPagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaticPages",
                columns: table => new
                {
                    PageId = table.Column<string>(nullable: false),
                    PageTitle = table.Column<string>(nullable: false),
                    Controller = table.Column<string>(nullable: false),
                    Action = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    BackgroundImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticPages", x => x.PageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaticPages_PageId",
                table: "StaticPages",
                column: "PageId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaticPages");
        }
    }
}
