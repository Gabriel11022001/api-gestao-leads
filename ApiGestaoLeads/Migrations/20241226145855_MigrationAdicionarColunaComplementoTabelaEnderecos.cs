﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiGestaoLeads.Migrations
{
    /// <inheritdoc />
    public partial class MigrationAdicionarColunaComplementoTabelaEnderecos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Enderecos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Enderecos");
        }
    }
}
