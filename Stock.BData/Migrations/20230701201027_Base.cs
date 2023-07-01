using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock.BData.Migrations
{
    /// <inheritdoc />
    public partial class Base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProductos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreProducto = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PrecioProducto = table.Column<decimal>(type: "Decimal(10,2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.IdProductos);
                });

            migrationBuilder.CreateTable(
                name: "Informes",
                columns: table => new
                {
                    IdProd = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    IdProductos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Informes", x => x.IdProd);
                    table.ForeignKey(
                        name: "FK_Informes_Producto_IdProductos",
                        column: x => x.IdProductos,
                        principalTable: "Producto",
                        principalColumn: "IdProductos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "Informe_IdProd_UQ",
                table: "Informes",
                column: "IdProd",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Informes_IdProductos",
                table: "Informes",
                column: "IdProductos");

            migrationBuilder.CreateIndex(
                name: "Productos_IdProductos_UQ",
                table: "Producto",
                column: "IdProductos",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Informes");

            migrationBuilder.DropTable(
                name: "Producto");
        }
    }
}
