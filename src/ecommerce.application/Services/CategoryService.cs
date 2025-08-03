using ecommerce.Application.Interfaces;
using ecommerce.Domain.Enitities;
using ecommerce.Domain.Interfaces;

namespace ecommerce.Application.Services;

public class CategoryService : ICateogryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    public async Task<Category?> AddCategoryAsync(Category category)
    {
        return await _categoryRepository.AddAsync(category);
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async ValueTask<IReadOnlyList<Category>> GetCategoriesPaginatedAsync(int skip, int take)
    {
        return await _categoryRepository.GetCategoriesPaginatedAsync(skip, take); 
    }

    public async ValueTask UpdateCategoryAsync(Category category)
    {
        await _categoryRepository.UpdateAsync(category);
    }

    public async ValueTask<Category?> DeleteCategoryAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        if(category is null)
            return null;
        await _categoryRepository.DeleteAsync(category);
        return category;
        
    }
}