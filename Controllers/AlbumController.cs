using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data;

namespace Music.Controllers
{
    public class AlbumController(MusicDbContext context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var albums = await context.Albums.AsNoTracking().ToListAsync();
            return View(albums);
        }

        public async Task<IActionResult> Details(int id)
        {
            var album = await context.Albums
                .AsNoTracking()
                .Include(album => album.Songs)
                .FirstAsync(x => x.Id == id);
            return View(album);
        }
    }
}
