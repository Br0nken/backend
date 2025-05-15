var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Кастомные маршруты
app.MapControllerRoute(
    name: "products",
    pattern: "products/{action}/{id?}",
    defaults: new { controller = "Products" });

app.MapControllerRoute(
    name: "products-category",
    pattern: "products/category/{category}",
    defaults: new { controller = "Products", action = "Category" });

app.MapControllerRoute(
    name: "blog",
    pattern: "blog/{year:int:min(2000)}/{month:range(1,12)}/{day?}",
    defaults: new { controller = "Blog", action = "Archive" });

// Маршрут по умолчанию
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Обработка 404 ошибки
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/NotFound";
        await next();
    }
});

app.Run();