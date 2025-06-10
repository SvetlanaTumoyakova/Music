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
    }
}
