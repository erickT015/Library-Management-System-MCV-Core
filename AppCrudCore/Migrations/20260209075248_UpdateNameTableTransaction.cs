using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCrudCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameTableTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesBiblioteca_Cliente_ClienteId",
                table: "TransaccionesBiblioteca");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesBiblioteca_Empleado_EmpleadoId",
                table: "TransaccionesBiblioteca");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesDetalle_Libro_LibroId",
                table: "TransaccionesDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesDetalle_TransaccionesBiblioteca_TransaccionBibliotecaId",
                table: "TransaccionesDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionesDetalle_TransaccionesBiblioteca_TransaccionBibliotecaIdTransaccionBiblioteca",
                table: "TransaccionesDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransaccionesDetalle",
                table: "TransaccionesDetalle");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionesDetalle_TransaccionBibliotecaIdTransaccionBiblioteca",
                table: "TransaccionesDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransaccionesBiblioteca",
                table: "TransaccionesBiblioteca");

            migrationBuilder.DropColumn(
                name: "TransaccionBibliotecaIdTransaccionBiblioteca",
                table: "TransaccionesDetalle");

            migrationBuilder.RenameTable(
                name: "TransaccionesDetalle",
                newName: "TransaccionDetalle");

            migrationBuilder.RenameTable(
                name: "TransaccionesBiblioteca",
                newName: "TransaccionBiblioteca");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionesDetalle_TransaccionBibliotecaId",
                table: "TransaccionDetalle",
                newName: "IX_TransaccionDetalle_TransaccionBibliotecaId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionesDetalle_LibroId",
                table: "TransaccionDetalle",
                newName: "IX_TransaccionDetalle_LibroId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionesBiblioteca_EmpleadoId",
                table: "TransaccionBiblioteca",
                newName: "IX_TransaccionBiblioteca_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionesBiblioteca_ClienteId",
                table: "TransaccionBiblioteca",
                newName: "IX_TransaccionBiblioteca_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransaccionDetalle",
                table: "TransaccionDetalle",
                column: "IdTransaccionDetalle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransaccionBiblioteca",
                table: "TransaccionBiblioteca",
                column: "IdTransaccionBiblioteca");

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionBiblioteca_Cliente_ClienteId",
                table: "TransaccionBiblioteca",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionBiblioteca_Empleado_EmpleadoId",
                table: "TransaccionBiblioteca",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "IdEmpleado",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionDetalle_Libro_LibroId",
                table: "TransaccionDetalle",
                column: "LibroId",
                principalTable: "Libro",
                principalColumn: "IdLibro",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionDetalle_TransaccionBiblioteca_TransaccionBibliotecaId",
                table: "TransaccionDetalle",
                column: "TransaccionBibliotecaId",
                principalTable: "TransaccionBiblioteca",
                principalColumn: "IdTransaccionBiblioteca",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionBiblioteca_Cliente_ClienteId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionBiblioteca_Empleado_EmpleadoId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionDetalle_Libro_LibroId",
                table: "TransaccionDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionDetalle_TransaccionBiblioteca_TransaccionBibliotecaId",
                table: "TransaccionDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransaccionDetalle",
                table: "TransaccionDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransaccionBiblioteca",
                table: "TransaccionBiblioteca");

            migrationBuilder.RenameTable(
                name: "TransaccionDetalle",
                newName: "TransaccionesDetalle");

            migrationBuilder.RenameTable(
                name: "TransaccionBiblioteca",
                newName: "TransaccionesBiblioteca");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionDetalle_TransaccionBibliotecaId",
                table: "TransaccionesDetalle",
                newName: "IX_TransaccionesDetalle_TransaccionBibliotecaId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionDetalle_LibroId",
                table: "TransaccionesDetalle",
                newName: "IX_TransaccionesDetalle_LibroId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionBiblioteca_EmpleadoId",
                table: "TransaccionesBiblioteca",
                newName: "IX_TransaccionesBiblioteca_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_TransaccionBiblioteca_ClienteId",
                table: "TransaccionesBiblioteca",
                newName: "IX_TransaccionesBiblioteca_ClienteId");

            migrationBuilder.AddColumn<int>(
                name: "TransaccionBibliotecaIdTransaccionBiblioteca",
                table: "TransaccionesDetalle",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransaccionesDetalle",
                table: "TransaccionesDetalle",
                column: "IdTransaccionDetalle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransaccionesBiblioteca",
                table: "TransaccionesBiblioteca",
                column: "IdTransaccionBiblioteca");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesDetalle_TransaccionBibliotecaIdTransaccionBiblioteca",
                table: "TransaccionesDetalle",
                column: "TransaccionBibliotecaIdTransaccionBiblioteca");

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesBiblioteca_Cliente_ClienteId",
                table: "TransaccionesBiblioteca",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "IdCliente",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesBiblioteca_Empleado_EmpleadoId",
                table: "TransaccionesBiblioteca",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "IdEmpleado");

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesDetalle_Libro_LibroId",
                table: "TransaccionesDetalle",
                column: "LibroId",
                principalTable: "Libro",
                principalColumn: "IdLibro",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesDetalle_TransaccionesBiblioteca_TransaccionBibliotecaId",
                table: "TransaccionesDetalle",
                column: "TransaccionBibliotecaId",
                principalTable: "TransaccionesBiblioteca",
                principalColumn: "IdTransaccionBiblioteca",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionesDetalle_TransaccionesBiblioteca_TransaccionBibliotecaIdTransaccionBiblioteca",
                table: "TransaccionesDetalle",
                column: "TransaccionBibliotecaIdTransaccionBiblioteca",
                principalTable: "TransaccionesBiblioteca",
                principalColumn: "IdTransaccionBiblioteca");
        }
    }
}
