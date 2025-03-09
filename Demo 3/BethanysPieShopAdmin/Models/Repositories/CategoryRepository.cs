using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopAdmin.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopDbContext _context;
        public CategoryRepository(BethanysPieShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            if (await _context.Categories.AnyAsync(x => x.Name == category.Name))
            {
                throw new Exception("It already exists");
            }

            _context.Categories.Add(category); 
            return await _context.SaveChangesAsync(); 
        }


        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.OrderBy(x => x.CategoryId);
        }

        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await _context.Categories.OrderBy(x => x.CategoryId).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.Include(x => x.Pies).FirstOrDefaultAsync(x => x.CategoryId == id);
        }
    }
}
