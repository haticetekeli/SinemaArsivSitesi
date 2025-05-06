using SinemaArsivSitesi.Models;

namespace SinemaArsivSitesi.Services.Category
{
    public interface ICategoryService
    {
        Task InitializeDefaultCategories();
        Task<bool> AddCategory(string name, string description);
        Task<bool> UpdateCategory(int id, string name, string description);
        Task<bool> DeleteCategory(int id);
        Task<List<Models.Category>> GetAllCategories();
        Task<Models.Category> GetCategoryById(int id);
    }
}
