using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCrudCore.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLibroAndClienteStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Rol_RolId",
                table: "Cliente");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Libro_Stock",
                table: "Libro");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Libro_Stock_NoNegativo",
                table: "Libro");

            migrationBuilder.RenameColumn(
                name: "StockDisponible",
                table: "Libro",
                newName: "StockVenta");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioVenta",
                table: "Libro",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "StockPrestamo",
                table: "Libro",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Cliente",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PasswordTemp",
                table: "Cliente",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Libro_Stock_NoNegativo",
                table: "Libro",
                sql: "StockTotal >= 0 AND StockPrestamo >= 0 AND StockVenta >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Libro_Stock_Total",
                table: "Libro",
                sql: "StockPrestamo + StockVenta <= StockTotal");

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_Correo",
                table: "Cliente",
                column: "Correo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Rol_RolId",
                table: "Cliente",
                column: "RolId",
                principalTable: "Rol",
                principalColumn: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Rol_RolId",
                table: "Cliente");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Libro_Stock_NoNegativo",
                table: "Libro");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Libro_Stock_Total",
                table: "Libro");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_Correo",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "PrecioVenta",
                table: "Libro");

            migrationBuilder.DropColumn(
                name: "StockPrestamo",
                table: "Libro");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "PasswordTemp",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "StockVenta",
                table: "Libro",
                newName: "StockDisponible");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Cliente",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Libro_Stock",
                table: "Libro",
                sql: "StockDisponible <= StockTotal");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Libro_Stock_NoNegativo",
                table: "Libro",
                sql: "StockTotal >= 0 AND StockDisponible >= 0");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Rol_RolId",
                table: "Cliente",
                column: "RolId",
                principalTable: "Rol",
                principalColumn: "IdRol",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
