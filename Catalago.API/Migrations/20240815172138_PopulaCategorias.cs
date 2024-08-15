using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalago.API.Migrations
{
    /// <inheritdoc />
    public partial class PopulaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO categories(name, image_url) VALUES ('Bebidas', 'bebidas.jpg')");
            migrationBuilder.Sql("INSERT INTO categories(name, image_url) VALUES ('Lanches', 'lanches.jpg')");
            migrationBuilder.Sql("INSERT INTO categories(name, image_url) VALUES ('Sobremesas', 'sobremesas.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM categories");
        }
    }
}
