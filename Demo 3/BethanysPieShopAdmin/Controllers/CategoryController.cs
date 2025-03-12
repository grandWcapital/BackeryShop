using BethanysPieShopAdmin.Models;
using BethanysPieShopAdmin.Models.Repositories;
using BethanysPieShopAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShopAdmin.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            CategoryListViewModel model = new()
            {
                Categories = (await _categoryRepository.GetAllCategoriesAsync()).ToList()
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedCategory = await _categoryRepository.GetCategoryByIdAsync(id.Value);
            return View(selectedCategory);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name,Description,DateAdded")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryRepository.AddCategoryAsync(category);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Adding the category failed, please try again! Error: {ex.Message}");
            }

            return View(category);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _categoryRepository.GetCategoryByIdAsync(id.Value);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _categoryRepository.UpdateCategoryAsync(category);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Updating the category failed, please try again! Error: {ex.Message}");
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? Categoryid)
        {
            if (Categoryid == null) {
            ViewData["ErrorMessage"] = "Deleting the Category failed, invalid ID";
                return View();

            }
            try
            {
                await _categoryRepository.DeleteCategoryAsync(Categoryid.Value);
                TempData["CategoryDeleted"] = "Category Deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "ыыыыыыыыыыыыыыыыы";
            }
        }
     } 
}
