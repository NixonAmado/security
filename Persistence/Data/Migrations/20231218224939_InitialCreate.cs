using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.CreateTable(
                name: "categoriaPer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "estado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "pais",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "tipoContacto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "tipoDireccion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "tipoPersona",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "turno",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    nombre_turno = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    hora_turno_inicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    hora_turno_fin = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "departamento",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    id_pais = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_pais",
                        column: x => x.id_pais,
                        principalTable: "pais",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "ciudad",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    id_departamento = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_departamento",
                        column: x => x.id_departamento,
                        principalTable: "departamento",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    id_persona = table.Column<int>(type: "int(11)", nullable: false),
                    nombre = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    fechaRegistro = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_tipo_persona = table.Column<int>(type: "int(11)", nullable: false),
                    id_categoria = table.Column<int>(type: "int(11)", nullable: false),
                    id_ciudad = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_categoria_persona",
                        column: x => x.id_categoria,
                        principalTable: "categoriaPer",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_ciudad_persona",
                        column: x => x.id_ciudad,
                        principalTable: "ciudad",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tipo_persona_persona",
                        column: x => x.id_tipo_persona,
                        principalTable: "tipoPersona",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "contactoPer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    id_tipo_contacto = table.Column<int>(type: "int(11)", nullable: false),
                    id_persona = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_persona_contacto",
                        column: x => x.id_persona,
                        principalTable: "persona",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tipo_contacto_contacto",
                        column: x => x.id_tipo_contacto,
                        principalTable: "tipoContacto",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "contrato",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    id_cliente = table.Column<int>(type: "int(11)", nullable: false),
                    fecha_contrato = table.Column<DateOnly>(type: "date", nullable: false),
                    fecha_fin = table.Column<DateOnly>(type: "date", nullable: false),
                    id_empleado = table.Column<int>(type: "int(11)", nullable: false),
                    id_estado = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_cliente_contrato",
                        column: x => x.id_cliente,
                        principalTable: "persona",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_empleado_contrato",
                        column: x => x.id_empleado,
                        principalTable: "persona",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_estado_contrato",
                        column: x => x.id_estado,
                        principalTable: "estado",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "dirPersona",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    id_tipo_direccion = table.Column<int>(type: "int(11)", nullable: false),
                    id_persona = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_persona_direccion",
                        column: x => x.id_persona,
                        principalTable: "persona",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tipo_direccion_direccion",
                        column: x => x.id_tipo_direccion,
                        principalTable: "tipoDireccion",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "programacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false),
                    id_contrato = table.Column<int>(type: "int(11)", nullable: false),
                    id_turno = table.Column<int>(type: "int(11)", nullable: false),
                    id_empleado = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_contrato_programacion",
                        column: x => x.id_contrato,
                        principalTable: "contrato",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_persona_programacion",
                        column: x => x.id_empleado,
                        principalTable: "persona",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_turno_programacion",
                        column: x => x.id_turno,
                        principalTable: "turno",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_departamento",
                table: "ciudad",
                column: "id_departamento");

            migrationBuilder.CreateIndex(
                name: "fk_persona_contacto",
                table: "contactoPer",
                column: "id_persona");

            migrationBuilder.CreateIndex(
                name: "fk_tipo_contacto_contacto",
                table: "contactoPer",
                column: "id_tipo_contacto");

            migrationBuilder.CreateIndex(
                name: "fk_cliente_contrato",
                table: "contrato",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "fk_empleado_contrato",
                table: "contrato",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "fk_estado_contrato",
                table: "contrato",
                column: "id_estado");

            migrationBuilder.CreateIndex(
                name: "fk_pais",
                table: "departamento",
                column: "id_pais");

            migrationBuilder.CreateIndex(
                name: "fk_persona_direccion",
                table: "dirPersona",
                column: "id_persona");

            migrationBuilder.CreateIndex(
                name: "fk_tipo_direccion_direccion",
                table: "dirPersona",
                column: "id_tipo_direccion");

            migrationBuilder.CreateIndex(
                name: "fk_categoria_persona",
                table: "persona",
                column: "id_categoria");

            migrationBuilder.CreateIndex(
                name: "fk_ciudad_persona",
                table: "persona",
                column: "id_ciudad");

            migrationBuilder.CreateIndex(
                name: "fk_tipo_persona_persona",
                table: "persona",
                column: "id_tipo_persona");

            migrationBuilder.CreateIndex(
                name: "id_persona",
                table: "persona",
                column: "id_persona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_contrato_programacion",
                table: "programacion",
                column: "id_contrato");

            migrationBuilder.CreateIndex(
                name: "fk_persona_programacion",
                table: "programacion",
                column: "id_empleado");

            migrationBuilder.CreateIndex(
                name: "fk_turno_programacion",
                table: "programacion",
                column: "id_turno");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contactoPer");

            migrationBuilder.DropTable(
                name: "dirPersona");

            migrationBuilder.DropTable(
                name: "programacion");

            migrationBuilder.DropTable(
                name: "tipoContacto");

            migrationBuilder.DropTable(
                name: "tipoDireccion");

            migrationBuilder.DropTable(
                name: "contrato");

            migrationBuilder.DropTable(
                name: "turno");

            migrationBuilder.DropTable(
                name: "persona");

            migrationBuilder.DropTable(
                name: "estado");

            migrationBuilder.DropTable(
                name: "categoriaPer");

            migrationBuilder.DropTable(
                name: "ciudad");

            migrationBuilder.DropTable(
                name: "tipoPersona");

            migrationBuilder.DropTable(
                name: "departamento");

            migrationBuilder.DropTable(
                name: "pais");
        }
    }
}
