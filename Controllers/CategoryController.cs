using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SinemaArsivSitesi.Models;
using SinemaArsivSitesi.Services.Category;

namespace SinemaArsivSitesi.Controllers
{
 
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

   
        [HttpGet]
        [Route("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }
 
        [HttpPost("initialize")]
        public async Task<IActionResult> InitializeCategories()
        {
            await _categoryService.InitializeDefaultCategories();
            return Ok();
        }

    
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                return BadRequest("Kategori adı boş olamaz");
            }

            var result = await _categoryService.AddCategory(category.Name, category.Description);
            if (!result)
            {
                return BadRequest("Kategori oluşturulamadı");
            }

            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var result = await _categoryService.UpdateCategory(id, category.Name, category.Description);
            if (!result)
            {
                return BadRequest("Kategori güncellenemedi");
            }

            return NoContent();
        }

     
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            var result = await _categoryService.DeleteCategory(id);
            if (!result)
            {
                return BadRequest("Kategori silinemedi");
            }

            return NoContent();
        }
    }
} 