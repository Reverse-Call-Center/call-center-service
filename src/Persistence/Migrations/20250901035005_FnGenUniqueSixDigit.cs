using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace call_center_service.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FnGenUniqueSixDigit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AddColumn<int>(
                name: "RedemptionCode",
                table: "Receipts",
                type: "integer",
                maxLength: 6,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Receipts",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true);
            
            migrationBuilder.Sql(@"
                CREATE OR REPLACE FUNCTION gen_unique_six_digit()
                RETURNS int AS $$
                DECLARE
                    candidate int;
                    BEGIN
                        LOOP
                        candidate := floor(random() * 900000 + 100000);

                        EXIT WHEN NOT EXISTS (
                            SELECT 1 FROM ""Receipts"" WHERE ""RedemptionCode"" = candidate
                       );
                    END LOOP;

                   RETURN candidate;
                END;
                $$ LANGUAGE plpgsql;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedemptionCode",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Receipts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Receipts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "NOW()");
            
            migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS public.gen_unique_six_digit();");
        }
    }
}
