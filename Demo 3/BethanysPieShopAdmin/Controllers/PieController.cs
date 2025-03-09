using BethanysPieShopAdmin.Models.Repositories;
using BethanysPieShopAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Add()
        {
            var allCategories = await _categoryRepository.GetCategoryAsync();
            IEnumerable<SelectListItem> selectListItems = new SelectList(allCategories, "CategoryId", "Name", null);
            PieAddViewModel pieAddViewModel = new() { Categories = selectListItems };
            return View(pieAddViewModel);
        }

    }
}
