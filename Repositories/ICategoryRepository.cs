using dotnetcoreapi_cake_shop.Entities;

namespace dotnetcoreapi_cake_shop.Repositories
{
    public interface ICategoryRepository
    {
        // Get all categories
        IQueryable<Category> GetAllCategories();

        // Get category by id
        Task<Category> GetCategoryById(int categoryId);

        // Create category
        Task<Category> CreateCategory(Category category);

        // Update category
        Task<Category> UpdateCategory(Category category);

        // Delete category
        Task<Category> DeleteCategory(Category category);
    }
}
