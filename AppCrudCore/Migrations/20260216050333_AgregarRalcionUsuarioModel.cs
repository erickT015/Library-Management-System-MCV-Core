using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCrudCore.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRalcionUsuarioModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClienteUsuarioId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoUsuarioId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "TransaccionBiblioteca",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioPrestamo",
                table: "Libro",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Correo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UltimoLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequiereCambioPassword = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RolId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionBiblioteca_ClienteUsuarioId",
                table: "TransaccionBiblioteca",
                column: "ClienteUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionBiblioteca_EmpleadoUsuarioId",
                table: "TransaccionBiblioteca",
                column: "EmpleadoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionBiblioteca_UsuarioId",
                table: "TransaccionBiblioteca",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Cedula",
                table: "Usuario",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolId",
                table: "Usuario",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionBiblioteca_Usuario_ClienteUsuarioId",
                table: "TransaccionBiblioteca",
                column: "ClienteUsuarioId",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionBiblioteca_Usuario_EmpleadoUsuarioId",
                table: "TransaccionBiblioteca",
                column: "EmpleadoUsuarioId",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransaccionBiblioteca_Usuario_UsuarioId",
                table: "TransaccionBiblioteca",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionBiblioteca_Usuario_ClienteUsuarioId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionBiblioteca_Usuario_EmpleadoUsuarioId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropForeignKey(
                name: "FK_TransaccionBiblioteca_Usuario_UsuarioId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionBiblioteca_ClienteUsuarioId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionBiblioteca_EmpleadoUsuarioId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropIndex(
                name: "IX_TransaccionBiblioteca_UsuarioId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropColumn(
                name: "ClienteUsuarioId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropColumn(
                name: "EmpleadoUsuarioId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "TransaccionBiblioteca");

            migrationBuilder.DropColumn(
                name: "PrecioPrestamo",
                table: "Libro");
        }
    }
}
