using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarketApi.Models;
using MarketApi.Models;

namespace MarketApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketController : ControllerBase
{
    private readonly MarketDbContext _context;

    public MarketController(MarketDbContext context)
    {
        _context = context;
    }

    // 1. جلب المنتجات
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _context.Products.ToListAsync();
        return Ok(products);
    }

    // 2. إضافة منتج جديد من لوحة الأدمن
    [HttpPost("products")]
    public async Task<IActionResult> AddProduct([FromBody] Product product)
    {
        if (product == null) return BadRequest();

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return Ok(product);
    }

    // 3. استقبال وحفظ طلب جديد
    [HttpPost("order")]
    public async Task<IActionResult> CreateOrder([FromBody] Order order)
    {
        if (order == null) return BadRequest();


        order.Status = "تم استلام الطلب";

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Order received successfully", orderId = order.Id });
    }

    // 4. جلب جميع الطلبات للوحة التحكم
    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _context.Orders.OrderByDescending(o => o.Id).ToListAsync();
        return Ok(orders);
    }

    // 5. جلب طلب محدد بواسطة الـ ID لصفحة متابعة الطلب
    [HttpGet("order/{id}")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound(new { message = "Order not found" });

        return Ok(order);
    }

    // 6. تحديث حالة الطلب من لوحة الأدمن
    [HttpPut("order/{id}/status")]
    public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return NotFound();

        order.Status = status;
        await _context.SaveChangesAsync();

        return Ok(order);
    }

    // 7. تغيير حالة توفر المنتج
    [HttpPut("product/{id}/toggle-availability")]
    public async Task<IActionResult> ToggleProductAvailability(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        product.IsAvailable = !product.IsAvailable;
        await _context.SaveChangesAsync();

        return Ok(product);
    }
}