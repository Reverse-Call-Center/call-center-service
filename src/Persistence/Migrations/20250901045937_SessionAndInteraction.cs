using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace call_center_service.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SessionAndInteraction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Receipts",
                type: "character varying(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RedemptionCode",
                table: "Receipts",
                type: "integer",
                maxLength: 6,
                nullable: false,
                defaultValueSql: "gen_unique_six_digit()",
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 6);

            migrationBuilder.AddColumn<bool>(
                name: "IsValid",
                table: "Receipts",
                type: "boolean",
                nullable: false,
                defaultValue: true);

            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    InteractionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false),
                    InteractionType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    InteractionData = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    InteractionTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.InteractionId);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionId = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    RedemptionCode = table.Column<int>(type: "integer", nullable: false),
                    SessionType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SessionFingerPrint = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SessionPhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SessionIpAddress = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    SessionStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    SessionEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_RedemptionCode",
                table: "Receipts",
                column: "RedemptionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interactions_SessionId",
                table: "Interactions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_RedemptionCode",
                table: "Sessions",
                column: "RedemptionCode");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionFingerPrint",
                table: "Sessions",
                column: "SessionFingerPrint");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionIpAddress",
                table: "Sessions",
                column: "SessionIpAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SessionPhoneNumber",
                table: "Sessions",
                column: "SessionPhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_RedemptionCode",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Receipts");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                table: "Receipts",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<int>(
                name: "RedemptionCode",
                table: "Receipts",
                type: "integer",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 6,
                oldDefaultValueSql: "gen_unique_six_digit()");
        }
    }
}
