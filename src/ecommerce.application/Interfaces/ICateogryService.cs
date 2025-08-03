using ecommerce.Domain.Enitities;

namespace ecommerce.Application.Interfaces;

public interface ICateogryService
{
    Task<Category?> AddCategoryAsync(Category category);
    Task<Category?> GetCategoryByIdAsync(Guid id);
    ValueTask<IReadOnlyList<Category>> GetCategoriesPaginatedAsync(int skip, int take);
    ValueTask UpdateCategoryAsync(Category category);
    ValueTask<Category?> DeleteCategoryAsync(Guid id);
}