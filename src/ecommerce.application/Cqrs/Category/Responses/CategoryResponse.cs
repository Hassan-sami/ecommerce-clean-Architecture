using Microsoft.AspNetCore.Http;

namespace ecommerce.Application.Cqrs.Category.Responses;

public class CategoryResponse
{
    public Guid Id { get; init; }
   
    public string Name { get; init; }
    
    
}