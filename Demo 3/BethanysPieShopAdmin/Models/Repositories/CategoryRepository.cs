using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopAdmin.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public CategoryRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _bethanysPieShopDbContext.Categories.AsNoTracking().OrderBy(p => p.CategoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _bethanysPieShopDbContext.Categories.AsNoTracking().OrderBy(c => c.CategoryId).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _bethanysPieShopDbContext.Categories.AsNoTracking().Include(p => p.Pies).FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<int> AddCategoryAsync(Category category)
        {
            bool categoryWithSameNameExist = await _bethanysPieShopDbContext.Categories.AnyAsync(c => c.Name == category.Name);

            if (categoryWithSameNameExist)
            {
                throw new Exception("A category with the same name already exists");
            }

            _bethanysPieShopDbContext.Categories.Add(category);//could be done using async too

            return await _bethanysPieShopDbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateCategoryAsync(Category category)
        {
            bool CategoryWithSameNameExist = await _bethanysPieShopDbContext.Categories.AnyAsync(x => x.Name == category.Name && x.CategoryId != category.CategoryId);
            if (CategoryWithSameNameExist)
            {
                throw new Exception("A category with the same name already exists");
            }
            var CategoryToUpdate= await _bethanysPieShopDbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == category.CategoryId);

            if (CategoryToUpdate != null) {
            CategoryToUpdate.Name = category.Name;
                CategoryToUpdate.Description = category.Description;
              _bethanysPieShopDbContext.Categories.Update(CategoryToUpdate);
                return await _bethanysPieShopDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Category not found");
            }

        }

        public async Task<int> DeleteCategoryAsync(int categoryId)
        {
            var categoryToDelete = await _bethanysPieShopDbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
           
            var piesInCategory = _bethanysPieShopDbContext.Pies.Any(x=>x.CategoryId == categoryId);
            if (piesInCategory)
            {
                throw new Exception("Category has pies, cannot delete");
            }
            if (categoryToDelete != null)
            {
                _bethanysPieShopDbContext.Categories.Remove(categoryToDelete);
                return await _bethanysPieShopDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Category not found");
            }
        }
    }
}
