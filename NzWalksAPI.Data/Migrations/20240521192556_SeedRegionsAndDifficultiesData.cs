using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NzWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedRegionsAndDifficultiesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("afcc9581-dcb9-4096-916a-fbed178c3821"), "Easy" },
                    { new Guid("cfd30386-d2c9-4fd1-8646-51b02bdb3c17"), "Hard" },
                    { new Guid("f57e2c5d-2b32-490a-bfb2-d5a4b0d113cb"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("0c4d772e-27fa-4247-c199-08dc7985bde5"), "WLG", "https://fastly.picsum.photos/id/1001/800/600.jpg?hmac=SYMSTZ_jGTLCAYO7vGHOck2TiSy2qjo6OPHwBGNem0I", "Wellington Region" },
                    { new Guid("3da94be2-f663-4e18-8843-ede475618c77"), "STL", null, "Southland" },
                    { new Guid("63e99d9b-a915-4d21-1025-08dc7989d7f2"), "QLS", "https://fastly.picsum.photos/id/176/800/600.jpg?hmac=dGLT-aXDtvYjsZo8E4Vp7osGVQ_z0Gw0BomVl52uU5o", "Queenstown Region" },
                    { new Guid("b517a10d-2e08-4031-9c6b-b26e77560c36"), "AKL", "https://fastly.picsum.photos/id/337/800/600.jpg?hmac=Gx8pjhwU87sYnp2K9YBi3hvR78dBeAUbrVF6KJCwGJA", "Auckland" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("afcc9581-dcb9-4096-916a-fbed178c3821"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("cfd30386-d2c9-4fd1-8646-51b02bdb3c17"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f57e2c5d-2b32-490a-bfb2-d5a4b0d113cb"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("0c4d772e-27fa-4247-c199-08dc7985bde5"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3da94be2-f663-4e18-8843-ede475618c77"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("63e99d9b-a915-4d21-1025-08dc7989d7f2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b517a10d-2e08-4031-9c6b-b26e77560c36"));
        }
    }
}
