using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CourseGuide.Migrations
{
    /// <inheritdoc />
    public partial class Database_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    idrestaurant = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    namerestaurant = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    emailrestaurant = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phonerestaurant = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    iduser = table.Column<int>(type: "integer", nullable: false),
                    OwnerModelId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.idrestaurant);
                    table.ForeignKey(
                        name: "FK_restaurants_users_OwnerModelId",
                        column: x => x.OwnerModelId,
                        principalTable: "users",
                        principalColumn: "iduser");
                });

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_OwnerModelId",
                table: "restaurants",
                column: "OwnerModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "restaurants");
        }
    }
}
