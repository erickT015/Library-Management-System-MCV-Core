using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCrudCore.Migrations
{
    /// <inheritdoc />
    public partial class RemoverEmpleadoClienteFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionBiblioteca_Cliente_ClienteId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionBiblioteca_Empleado_EmpleadoId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionBiblioteca_ClienteId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionBiblioteca_EmpleadoId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "TransaccionBiblioteca");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoUsuarioId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteUsuarioId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmpleadoUsuarioId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteUsuarioId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionBiblioteca_ClienteId",
                table: "TransaccionBiblioteca",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionBiblioteca_EmpleadoId",
                table: "TransaccionBiblioteca",
                column: "EmpleadoId");

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
        }
    }
}
