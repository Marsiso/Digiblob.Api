using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Digiblob.Api.Auth.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    normalized_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    concurrency_stamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    given_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    family_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    user_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    normalized_user_name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    normalized_email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    concurrency_stamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    security_stamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    access_failed_count = table.Column<int>(type: "int", nullable: false),
                    date_lockout_end = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.UserId, x.RoleId })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_user_roles_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_roles_normalized_name",
                table: "roles",
                column: "normalized_name",
                unique: true)
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_RoleId",
                table: "user_roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_users_normalized_user_name_normalized_email",
                table: "users",
                columns: new[] { "normalized_user_name", "normalized_email" },
                unique: true)
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
