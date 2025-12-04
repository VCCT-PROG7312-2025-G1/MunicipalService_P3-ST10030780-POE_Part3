using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using MunicipalService_P3.Data;
using MunicipalService_P3.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register MVC services
builder.Services.AddControllersWithViews();

// ✅ Register EF Core with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register in-memory fallback service (optional if EF not used)
builder.Services.AddSingleton<IDataService, InMemoryDataService>();

var app = builder.Build();

// ✅ Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔹 Add authentication if needed
// app.UseAuthentication();

app.UseAuthorization();

// ✅ Configure default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();