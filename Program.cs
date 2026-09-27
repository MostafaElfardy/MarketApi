using MarketApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ربط قاعدة بيانات SQLite
builder.Services.AddDbContext<MarketDbContext>(options =>
    options.UseSqlite("Data Source=market.db"));

builder.Services.AddControllers();

var app = builder.Build();

// إنشاء قاعدة البيانات والجداول تلقائياً إذا لم تكن موجودة
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MarketDbContext>();
    db.Database.EnsureCreated();
}

// تفعيل قراءة ملفات الموقع من مجلد wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();