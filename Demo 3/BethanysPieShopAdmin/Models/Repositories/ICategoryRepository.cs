namespace BethanysPieShopAdmin.Models.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Task<IEnumerable<Category>> GetCategoryAsync();
        Task<Category> GetCategoryById(int id);
        Task<int> AddCategoryAsync(Category category);
    }
}
