using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using Music.ViewModel;

namespace Music.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IArtistRepository _artistRepository;
        public AlbumController(IAlbumRepository albumRepository, IPhotoRepository photoRepository, IArtistRepository artistRepository)
        {
            _albumRepository = albumRepository;
            _photoRepository = photoRepository;
            _artistRepository = artistRepository;
        }
        public async Task<IActionResult> Index()
        {
            var albums = await _albumRepository.GetAllAsync();
            return View(albums);
        }

        public async Task<IActionResult> Details(int id)
        {
            var album = await _albumRepository.GetDetailsByIdAsync(id);
            return View(album);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumViewModel albumViewModel)
        {
            var urlImg = await _photoRepository.UploadPhotoAsync(albumViewModel.File);

            var artist = _artistRepository.GetByName(albumViewModel.ArtistName);

        if (artist == null)
        {
            ModelState.AddModelError("ArtistName", "Исполнитель не найден. Пожалуйста, введите корректное имя исполнителя.");
            return View(albumViewModel);
        }

            var album = new Album
            {
                Name = albumViewModel.Name,
                YearOfIssue = albumViewModel.YearOfIssue,
                UrlImg = urlImg,
                Artists = new List<Artist> { artist }
            };
            _albumRepository.Add(album);
            return RedirectToAction(nameof(Index), "Album");
        }

        public IActionResult Edit(int id)
        {
            var album = _albumRepository.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            var albumViewModel = new AlbumViewModel
            {
                Id = album.Id,
                Name = album.Name,
                YearOfIssue = album.YearOfIssue,
                ArtistName = album.Artists.FirstOrDefault().Name
            };

            return View(albumViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AlbumViewModel albumViewModel)
        {
            var editAlbum = _albumRepository.GetById(albumViewModel.Id);
            if (editAlbum == null)
            {
                return NotFound();
            }

            editAlbum.Name = albumViewModel.Name;
            editAlbum.YearOfIssue = albumViewModel.YearOfIssue;

            if (albumViewModel.File != null)
            {
                var urlImg = await _photoRepository.UploadPhotoAsync(albumViewModel.File);
                editAlbum.UrlImg = urlImg;
            }

            var artist = _artistRepository.GetByName(albumViewModel.ArtistName);
            if (artist == null)
            {
                ModelState.AddModelError("ArtistName", "Исполнитель не найден. Пожалуйста, введите корректное имя исполнителя.");
                return View(albumViewModel);
            }

            editAlbum.Artists = new List<Artist> { artist };

            _albumRepository.Edit(editAlbum);
            return RedirectToAction(nameof(Index), "Album");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _albumRepository.RemoveById(id);
            return RedirectToAction(nameof(Index), "Album");
        }
    }
}
