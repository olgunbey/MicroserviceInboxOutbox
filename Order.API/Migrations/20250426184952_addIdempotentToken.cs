using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Order.API.Migrations
{
    /// <inheritdoc />
    public partial class addIdempotentToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderOutbox",
                table: "OrderOutbox");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderOutbox");

            migrationBuilder.AddColumn<Guid>(
                name: "IdempotentToken",
                table: "OrderOutbox",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderOutbox",
                table: "OrderOutbox",
                column: "IdempotentToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderOutbox",
                table: "OrderOutbox");

            migrationBuilder.DropColumn(
                name: "IdempotentToken",
                table: "OrderOutbox");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderOutbox",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderOutbox",
                table: "OrderOutbox",
                column: "Id");
        }
    }
}
