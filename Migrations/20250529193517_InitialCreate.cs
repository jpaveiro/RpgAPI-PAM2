using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RpgApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_HABILIDADE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Dano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_HABILIDADE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_USUARIO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    PasswordSalt = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Foto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    DataAcesso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Perfil = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true, defaultValue: "Jogador"),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_USUARIO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_PERSONAGEM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    PontosVida = table.Column<int>(type: "int", nullable: false),
                    Forca = table.Column<int>(type: "int", nullable: false),
                    Defesa = table.Column<int>(type: "int", nullable: false),
                    Inteligencia = table.Column<int>(type: "int", nullable: false),
                    Classe = table.Column<int>(type: "int", nullable: false),
                    FotoPersonagem = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    Disputas = table.Column<int>(type: "int", nullable: false),
                    Vitorias = table.Column<int>(type: "int", nullable: false),
                    Derrotas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_PERSONAGEM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_PERSONAGEM_TBL_USUARIO_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "TBL_USUARIO",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TB_PERSONAGEM_HABILIDADE",
                columns: table => new
                {
                    PersonagemId = table.Column<int>(type: "int", nullable: false),
                    HabilidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_PERSONAGEM_HABILIDADE", x => new { x.PersonagemId, x.HabilidadeId });
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGEM_HABILIDADE_TBL_HABILIDADE_HabilidadeId",
                        column: x => x.HabilidadeId,
                        principalTable: "TBL_HABILIDADE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_PERSONAGEM_HABILIDADE_TBL_PERSONAGEM_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "TBL_PERSONAGEM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ARMA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Dano = table.Column<int>(type: "int", nullable: false),
                    PersonagemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ARMA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_ARMA_TBL_PERSONAGEM_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "TBL_PERSONAGEM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TBL_HABILIDADE",
                columns: new[] { "Id", "Dano", "Nome" },
                values: new object[,]
                {
                    { 1, 39, "Adormecer" },
                    { 2, 41, "Congelar" },
                    { 3, 37, "Hipnotizar" }
                });

            migrationBuilder.InsertData(
                table: "TBL_USUARIO",
                columns: new[] { "Id", "DataAcesso", "Email", "Foto", "Latitude", "Longitude", "PasswordHash", "PasswordSalt", "Perfil", "Username" },
                values: new object[] { 1, null, "seuEmail@gmail.com", null, -23.520024100000001, -46.596497999999997, "hash muito louco", "salt muito louco", "Admin", "UsuarioAdmin" });

            migrationBuilder.InsertData(
                table: "TBL_PERSONAGEM",
                columns: new[] { "Id", "Classe", "Defesa", "Derrotas", "Disputas", "Forca", "FotoPersonagem", "Inteligencia", "Nome", "PontosVida", "UsuarioId", "Vitorias" },
                values: new object[,]
                {
                    { 1, 1, 23, 0, 0, 17, null, 33, "Frodo", 100, 1, 0 },
                    { 2, 1, 25, 0, 0, 15, null, 30, "Sam", 100, 1, 0 },
                    { 3, 3, 21, 0, 0, 18, null, 35, "Galadriel", 100, 1, 0 },
                    { 4, 2, 18, 0, 0, 18, null, 37, "Gandalf", 100, 1, 0 },
                    { 5, 1, 17, 0, 0, 20, null, 31, "Hobbit", 100, 1, 0 },
                    { 6, 3, 13, 0, 0, 21, null, 34, "Celeborn", 102, 1, 0 },
                    { 7, 2, 11, 0, 0, 25, null, 35, "Radagast", 103, 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "TBL_ARMA",
                columns: new[] { "Id", "Dano", "Nome", "PersonagemId" },
                values: new object[,]
                {
                    { 1, 17, "AK-47", 1 },
                    { 2, 53, "Matadora de Dragões", 2 },
                    { 3, 32, "Martelo do Gigante", 3 },
                    { 4, 25, "Presa da Serpente", 4 },
                    { 5, 9, "Luvas com Espinhos", 5 },
                    { 6, 40, "Foice da Perdição", 6 },
                    { 7, 29, "Super Manopla", 7 }
                });

            migrationBuilder.InsertData(
                table: "TB_PERSONAGEM_HABILIDADE",
                columns: new[] { "HabilidadeId", "PersonagemId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 3, 4 },
                    { 1, 5 },
                    { 2, 6 },
                    { 3, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_PERSONAGEM_HABILIDADE_HabilidadeId",
                table: "TB_PERSONAGEM_HABILIDADE",
                column: "HabilidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ARMA_PersonagemId",
                table: "TBL_ARMA",
                column: "PersonagemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PERSONAGEM_UsuarioId",
                table: "TBL_PERSONAGEM",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_PERSONAGEM_HABILIDADE");

            migrationBuilder.DropTable(
                name: "TBL_ARMA");

            migrationBuilder.DropTable(
                name: "TBL_HABILIDADE");

            migrationBuilder.DropTable(
                name: "TBL_PERSONAGEM");

            migrationBuilder.DropTable(
                name: "TBL_USUARIO");
        }
    }
}
