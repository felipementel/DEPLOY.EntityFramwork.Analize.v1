using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DEPLOY.EntityFramework.v1.Migrations.EF
{
    /// <inheritdoc />
    public partial class InitDatabaseCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    ContratoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<long>(type: "bigint", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.ContratoId);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    PessoaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.PessoaId);
                });

            migrationBuilder.CreateTable(
                name: "ContratoPessoa",
                columns: table => new
                {
                    ContratosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InteressadosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContratoPessoa", x => new { x.ContratosId, x.InteressadosId });
                    table.ForeignKey(
                        name: "FK_ContratoPessoa_Contratos_ContratosId",
                        column: x => x.ContratosId,
                        principalTable: "Contratos",
                        principalColumn: "ContratoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContratoPessoa_Pessoas_InteressadosId",
                        column: x => x.InteressadosId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContratoPessoa_InteressadosId",
                table: "ContratoPessoa",
                column: "InteressadosId");

            migrationBuilder.CreateIndex(
                name: "IX_Names_Descending",
                table: "Pessoas",
                columns: new[] { "Endereco", "Documento" },
                descending: new bool[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContratoPessoa");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
