using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopAdmin.Models.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public PieRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public async Task<IEnumerable<Pie>> GetAllPiesAsync()
        {
            return await _bethanysPieShopDbContext.Pies.OrderBy(c => c.PieId).AsNoTracking().ToListAsync();
        }

        public async Task<Pie?> GetPieByIdAsync(int pieId)
        {
            return await _bethanysPieShopDbContext.Pies.Include(p => p.Ingredients).Include(p => p.Category).AsNoTracking().FirstOrDefaultAsync(p => p.PieId == pieId);
        }

        public async Task<int> AddPieAsync(Pie pie)
        {
            //throw new Exception("Database down");
            _bethanysPieShopDbContext.Pies.Add(pie);//could be done using async too
            return await _bethanysPieShopDbContext.SaveChangesAsync();
        }
        public async Task<int> UpdatePieAsync(Pie pie)
        {
            var pieToUpdate = await _bethanysPieShopDbContext.Pies.FirstOrDefaultAsync(p => p.PieId == pie.PieId);
            if (pieToUpdate != null)
            {
                pieToUpdate.CategoryId = pie.CategoryId;
                pieToUpdate.Name = pie.Name;
                pieToUpdate.Price = pie.Price;
                pieToUpdate.ShortDescription = pie.ShortDescription;
                pieToUpdate.LongDescription = pie.LongDescription;
                pieToUpdate.AllergyInformation = pie.AllergyInformation;
                pieToUpdate.ImageUrl = pie.ImageUrl;
                pieToUpdate.ImageThumbnailUrl = pie.ImageThumbnailUrl;
                pieToUpdate.IsPieOfTheWeek = pie.IsPieOfTheWeek;
                pieToUpdate.InStock = pie.InStock;
                _bethanysPieShopDbContext.Pies.Update(pieToUpdate);
                return await _bethanysPieShopDbContext.SaveChangesAsync();

            }
            else
            {
                throw new ArgumentException("Pie not found");
            }
        }
    }
}
