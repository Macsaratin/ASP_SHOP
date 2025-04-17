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
    public class AddressesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AddressesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Lấy danh sách địa chỉ
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddresses()
        {
            var addresses = await _context.Addresses
                .Include(a => a.User) // Nếu bạn muốn bao gồm thông tin người dùng
                .ToListAsync();
            var addressDtos = _mapper.Map<List<AddressResponseDto>>(addresses);
            return Ok(addressDtos);
        }

        // Lấy thông tin một địa chỉ theo Id
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddress(int id)
        {
            var address = await _context.Addresses
                .Include(a => a.User) // Nếu bạn muốn bao gồm thông tin người dùng
                .FirstOrDefaultAsync(a => a.Id == id);
            if (address == null)
                return NotFound();

            var addressDto = _mapper.Map<AddressResponseDto>(address);
            return Ok(addressDto);
        }

        // Tạo mới một địa chỉ
        [HttpPost]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressCreateDto addressDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var address = _mapper.Map<Address>(addressDto);
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            var responseDto = _mapper.Map<AddressResponseDto>(address);
            return CreatedAtAction(nameof(GetAddress), new { id = address.Id }, responseDto);
        }

        // Cập nhật thông tin một địa chỉ
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Admin")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressCreateDto addressDto)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
                return NotFound();

            _mapper.Map(addressDto, address);
            await _context.SaveChangesAsync();

            var responseDto = _mapper.Map<AddressResponseDto>(address);
            return Ok(responseDto);
        }

        // Xoá một địa chỉ
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
                return NotFound();

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
