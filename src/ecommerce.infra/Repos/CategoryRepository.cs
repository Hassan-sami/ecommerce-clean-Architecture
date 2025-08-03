using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;
using ecommerce.Domain.Specifications;
using ecommerce.infra.Context;

namespace ecommerce.infra.Repos;

public class CategoryRepository : Repository<Category> , ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async ValueTask<IReadOnlyList<Category>> GetCategoriesPaginatedAsync(int skip, int take)
    {
        var spec = new CategoryPaginatedSpec(skip, take);
        return await GetAsync(spec);
    }
}