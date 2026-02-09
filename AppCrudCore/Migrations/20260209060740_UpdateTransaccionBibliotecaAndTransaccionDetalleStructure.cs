using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCrudCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransaccionBibliotecaAndTransaccionDetalleStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaRegistro",
                table: "Cliente",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.CreateTable(
                name: "TransaccionesBiblioteca",
                columns: table => new
                {
                    IdTransaccionBiblioteca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    EmpleadoId = table.Column<int>(type: "int", nullable: true),
                    TipoServicio = table.Column<int>(type: "int", nullable: false),
                    Origen = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCompletada = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionesBiblioteca", x => x.IdTransaccionBiblioteca);
                    table.ForeignKey(
                        name: "FK_TransaccionesBiblioteca_Cliente_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionesBiblioteca_Empleado_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado");
                });

            migrationBuilder.CreateTable(
                name: "TransaccionesDetalle",
                columns: table => new
                {
                    IdTransaccionDetalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransaccionBibliotecaId = table.Column<int>(type: "int", nullable: false),
                    LibroId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransaccionBibliotecaIdTransaccionBiblioteca = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionesDetalle", x => x.IdTransaccionDetalle);
                    table.ForeignKey(
                        name: "FK_TransaccionesDetalle_Libro_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libro",
                        principalColumn: "IdLibro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionesDetalle_TransaccionesBiblioteca_TransaccionBibliotecaId",
                        column: x => x.TransaccionBibliotecaId,
                        principalTable: "TransaccionesBiblioteca",
                        principalColumn: "IdTransaccionBiblioteca",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransaccionesDetalle_TransaccionesBiblioteca_TransaccionBibliotecaIdTransaccionBiblioteca",
                        column: x => x.TransaccionBibliotecaIdTransaccionBiblioteca,
                        principalTable: "TransaccionesBiblioteca",
                        principalColumn: "IdTransaccionBiblioteca");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesBiblioteca_ClienteId",
                table: "TransaccionesBiblioteca",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesBiblioteca_EmpleadoId",
                table: "TransaccionesBiblioteca",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesDetalle_LibroId",
                table: "TransaccionesDetalle",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesDetalle_TransaccionBibliotecaId",
                table: "TransaccionesDetalle",
                column: "TransaccionBibliotecaId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesDetalle_TransaccionBibliotecaIdTransaccionBiblioteca",
                table: "TransaccionesDetalle",
                column: "TransaccionBibliotecaIdTransaccionBiblioteca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransaccionesDetalle");

            migrationBuilder.DropTable(
                name: "TransaccionesBiblioteca");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "FechaRegistro",
                table: "Cliente",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
