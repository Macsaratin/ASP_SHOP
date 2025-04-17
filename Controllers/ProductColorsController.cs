using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_SHOP.Data;
using ASP_SHOP.DTOs;
using ASP_SHOP.Models;
using System.Security.Claims;

namespace ASP_SHOP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductColorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductColorsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductColors()
        {
            var colors = await _context.ProductColors
                .Include(pc => pc.Variant).ThenInclude(v => v.Product)
                .ToListAsync();

            var colorDtos = _mapper.Map<List<ProductColorResponseDto>>(colors);
            return Ok(colorDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductColor(int id)
        {
            var color = await _context.ProductColors
                .Include(pc => pc.Variant).ThenInclude(v => v.Product)
                .FirstOrDefaultAsync(pc => pc.Id == id);

            if (color == null)
                return NotFound();

            var colorDto = _mapper.Map<ProductColorResponseDto>(color);
            return Ok(colorDto);
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> CreateProductColor([FromBody] ProductColorCreateDto colorDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var color = _mapper.Map<ProductColor>(colorDto);

            _context.ProductColors.Add(color);
            await _context.SaveChangesAsync();

            var responseDto = _mapper.Map<ProductColorResponseDto>(color);
            return CreatedAtAction(nameof(GetProductColor), new { id = color.Id }, responseDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> UpdateProductColor(int id, [FromBody] ProductColorCreateDto colorDto)
        {
            var color = await _context.ProductColors.FindAsync(id);
            if (color == null)
                return NotFound();

            _mapper.Map(colorDto, color);
            await _context.SaveChangesAsync();

            var responseDto = _mapper.Map<ProductColorResponseDto>(color);
            return Ok(responseDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProductColor(int id)
        {
            var color = await _context.ProductColors.FindAsync(id);
            if (color == null)
                return NotFound();

            _context.ProductColors.Remove(color);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
