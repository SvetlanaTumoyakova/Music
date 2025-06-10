using Microsoft.EntityFrameworkCore;
using Music.Data;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("MusicDbConnection");
builder.Services.AddDbContext<MusicDbContext>(options =>
options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();

var app = builder.Build();


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

app.Run();
