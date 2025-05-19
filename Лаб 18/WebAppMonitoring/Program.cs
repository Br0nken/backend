using System.Diagnostics.Metrics;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

var meter = new Meter(
    name: "WebAppMetrics",
    version: "1.0");

var requestCounter = meter.CreateCounter<int>(
    name: "http.requests",
    unit: "{requests}",
    description: "Total HTTP requests");

var errorCounter = meter.CreateCounter<int>(
    name: "http.errors",
    unit: "{errors}",
    description: "Total HTTP errors");

var responseTime = meter.CreateHistogram<double>(
    name: "http.response.time",
    unit: "ms",
    description: "Response time in milliseconds");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var startTime = DateTime.UtcNow;

    if (!context.Request.Path.StartsWithSegments("/metrics-dashboard") &&
        !context.Request.Path.StartsWithSegments("/lib") &&
        !context.Request.Path.StartsWithSegments("/css"))
    {
        Interlocked.Increment(ref AppMetrics.TotalRequests);
    }

    try
    {
        await next();
    }
    finally
    {
        var elapsed = (DateTime.UtcNow - startTime).TotalMilliseconds;
        lock (AppMetrics.ResponseTimes)
        {
            AppMetrics.ResponseTimes.Add(elapsed);
        }
    }
});

app.MapGet("/metrics-data", () =>
{
    double avgTime;
    lock (AppMetrics.ResponseTimes)
    {
        avgTime = AppMetrics.ResponseTimes.Count > 0 ?
            AppMetrics.ResponseTimes.Average() : 0;
    }

    return Results.Json(new
    {
        totalRequests = AppMetrics.TotalRequests,
        totalErrors = AppMetrics.TotalErrors,
        avgResponseTime = Math.Round(avgTime, 2)
    });
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/monitoring/test", () =>
{
    var delay = new Random().Next(50, 500);
    Thread.Sleep(delay);
    Interlocked.Increment(ref AppMetrics.TotalRequests);

    return Results.Redirect($"/test-result.html?delay={delay}");
});

app.MapGet("/monitoring/generate-error", () =>
{
    Interlocked.Increment(ref AppMetrics.TotalErrors);
    return Results.Redirect("/error-page.html");
});

app.Run();

public static class AppMetrics
{
    public static int TotalRequests = 0;
    public static int TotalErrors = 0;
    public static readonly List<double> ResponseTimes = new();
}