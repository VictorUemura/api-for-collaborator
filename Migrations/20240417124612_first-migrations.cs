using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class firstmigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DataNasc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Telefone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataDeCriacao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DataDeAlteracao = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    IdColaborador = table.Column<long>(type: "bigint", nullable: false),
                    Arquivo = table.Column<byte[]>(type: "longblob", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
            migrationBuilder.AddForeignKey(
                name: "FK_Documentos_Colaboradores",
                table: "Documentos",
                column: "IdColaborador",
                principalTable: "Colaboradores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentos_Colaboradores",
                table: "Documentos");

            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "Documentos");
        }
    }
}
