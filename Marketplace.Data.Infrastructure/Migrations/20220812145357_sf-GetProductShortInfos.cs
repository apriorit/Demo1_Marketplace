using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace.Data.Infrastructure.Migrations
{
    public partial class sfGetProductShortInfos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createUspGetProductShortInfos = @"CREATE OR ALTER PROCEDURE dbo.usp_GetProductShortInfos
                        AS
                        SELECT 
                          p.id
                        , p.Name
                        , pr.CurrentPrice
                        , pr.OldPrice
                        , pin.PathToImage
                        , p.ProductSubCategoryId
                        FROM Products as p
                        LEFT JOIN Prices as pr
	                        ON p.id = pr.ProductId
                        LEFT JOIN ProductInfos as PIn
	                        ON p.Id = pin.ProductId
                        LEFT JOIN ProductSubCategories as PSC
	                        ON p.ProductSubCategoryId = psc.Id
                        LEFT JOIN InformationSubCategories as isc
	                        ON pin.InformationSubCategoryId = isc.id 
                        LEFT JOIN InformationCategories as ic
	                        ON isc.InformationCategoryId = ic.Id 
                        WHERE (isc.Id IS NULL
	                        OR isc.Id = (SELECT TOP 1 Id FROM InformationSubCategories 
				                        WHERE UPPER(Name) = UPPER('PathToImage' )
					                        AND InformationSubCategoryId = (SELECT TOP 1 Id 
													                        FROM InformationCategories
													                        WHERE UPPER(Name) = UPPER('Main'))));";

            migrationBuilder.Sql(createUspGetProductShortInfos);

            var createUspGetProductShortInfosBySubcategory = @"CREATE OR ALTER PROCEDURE dbo.usp_GetProductShortInfosBySubcategory @SubCategoryId INT
                        AS
                        SELECT 
                          p.id
                        , p.Name
                        , pr.CurrentPrice
                        , pr.OldPrice
                        , pin.PathToImage
                        , p.ProductSubCategoryId
                        FROM Products as p
                        LEFT JOIN Prices as pr
	                        ON p.id = pr.ProductId
                        LEFT JOIN ProductInfos as PIn
	                        ON p.Id = pin.ProductId
                        LEFT JOIN ProductSubCategories as PSC
	                        ON p.ProductSubCategoryId = psc.Id
                        LEFT JOIN InformationSubCategories as isc
	                        ON pin.InformationSubCategoryId = isc.id 
                        LEFT JOIN InformationCategories as ic
	                        ON isc.InformationCategoryId = ic.Id 
                        WHERE (isc.Id IS NULL
	                        OR isc.Id = (SELECT TOP 1 Id FROM InformationSubCategories 
				                        WHERE UPPER(Name) = UPPER('PathToImage' )
					                        AND InformationSubCategoryId = (SELECT TOP 1 Id 
													                        FROM InformationCategories
													                        WHERE UPPER(Name) = UPPER('Main'))))
                            AND p.ProductSubCategoryId = @SubCategoryId;";

            migrationBuilder.Sql(createUspGetProductShortInfosBySubcategory);

            var createUspGetProductDetailById = @"CREATE OR ALTER PROCEDURE dbo.usp_GetProductDetailById @ProductId INT
                        AS
                        SELECT p.Id, p.Code, p.Name, pr.CurrentPrice, pr.OldPrice, pin.Information, pin.IsActive, pin.PathToImage
						FROM [dbo].[Products] p
							JOIN [dbo].[ProductInfos] pin ON p.Id = pin.ProductId
							JOIN [dbo].[Prices] pr ON p.Id = pr.ProductId
						WHERE p.Id = @ProductId;";

            migrationBuilder.Sql(createUspGetProductDetailById);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropSp = "DROP PROCEDURE dbo.usp_GetProductShortInfos ";
            migrationBuilder.Sql(dropSp);

            dropSp = "DROP PROCEDURE dbo.usp_GetProductShortInfosBySubcategory ";
            migrationBuilder.Sql(dropSp);

            dropSp = "DROP PROCEDURE dbo.usp_GetProductDetailById ";
            migrationBuilder.Sql(dropSp);
        }
    }
}
