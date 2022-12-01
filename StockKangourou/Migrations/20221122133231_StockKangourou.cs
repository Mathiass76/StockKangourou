using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockKangourou.Migrations
{
    public partial class StockKangourou : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kangourous",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codeKangourou = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nomKangourou = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    raceKangourou = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tailleKangourou = table.Column<double>(type: "float", nullable: true),
                    poidsKangourou = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kangourous", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kangourous");
        }
    }
}
