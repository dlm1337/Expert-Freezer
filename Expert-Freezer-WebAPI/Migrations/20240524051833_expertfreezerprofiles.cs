using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExpertFreezerWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class expertfreezerprofiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "expertFreezerProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    ProfilePic = table.Column<string>(type: "text", nullable: true),
                    ExtraPics = table.Column<List<string>>(type: "text[]", nullable: true),
                    ExtraPicsDesc = table.Column<List<string>>(type: "text[]", nullable: true),
                    CompanyDescription = table.Column<string>(type: "text", nullable: true),
                    Services = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Pricing = table.Column<string>(type: "text", nullable: true),
                    IsComplete = table.Column<bool>(type: "boolean", nullable: false),
                    Secret = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expertFreezerProfiles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expertFreezerProfiles");
        }
    }
}
