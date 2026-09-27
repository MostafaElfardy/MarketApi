using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketApi.Models;

namespace MarketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketController : ControllerBase
    {
        private readonly MarketDbContext _context;

        public MarketController(MarketDbContext context)
        {
            _context = context;
        }

        // 1. جلب جميع المنتجات
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // 2. تبديل حالة توفر المنتج (متاح / غير متوفر)
        [HttpPost("product/{id}/toggle-availability")]
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.IsAvailable = !product.IsAvailable;
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // 3. إضافة طلب جديد
        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null) return BadRequest();

            order.OrderDate = DateTime.Now;
            order.Status = "طلب جديد";

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم حفظ الطلب بنجاح", orderId = order.Id });
        }

        // 4. جلب جميع الطلبات
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.OrderByDescending(o => o.Id).ToListAsync();
            return Ok(orders);
        }

        // 5. تحديث حالة الطلب
        [HttpPut("order/{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Status = status;
            await _context.SaveChangesAsync();

            return Ok(order);
        }
    }
}