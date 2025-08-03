using ecommerce.Domain.Enitities;

namespace ecommerce.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    ValueTask<IReadOnlyList<Category>> GetCategoriesPaginatedAsync(int skip, int take);
}