using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[] { 1L, "admin@brand.com", "Admin", "BILqc4FrKQJMKrgEf8XfEMJhIHBtmkRQ5d2NQ1aUVnAFQZcq88aEcGspDKrXAAWf+j7/Bx+5jv83oYOeSuZyg/U8+a/mrlvJoLi+ZkMALa1RwTewDcIGH+hMRV0ikYwV7m7hQ6HVxwYOonAOplTaTVBoupuGrMKWoc+XBanxioEXPWwnVHMLGnixS4ginJx49h10WkzrVFnk1mfyduSWvyr8D+f4pIMx2XnPGbdfeKynCFQ2PIvwYNeR41wA8O1md1Q3M5mkJAc+ibLLaSGkP807KsGFvg2cmWK9MYf0MiNI6ZlkPUsxWwvZiW9YRBaKGYe4WuBDHgY+5Fa87/6E0rOYTC675/yUd2M7bp3c2xatUY/lJv6WQ83dzEp6YuFV5OJyTRAidDJEhrKcbUasrl6mFGfw2DRGKLPfokRAnFY=" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
