using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers
{
    public class HomeController(IArtistRepository artistRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var artists = await artistRepository.GetAllAsync();
            return View(artists);
        }
        public async Task<IActionResult> Details(int id)
        {
            var artist = await artistRepository.GetArtistDetailsByIdAsync(id);
            return View(artist);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
