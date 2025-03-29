using ASP_SHOP.Data;
using ASP_SHOP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace ASP_SHOP.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // Thêm mới sản phẩm
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("⚠ ModelState không hợp lệ!");
                return View(product);
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

            // Tạo thư mục nếu chưa có
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
                Console.WriteLine("✅ Đã tạo thư mục: " + uploadsFolder);
            }

            // Xử lý file ảnh nếu có
            if (imageFile != null && imageFile.Length > 0)
            {
                string fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("Avatar", "Chỉ chấp nhận ảnh PNG, JPG, JPEG.");
                    return View(product);
                }

                // Đặt tên file duy nhất
                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                product.Avatar = "/images/" + uniqueFileName;
                Console.WriteLine("✅ Ảnh đã lưu: " + product.Avatar);
            }
            else
            {
                // Nếu không có ảnh, đặt ảnh mặc định
                product.Avatar = "/images/default.png";
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            Console.WriteLine($"🛒 Sản phẩm '{product.Name}' đã được thêm!");

            return RedirectToAction("Index");
        }




        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(int id, Product product, IFormFile? imageFile)
        {
            var existingProduct = _context.Products.Find(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(product);
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            if (imageFile != null && imageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileExtension = Path.GetExtension(imageFile.FileName).ToLower();
                string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };

                if (!allowedExtensions.Contains(fileExtension))
                {
                    ModelState.AddModelError("Avatar", "Chỉ chấp nhận ảnh PNG, JPG, JPEG.");
                    return View(product);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                existingProduct.Avatar = "/images/" + uniqueFileName;
                Console.WriteLine("✅ Ảnh đã cập nhật: " + existingProduct.Avatar);
            }

            _context.SaveChanges();
            Console.WriteLine($"🛠 Sản phẩm '{existingProduct.Name}' đã được cập nhật!");

            return RedirectToAction("Index");
        }


        // Xóa sản phẩm
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
