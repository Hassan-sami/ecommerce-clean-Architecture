using Microsoft.EntityFrameworkCore;

namespace ecommerce.infra.Context;

public class CreateTopSelletStoredProducre
{
    private readonly AppDbContext _context;

    public CreateTopSelletStoredProducre(AppDbContext context)
    {
        _context = context;
    }
    public async Task Create()
    {

        
            try
            {
                string sql = @"
            CREATE PROCEDURE GetBestSellers
                @TopN INT = 10
            AS
            BEGIN
                SET NOCOUNT ON;

                SELECT 
                    p.Id,
                    p.Name,
                    p.Description,
                    p.ImageUrl,
                    p.UnitPrice,
                    p.UnitsInStock,
                    p.Star,
                    p.CategoryId,
                    SUM(oi.Quantity) as TotalSold
                FROM 
                    Products p
                    INNER JOIN OrderItems oi ON p.Id = oi.ProductId
                    INNER JOIN Orders o ON oi.OrderId = o.Id
                WHERE 
                    o.Status = 3  -- Only count completed orders
                GROUP BY 
                    p.Id,
                    p.Name,
                    p.Description,
                    p.ImageUrl,
                    p.UnitPrice,
                    p.UnitsInStock,
                    p.Star,
                    p.CategoryId
                ORDER BY 
                    TotalSold DESC
                OFFSET 0 ROWS FETCH NEXT @TopN ROWS ONLY;
            END";
                await _context.Database.ExecuteSqlRawAsync(sql);
            }
            catch (Exception e)
            {

            }

    }
}