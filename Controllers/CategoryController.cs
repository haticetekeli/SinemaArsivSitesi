using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SinemaArsivSitesi.Services.Category;

namespace SinemaArsivSitesi.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();
            return View(categories);
        }

        public async Task<IActionResult> InitializeCategories()
        {
            await _categoryService.InitializeDefaultCategories();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Kategori adı boş olamaz.");
            }

            var result = await _categoryService.AddCategory(name, description);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Kategori eklenirken bir hata oluştu.");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(int id, string name, string description)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Kategori adı boş olamaz.");
            }

            var result = await _categoryService.UpdateCategory(id, name, description);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Kategori güncellenirken bir hata oluştu.");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Kategori silinirken bir hata oluştu.");

        }
    }
} 