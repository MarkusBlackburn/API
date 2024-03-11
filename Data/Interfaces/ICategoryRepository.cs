using API.Models.Domain;

namespace API.Data.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category category);

        Task<IEnumerable<Category>> GetAllCategories();

        Task<Category?> GetCategoryById(Guid id);

        Task<Category?> UpdateCategoryById(Category category);

        Task<Category?> DeleteCategoryById(Guid id);
    }
}