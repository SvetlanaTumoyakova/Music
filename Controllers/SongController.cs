using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongRepository _songRepository;
        private readonly ISearchRepository _searchRepository;
        public SongController(ISongRepository songRepository, ISearchRepository searchRepository)
        {
            _songRepository = songRepository;
            _searchRepository = searchRepository;
        }
        public async Task<IActionResult> Index()
        {
            var song = await _songRepository.GetAllAsync();
            return View(song);
        }


        [HttpPost]
        public async Task<IActionResult> Index(string name)
        {
            List<Song> foundSong;
            if (string.IsNullOrWhiteSpace(name))
            {
                foundSong = await _songRepository.GetAllAsync();
            }
            else
            {
                foundSong = await _searchRepository.SearchAsync<Song>(name);
            }
            return View(foundSong);
        }


    }
}
