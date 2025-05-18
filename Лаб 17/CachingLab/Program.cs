var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы кеширования
builder.Services.AddResponseCaching();
builder.Services.AddControllersWithViews();

// Добавьте это (важная строка):
builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("DistCache_ConnectionString");
    options.SchemaName = "dbo";
    options.TableName = "TestCache";
});

var app = builder.Build();

// Конфигурация pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Включение кеширования ответов
app.UseResponseCaching();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();