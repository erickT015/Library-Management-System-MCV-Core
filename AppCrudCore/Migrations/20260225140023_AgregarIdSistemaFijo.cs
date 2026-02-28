using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCrudCore.Migrations
{
    /// <inheritdoc />
    public partial class AgregarIdSistemaFijo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "IdUsuario", "Activo", "Cedula", "Correo", "Direccion", "FechaRegistro", "NombreCompleto", "PasswordHash", "RolId", "Telefono", "UltimoLogin" },
                values: new object[] { 1, true, "SYS-000", "system@internal.local", "INTERNO", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SISTEMA", "$2a$14$f/x5ixXCJXnA81FyYShXt.d4u3I4oOBlnIMeuXYb7KUe4sRXWIMT6", 1, "0000-0000", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "IdUsuario",
                keyValue: 1);
        }
    }
}
