using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

// Дополнительная папка для статики (wwwroot/special)
var specialStaticPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "special");
if (!Directory.Exists(specialStaticPath))
{
    Directory.CreateDirectory(specialStaticPath); // Создаем папку, если её нет
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(specialStaticPath),
    RequestPath = "/special-content"  // URL-префикс для доступа
});

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();