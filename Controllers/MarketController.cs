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

        // 1. جلب قائمة المنتجات
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // 2. إنشاء طلب جديد
        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null) return BadRequest("بيانات الطلب غير صحيحة");

            order.Status = "تم الاستلام";
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "تم استلام طلبك بنجاح!", orderId = order.Id });
        }

        // 3. جلب جميع الطلبات مرتبة بالأحدث حسب الـ Id
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.OrderByDescending(o => o.Id).ToListAsync();
            return Ok(orders);
        }

        // 4. تحديث حالة الطلب
        [HttpPut("order/{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            order.Status = status;
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        // 5. جلب تفاصيل طلب محدد برقم الـ ID
        [HttpGet("order/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }
    }
}