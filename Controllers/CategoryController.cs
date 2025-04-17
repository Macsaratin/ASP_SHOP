using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_SHOP.Data;
using ASP_SHOP.DTOs;
using ASP_SHOP.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASP_SHOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.Parent)
                .ToListAsync();
            var categoryDtos = _mapper.Map<List<CategoryResponseDto>>(categories);
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
                return NotFound();

            var categoryDto = _mapper.Map<CategoryResponseDto>(category);
            return Ok(categoryDto);
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (categoryDto.ParentId.HasValue)
            {
                var parentCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == categoryDto.ParentId.Value);
                if (parentCategory == null)
                {
                    return BadRequest("ParentId must refer to an existing category.");
                }
            }

            var category = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var responseDto = _mapper.Map<CategoryResponseDto>(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, responseDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryCreateDto categoryDto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();
            if (categoryDto.ParentId.HasValue)
            {
                var parentCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.Id == categoryDto.ParentId.Value);
                if (parentCategory == null)
                {
                    return BadRequest("ParentId must refer to an existing category.");
                }
            }

            _mapper.Map(categoryDto, category);
            await _context.SaveChangesAsync();

            var responseDto = _mapper.Map<CategoryResponseDto>(category);
            return Ok(responseDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
