
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShopAdmin.Models.Repositories
{
    public class PieRepository : IPieRepository
        
    {
        public readonly BethanysPieShopDbContext _context;
        public PieRepository(BethanysPieShopDbContext context)
        {
            _context = context;
        }

        public async  Task<int> AddPieAsync(Pie pie)
        {
            _context.Pies.Add(pie);
            return await _context.SaveChangesAsync();
        }
        

        public async Task<IEnumerable<Pie>> GetAllPiesAsync()
        {
            return await _context.Pies.OrderBy(x=>x.PieId).ToListAsync();
        }

        public async Task<Pie> GetPieByIdAsync(int id)
        {
            return await _context.Pies.Include(p=>p.Ingredients )
                                .Include(p => p.Category).FirstOrDefaultAsync(x => x.PieId == id);
        }
    }
}
