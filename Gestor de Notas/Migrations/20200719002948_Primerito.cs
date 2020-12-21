using Microsoft.EntityFrameworkCore.Migrations;

namespace Gestor_de_Notas.Migrations
{
    public partial class Primerito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciclo",
                columns: table => new
                {
                    CicloId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CicloCantidadCursos = table.Column<int>(nullable: false),
                    CicloPromedio = table.Column<float>(nullable: false),
                    CicloPromedioBeca = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciclo", x => x.CicloId);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    CursoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoCodigo = table.Column<string>(nullable: false),
                    CursoNombre = table.Column<string>(nullable: false),
                    CursoCreditos = table.Column<int>(nullable: false),
                    CursoCantidadCampos = table.Column<int>(nullable: false),
                    CursoPromedio = table.Column<float>(nullable: false),
                    CursoVez = table.Column<int>(nullable: false),
                    CicloId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.CursoId);
                    table.ForeignKey(
                        name: "FK_Curso_Ciclo_CicloId",
                        column: x => x.CicloId,
                        principalTable: "Ciclo",
                        principalColumn: "CicloId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campo",
                columns: table => new
                {
                    CampoTipo = table.Column<string>(nullable: false),
                    CampoNumero = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    CampoDescripcion = table.Column<string>(nullable: false),
                    CampoPeso = table.Column<float>(nullable: false),
                    CampoNota = table.Column<float>(nullable: false),
                    CampoRellenado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campo", x => new { x.CampoTipo, x.CampoNumero, x.CursoId });
                    table.ForeignKey(
                        name: "FK_Campo_Curso_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Curso",
                        principalColumn: "CursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campo_CursoId",
                table: "Campo",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_CicloId",
                table: "Curso",
                column: "CicloId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campo");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "Ciclo");
        }
    }
}
