using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Music.Data;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using Music.Uploadcare;
using Music.ViewModel;
using Uploadcare;
using Uploadcare.Upload;

namespace Music.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IPhotoRepository _photoRepository;
        public HomeController(IArtistRepository artistRepository, IPhotoRepository photoRepository)
        {
            _artistRepository = artistRepository;
            _photoRepository = photoRepository;
        }
        public async Task<IActionResult> Index()
        {
            var artists = await _artistRepository.GetAllAsync();
            return View(artists);
        }
        public async Task<IActionResult> Details(int id)
        {
            var artist = await _artistRepository.GetArtistDetailsByIdAsync(id);
            return View(artist);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArtistViewModel artistViewModel)
        {
            var urlImg =  await _photoRepository.UploadPhotoAsync(artistViewModel.File);
            var artist = new Artist
            {
                Name = artistViewModel.Name,
                UrlImg = urlImg,
            };
            _artistRepository.Add(artist);
            return RedirectToAction(nameof(Index), "Home");
        }
        public IActionResult Edit(int id)
        {
            var artist = _artistRepository.GetById(id);
            if(artist == null)
            {
                return NotFound();
            }

            var atistViewModel = new ArtistViewModel
            {
                Id = artist.Id,
                Name = artist.Name,
            };

            return View(atistViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ArtistViewModel artistViewModel)
        {
            var editArtist = _artistRepository.GetById(artistViewModel.Id);
            if (editArtist == null)
            {
                return NotFound();
            }

            editArtist.Name = artistViewModel.Name;

            if (artistViewModel.File != null)
            {
                var urlImg =  await _photoRepository.UploadPhotoAsync(artistViewModel.File);
                editArtist.UrlImg = urlImg;
            }
            _artistRepository.Edit(editArtist);
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _artistRepository.RemoveById(id);
            return RedirectToAction(nameof(Index), "Home");
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
