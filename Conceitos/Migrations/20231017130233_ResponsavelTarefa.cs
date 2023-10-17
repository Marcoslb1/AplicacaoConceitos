using Microsoft.EntityFrameworkCore.Migrations;

namespace Conceitos.Migrations
{
    public partial class ResponsavelTarefa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsavel",
                table: "Tarefas");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Tarefas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_AspNetUsers_UsuarioId",
                table: "Tarefas",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_AspNetUsers_UsuarioId",
                table: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_UsuarioId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Tarefas");

            migrationBuilder.AddColumn<string>(
                name: "Responsavel",
                table: "Tarefas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
