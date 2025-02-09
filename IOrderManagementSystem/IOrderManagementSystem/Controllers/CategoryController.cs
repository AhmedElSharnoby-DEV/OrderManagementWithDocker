using IOrderManagementSystem.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace IOrderManagementSystem.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }
        [HttpGet("get-category-by-id")]
        public async Task<IActionResult> GetCategoryById([FromQuery] long id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }
    }
}
