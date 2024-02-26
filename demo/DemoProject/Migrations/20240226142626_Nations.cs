using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoProject.Migrations
{
    /// <inheritdoc />
    public partial class Nations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NationId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Nations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nations", x => x.Id);
                });
            
            migrationBuilder.InsertData(
                table: "Nations",
                columns: new[] { "Name" },
                values: new object[,]
                {
                    { "Fire Nation" },
                    { "Air Nomads" },
                    { "Water Tribe" },
                    { "Earth Kingdom" }
                });
            
            migrationBuilder.UpdateData(
                table: "Characters", 
                keyColumn: "Id", 
                keyValue: 1, 
                column: "NationId", 
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_NationId",
                table: "Characters",
                column: "NationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Nations_NationId",
                table: "Characters",
                column: "NationId",
                principalTable: "Nations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Nations_NationId",
                table: "Characters");

            migrationBuilder.DropTable(
                name: "Nations");

            migrationBuilder.DropIndex(
                name: "IX_Characters_NationId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "NationId",
                table: "Characters");
        }
    }
}
