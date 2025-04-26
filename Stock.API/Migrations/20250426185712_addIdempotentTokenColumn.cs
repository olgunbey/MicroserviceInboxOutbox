using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Stock.API.Migrations
{
    /// <inheritdoc />
    public partial class addIdempotentTokenColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInbox",
                table: "OrderInbox");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderInbox");

            migrationBuilder.AddColumn<Guid>(
                name: "IdempotentToken",
                table: "OrderInbox",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInbox",
                table: "OrderInbox",
                column: "IdempotentToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderInbox",
                table: "OrderInbox");

            migrationBuilder.DropColumn(
                name: "IdempotentToken",
                table: "OrderInbox");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderInbox",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderInbox",
                table: "OrderInbox",
                column: "Id");
        }
    }
}
