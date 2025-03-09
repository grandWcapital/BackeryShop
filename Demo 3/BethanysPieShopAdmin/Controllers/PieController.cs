using BethanysPieShopAdmin.Models.Repositories;
using BethanysPieShopAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using BethanysPieShopAdmin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BethanysPieShopAdmin.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository repository, ICategoryRepository categoryRepository)
        {   
            _categoryRepository = categoryRepository;
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var pies = await _repository.GetAllPiesAsync();
            return View(pies);
        }
        public async Task<IActionResult> Details( int id)
        {
            var pie = await _repository.GetPieByIdAsync(id);
            return View(pie);
        }
        [HttpPost]
        public async Task<IActionResult> Add(PieAddViewModel pieAddViewModel)
        {
            if (ModelState.IsValid)
            {
                Pie pie = new()
                {
                    CategoryId = pieAddViewModel.Pie.CategoryId,
                    ShortDescription = pieAddViewModel.Pie.ShortDescription,
                    LongDescription = pieAddViewModel.Pie.LongDescription,
                    Price = pieAddViewModel.Pie.Price,
                    AllergyInformation = pieAddViewModel.Pie.AllergyInformation,
                    ImageThumbnailUrl = pieAddViewModel.Pie.ImageThumbnailUrl,
                    ImageUrl = pieAddViewModel.Pie.ImageUrl,
                    InStock = pieAddViewModel.Pie.InStock,
                    IsPieOfTheWeek = pieAddViewModel.Pie.IsPieOfTheWeek,
                    Name = pieAddViewModel.Pie.Name,

                };
                await _repository.AddPieAsync(pie);
                return RedirectToAction(nameof(Index));
            }  
            var allCategories = await _categoryRepository.GetCategoryAsync();

            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories, "CategoryId", "Name", null);
            pieAddViewModel.Categories = selectListItems;
            return View(pieAddViewModel);
        
        
        }
    


    }
}
