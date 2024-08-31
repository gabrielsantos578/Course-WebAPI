using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseGuide.Migrations
{
    /// <inheritdoc />
    public partial class Database_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    iduser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    imageprofile = table.Column<string>(type: "text", nullable: false),
                    nameuser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    emailuser = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    passworduser = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    phoneuser = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.iduser);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "iduser", "emailuser", "imageprofile", "nameuser", "passworduser", "phoneuser" },
                values: new object[] { 1, "master@development.com", "", "Master", "99db87c3278f5eaa517260eaaa2b4b376be63d7f8a79c0f43992a493a3de8fc9", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
