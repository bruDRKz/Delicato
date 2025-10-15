using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DelicatoProject.Migrations
{
    /// <inheritdoc />
    public partial class ModelosFinais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avaliacao",
                columns: table => new
                {
                    IdAvaliacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacao", x => x.IdAvaliacao);
                    table.ForeignKey(
                        name: "FK_Avaliacao_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    IdReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    NomeNaReserva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataReserva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraReserva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroPessoas = table.Column<int>(type: "int", nullable: false),
                    Contato = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservaConcluida = table.Column<bool>(type: "bit", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.IdReserva);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardapioBebidas",
                columns: table => new
                {
                    IdBebida = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeBebida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescricaoBebida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    CategoriaIdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardapioBebidas", x => x.IdBebida);
                    table.ForeignKey(
                        name: "FK_CardapioBebidas_Categorias_CategoriaIdCategoria",
                        column: x => x.CategoriaIdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardapioComidas",
                columns: table => new
                {
                    IdComida = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeComida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescricaoComida = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdCategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false),
                    CategoriaIdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardapioComidas", x => x.IdComida);
                    table.ForeignKey(
                        name: "FK_CardapioComidas_Categorias_CategoriaIdCategoria",
                        column: x => x.CategoriaIdCategoria,
                        principalTable: "Categorias",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacao_IdUsuario",
                table: "Avaliacao",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_CardapioBebidas_CategoriaIdCategoria",
                table: "CardapioBebidas",
                column: "CategoriaIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_CardapioComidas_CategoriaIdCategoria",
                table: "CardapioComidas",
                column: "CategoriaIdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_IdUsuario",
                table: "Reservas",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacao");

            migrationBuilder.DropTable(
                name: "CardapioBebidas");

            migrationBuilder.DropTable(
                name: "CardapioComidas");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
