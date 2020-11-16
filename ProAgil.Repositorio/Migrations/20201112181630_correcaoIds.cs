using Microsoft.EntityFrameworkCore.Migrations;

namespace ProAgil.Repositorio.Migrations
{
    public partial class correcaoIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventosPalestrantes_Eventos_EventoId",
                table: "EventosPalestrantes");

            migrationBuilder.DropForeignKey(
                name: "FK_EventosPalestrantes_Palestrantes_PalestranteId",
                table: "EventosPalestrantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Lotes_Eventos_EventoId",
                table: "Lotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventosPalestrantes",
                table: "EventosPalestrantes");

            migrationBuilder.DropIndex(
                name: "IX_EventosPalestrantes_EventoId",
                table: "EventosPalestrantes");

            migrationBuilder.DropColumn(
                name: "IdEvento",
                table: "RedesSociais");

            migrationBuilder.DropColumn(
                name: "IdPalestrante",
                table: "RedesSociais");

            migrationBuilder.DropColumn(
                name: "IdEvento",
                table: "Lotes");

            migrationBuilder.DropColumn(
                name: "IdEvento",
                table: "EventosPalestrantes");

            migrationBuilder.DropColumn(
                name: "IdPalestrante",
                table: "EventosPalestrantes");

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "Lotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PalestranteId",
                table: "EventosPalestrantes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "EventosPalestrantes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventosPalestrantes",
                table: "EventosPalestrantes",
                columns: new[] { "EventoId", "PalestranteId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventosPalestrantes_Eventos_EventoId",
                table: "EventosPalestrantes",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventosPalestrantes_Palestrantes_PalestranteId",
                table: "EventosPalestrantes",
                column: "PalestranteId",
                principalTable: "Palestrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lotes_Eventos_EventoId",
                table: "Lotes",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventosPalestrantes_Eventos_EventoId",
                table: "EventosPalestrantes");

            migrationBuilder.DropForeignKey(
                name: "FK_EventosPalestrantes_Palestrantes_PalestranteId",
                table: "EventosPalestrantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Lotes_Eventos_EventoId",
                table: "Lotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventosPalestrantes",
                table: "EventosPalestrantes");

            migrationBuilder.AddColumn<int>(
                name: "IdEvento",
                table: "RedesSociais",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPalestrante",
                table: "RedesSociais",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "Lotes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "IdEvento",
                table: "Lotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PalestranteId",
                table: "EventosPalestrantes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "EventoId",
                table: "EventosPalestrantes",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "IdEvento",
                table: "EventosPalestrantes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdPalestrante",
                table: "EventosPalestrantes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventosPalestrantes",
                table: "EventosPalestrantes",
                columns: new[] { "IdEvento", "IdPalestrante" });

            migrationBuilder.CreateIndex(
                name: "IX_EventosPalestrantes_EventoId",
                table: "EventosPalestrantes",
                column: "EventoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventosPalestrantes_Eventos_EventoId",
                table: "EventosPalestrantes",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EventosPalestrantes_Palestrantes_PalestranteId",
                table: "EventosPalestrantes",
                column: "PalestranteId",
                principalTable: "Palestrantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lotes_Eventos_EventoId",
                table: "Lotes",
                column: "EventoId",
                principalTable: "Eventos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
