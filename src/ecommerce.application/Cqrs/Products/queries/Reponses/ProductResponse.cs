namespace ecommerce.Application.Cqrs.Products.queries.Reponses;

public class ProductResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
    public int? UnitsInStock { get; set; }
    public string? CatgoryName { get; set; } 
}