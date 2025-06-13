using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers
{
    public class AlbumController(IAlbumRepository albumRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var albums = await albumRepository.GetAllAsync();
            return View(albums);
        }

        public async Task<IActionResult> Details(int id)
        {
            var album = await albumRepository.GetDetailsByIdAsync(id);
            return View(album);
        }
    }
}
