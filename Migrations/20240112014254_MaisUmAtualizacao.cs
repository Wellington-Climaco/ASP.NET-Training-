using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TreinandoApi.Migrations
{
    /// <inheritdoc />
    public partial class MaisUmAtualizacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Usuario",
                type: "VARCHAR(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Usuario");
        }
    }
}
