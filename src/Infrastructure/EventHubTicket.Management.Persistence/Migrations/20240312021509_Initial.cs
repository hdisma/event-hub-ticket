using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventHubTicket.Management.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    OrderPlaced = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Organizer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Description", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("0a326e93-c86b-49ce-8ea3-2d1964b26341"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Musicals" },
                    { new Guid("10d9dcc8-40b0-421b-bfdb-ced634f32a6e"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Conferences" },
                    { new Guid("4b546d73-1872-4842-b706-00993f0fc74b"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Plays" },
                    { new Guid("7369722a-bd6c-4c65-adc2-4e808feef74a"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Concerts" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsPaid", "LastModifiedBy", "LastModifiedDate", "OrderPlaced", "Total", "UserId" },
                values: new object[,]
                {
                    { new Guid("3dcb3ea0-80b1-4781-b5c0-4d85c41e55a6"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, new DateTime(2024, 3, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6144), 245, new Guid("4ad901be-f447-46dd-bcf7-dbe401afa203") },
                    { new Guid("771cca4b-066c-4ac7-b3df-4d12837fe7e0"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, new DateTime(2024, 3, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6129), 85, new Guid("d97a15fc-0d32-41c6-9ddf-62f0735c4c1c") },
                    { new Guid("7e94bc5b-71a5-4c8c-bc3b-71bb7976237e"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, new DateTime(2024, 3, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6096), 400, new Guid("a441eb40-9636-4ee6-be49-a66c5ec1330b") },
                    { new Guid("86d3a045-b42d-4854-8150-d6a374948b6e"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, new DateTime(2024, 3, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6113), 135, new Guid("ac3cfaf5-34fd-4e4d-bc04-ad1083ddc340") },
                    { new Guid("ba0eb0ef-b69b-46fd-b8e2-41b4178ae7cb"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, new DateTime(2024, 3, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6191), 116, new Guid("7aeb2c01-fe8e-4b84-a5ba-330bdf950f5c") },
                    { new Guid("e6a2679c-79a3-4ef1-a478-6f4c91b405b6"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, new DateTime(2024, 3, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6159), 142, new Guid("7aeb2c01-fe8e-4b84-a5ba-330bdf950f5c") },
                    { new Guid("f5a6a3a0-4227-4973-abb5-a63fbe725923"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, new DateTime(2024, 3, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6175), 40, new Guid("f5a6a3a0-4227-4973-abb5-a63fbe725923") }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CategoryId", "CreatedBy", "CreatedDate", "Date", "Description", "ImageUrl", "LastModifiedBy", "LastModifiedDate", "Name", "Organizer", "Price" },
                values: new object[,]
                {
                    { new Guid("1babd057-e980-4cb3-9cd2-7fdd9e525668"), new Guid("10d9dcc8-40b0-421b-bfdb-ced634f32a6e"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6060), "The best tech conference in the world", "https://gillcleerenpluralsight.blob.core.windows.net/files/FirstRowTickets/conf.jpg", null, null, "Techorama 2021", "Many", 400 },
                    { new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"), new Guid("7369722a-bd6c-4c65-adc2-4e808feef74a"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6014), "Michael Johnson doesn't need an introduction. His 25 concert across the globe last year were seen by thousands. Can we add you to the list?", "https://gillcleerenpluralsight.blob.core.windows.net/files/FirstRowTickets/michael.jpg", null, null, "The State of Affairs: Michael Live!", "Michael Johnson", 85 },
                    { new Guid("62787623-4c52-43fe-b0c9-b7044fb5929b"), new Guid("7369722a-bd6c-4c65-adc2-4e808feef74a"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6045), "Get on the hype of Spanish Guitar concerts with Manuel.", "https://gillcleerenpluralsight.blob.core.windows.net/files/FirstRowTickets/guitar.jpg", null, null, "Spanish guitar hits with Manuel", "Manuel Santinonisi", 25 },
                    { new Guid("adc42c09-08c1-4d2c-9f96-2d15bb1af299"), new Guid("0a326e93-c86b-49ce-8ea3-2d1964b26341"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6077), "The critics are over the moon and so will you after you've watched this sing and dance extravaganza written by Nick Sailor, the man from 'My dad and sister'.", "https://gillcleerenpluralsight.blob.core.windows.net/files/FirstRowTickets/musical.jpg", null, null, "To the Moon and Back", "Nick Sailor", 135 },
                    { new Guid("b419a7ca-3321-4f38-be8e-4d7b6a529319"), new Guid("7369722a-bd6c-4c65-adc2-4e808feef74a"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(6030), "DJs from all over the world will compete in this epic battle for eternal fame.", "https://gillcleerenpluralsight.blob.core.windows.net/files/FirstRowTickets/dj.jpg", null, null, "Clash of the DJs", "DJ 'The Mike'", 85 },
                    { new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"), new Guid("7369722a-bd6c-4c65-adc2-4e808feef74a"), null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 9, 11, 22, 15, 8, 782, DateTimeKind.Local).AddTicks(5980), "Join John for his farwell tour across 15 continents. John really needs no introduction since he has already mesmerized the world with his banjo.", "https://gillcleerenpluralsight.blob.core.windows.net/files/FirstRowTickets/banjo.jpg", null, null, "John Egbert Live", "John Egbert", 65 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
