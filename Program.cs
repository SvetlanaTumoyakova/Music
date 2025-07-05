using Microsoft.EntityFrameworkCore;
using Music.Data;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Uploadcare;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.Configure<UploadcareKeys>(
    builder.Configuration.GetSection("UploadcareKeys"));

string connection = builder.Configuration.GetConnectionString("MusicDbConnection");
builder.Services.AddDbContext<MusicDbContext>(options =>
options.UseSqlServer(connection));

builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<ISearchRepository, SearchRepository>();
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
