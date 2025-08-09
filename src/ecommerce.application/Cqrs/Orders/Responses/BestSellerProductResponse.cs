namespace ecommerce.Application.Cqrs.Orders.Responses;

public class BestSellerProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public int? UnitsInStock { get; set; }
    public double Star { get; set; }
    public Guid CategoryId { get; set; }
    public int TotalSold { get; set; }
}