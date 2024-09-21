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

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    idrestaurant = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    namerestaurant = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    emailrestaurant = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phonerestaurant = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    idowner = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.idrestaurant);
                    table.ForeignKey(
                        name: "FK_restaurants_users_idowner",
                        column: x => x.idowner,
                        principalTable: "users",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tables",
                columns: table => new
                {
                    idtable = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    numbertable = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    capacitytable = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    locationtable = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    valuetable = table.Column<decimal>(type: "numeric", nullable: false),
                    availabletable = table.Column<bool>(type: "boolean", nullable: false),
                    idrestaurant = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tables", x => x.idtable);
                    table.ForeignKey(
                        name: "FK_tables_restaurants_idrestaurant",
                        column: x => x.idrestaurant,
                        principalTable: "restaurants",
                        principalColumn: "idrestaurant",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    idreservation = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    datereservation = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    datefinish = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    timeduration = table.Column<string>(type: "character varying(23)", maxLength: 23, nullable: false),
                    valuereservation = table.Column<decimal>(type: "numeric(8,2)", nullable: false),
                    statusreservation = table.Column<int>(type: "integer", nullable: false),
                    IdUser = table.Column<int>(type: "integer", nullable: false),
                    IdTable = table.Column<int>(type: "integer", nullable: false),
                    createat = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    updateat = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.idreservation);
                    table.ForeignKey(
                        name: "FK_reservations_tables_IdTable",
                        column: x => x.IdTable,
                        principalTable: "tables",
                        principalColumn: "idtable",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservations_users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "users",
                        principalColumn: "iduser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "iduser", "emailuser", "imageprofile", "nameuser", "passworduser", "phoneuser" },
                values: new object[] { 1, "master@development.com", "", "Master", "99db87c3278f5eaa517260eaaa2b4b376be63d7f8a79c0f43992a493a3de8fc9", "" });

            migrationBuilder.CreateIndex(
                name: "IX_reservations_IdTable",
                table: "reservations",
                column: "IdTable");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_IdUser",
                table: "reservations",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_restaurants_idowner",
                table: "restaurants",
                column: "idowner");

            migrationBuilder.CreateIndex(
                name: "IX_tables_idrestaurant",
                table: "tables",
                column: "idrestaurant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "tables");

            migrationBuilder.DropTable(
                name: "restaurants");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
