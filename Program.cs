using Microsoft.EntityFrameworkCore;
using ProiectBigData.Data;
using ProiectBigData.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpClient<IDiseasePredictionService,DiseasePredictionService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:62792"); // pune aici portul tău
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddGrpcClient<ProiectBigData.GrpcService.AnalyticsMonitor.AnalyticsMonitorClient>(o =>
{
   
    o.Address = new Uri("http://localhost:5078");
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
