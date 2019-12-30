using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTask.Migrations
{
    public partial class AddCompanyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    TableNumber = table.Column<int>(nullable: false),
                    Order = table.Column<string>(nullable: true),
                    ManagerId = table.Column<int>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Persons_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "IsRemoved", "ManagerId", "Name", "Order", "Position", "TableNumber" },
                values: new object[,]
                {
                    { 1, false, null, "Конрад Керз", "Отдел Фронтэнда", "Разработчик", 1000 },
                    { 2, false, null, "Марнеус Калгар", "Отдел Бэкэнда", "Разработчик", 1001 },
                    { 3, false, null, "Робаут Жиллиман", "Отдел Фронтэнда", "Разработчик", 1002 },
                    { 4, false, null, "Хорус Луперкаль", "IT-департамент", "Директор департамента", 1003 },
                    { 5, false, null, "Леман Русс", "Отдел Фронтэнда", "ТимЛид", 1004 },
                    { 6, false, null, "Лион Эль-Джонсон", "Отдел Бэкэнда", "Разработчик", 1005 },
                    { 7, false, null, "Магнус Рыжый", "Отдел Бэкэнда", "ТимЛид", 1006 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ManagerId",
                table: "Persons",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
