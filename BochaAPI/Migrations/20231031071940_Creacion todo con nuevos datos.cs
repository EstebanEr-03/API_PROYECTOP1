using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BochaAPI.Migrations
{
    /// <inheritdoc />
    public partial class Creaciontodoconnuevosdatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    IdCategoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoriaImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Imagenes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileDescricion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imagenes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    IdMarca = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.IdMarca);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    IdProducto = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    ImagenProductoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdMarca = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCategoria = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.IdProducto);
                });

            migrationBuilder.InsertData(
                table: "Categoria",
                columns: new[] { "IdCategoria", "CategoriaImageURL", "Code", "Nombre" },
                values: new object[,]
                {
                    { new Guid("2ec86d04-967a-408c-83d0-d7d89c1386f6"), "https://freshstreetculture.com/cdn/shop/files/hasbulla1_d677ff1d-cc0f-447d-aa5e-b8339cde6712.jpg?v=1685477923&width=1445", "BSKT", "BASKET" },
                    { new Guid("6ee6ae5c-e2a9-4c11-80b6-9977d3b2837e"), "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.goal.com%2Fen%2Flists%2Flionel-messi-want-make-olynpic-history-paris-2024-invitation-argentina-inter-miami-icon-games-france%2Fbltbc7be24aac1d9483&psig=AOvVaw2GA6GRIX7QwpOn0WxQfCqU&ust=1698706541950000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCLji-pGtnIIDFQAAAAAdAAAAABAE", "TRD", "TREND" },
                    { new Guid("7f994b7e-7eb2-45c4-8a13-80bdc9afd229"), "https://viapais.com.ar/resizer/fhAUasx4qvTMGOwvqChc_1H7Urk=/1080x1350/smart/cloudfront-us-east-1.images.arcpublishing.com/grupoclarin/6LU6B2QP4NDAHIJFTJGKS7NDKI.jpg", "FTB", "FUTBOL" }
                });

            migrationBuilder.InsertData(
                table: "Marca",
                columns: new[] { "IdMarca", "Nombre" },
                values: new object[,]
                {
                    { new Guid("3d8a684f-75f1-4dce-86d0-e4681e9c1859"), "Puma" },
                    { new Guid("7770a7c2-ef67-4852-8f96-49235cba1515"), "Nike" },
                    { new Guid("ce67dfa5-2908-406d-89e3-d8aaecb68cef"), "Adidas" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Imagenes");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "Producto");
        }
    }
}
