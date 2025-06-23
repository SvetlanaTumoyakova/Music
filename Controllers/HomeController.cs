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
        public async Task<IActionResult> Details(int id)
        {
            var artist = await artistRepository.GetArtistDetailsByIdAsync(id);
            return View(artist);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Artist artist)
        {
            artistRepository.Add(artist);
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Edit(int id)
        {
            var artist = artistRepository.GetById(id);
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
/*        public IActionResult Edit(Artist artist)
        {
            var artist = artistRepository.GetById(artist.Id);
            return View(artist);
        }*/
        public IActionResult About()
        {
            return View();
        }
    }
}
