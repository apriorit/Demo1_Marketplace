using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Data.Infrastructure.Migrations
{
    public partial class ChangeFieldType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "ProductInfos");

            migrationBuilder.AddColumn<string>(
                name: "PathToImage",
                table: "ProductInfos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathToImage",
                table: "ProductInfos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "ProductInfos",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
