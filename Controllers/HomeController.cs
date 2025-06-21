using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers
{
    public class HomeController(IArtistRepository artistRepository) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var artists = await artistRepository.GetAllAsync();
            return View(artists);
        }

        [HttpPost]
        public IActionResult Create(Artist artist)
        {
            artistRepository.Add(artist);
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
