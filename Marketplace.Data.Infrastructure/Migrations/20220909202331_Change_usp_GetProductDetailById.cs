using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Data.Infrastructure.Migrations
{
    public partial class Change_usp_GetProductDetailById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createUspGetProductDetailById = @"CREATE OR ALTER PROCEDURE dbo.usp_GetProductDetailById @ProductId INT
                        AS
                        SELECT p.Id, p.Code, p.Name, pr.CurrentPrice, pr.OldPrice, pin.Description, pin.IsActive, pin.PathToImage
						FROM [dbo].[Products] p
							JOIN [dbo].[ProductInfos] pin ON p.Id = pin.ProductId
							JOIN [dbo].[Prices] pr ON p.Id = pr.ProductId
						WHERE p.Id = @ProductId;";

            migrationBuilder.Sql(createUspGetProductDetailById);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropSp = "DROP PROCEDURE dbo.usp_GetProductDetailById ";
            migrationBuilder.Sql(dropSp);
        }
    }
}
