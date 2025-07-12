using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Helper;
using Music.Models;
using Music.ViewModel;

namespace Music.Controllers
{
    public class AlbumController : Controller
    {

        private readonly IAlbumRepository _albumRepository;
        private readonly IArtistRepository _artistRepository;
        private readonly ISearchRepository _searchRepository;
        private readonly IFileRepository _photoRepository;

        public AlbumController(IAlbumRepository albumRepository, IArtistRepository artistRepository, ISearchRepository searchRepository, IFileRepository photoRepository)
        {
            _albumRepository = albumRepository;
            _artistRepository = artistRepository;
            _searchRepository = searchRepository;
            _photoRepository = photoRepository;
        }
        public async Task<IActionResult> Index()
        {
            var albums = await _albumRepository.GetAllAsync();
            return View(albums);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string name)
        {
            List<Album> foundAlbum;     
            if (string.IsNullOrWhiteSpace(name))
            {
                foundAlbum = await _albumRepository.GetAllAsync();
            }
            else
            {
                foundAlbum = await _searchRepository.SearchAsync<Album>(name);
            }
            return View(foundAlbum);
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
            try
            {
                if (albumViewModel.File == null || albumViewModel.File.Length == 0)
                {
                    throw new Exception("Файл не выбран или пустой. Пожалуйста, выберите файл.");
                }

                var urlImg = await _photoRepository.UploadPhotoAsync(albumViewModel.File);
                var artist = await _artistRepository.GetByNameAsync(albumViewModel.ArtistName);

                if (artist == null)
                {
                    throw new Exception("Исполнитель не найден. Пожалуйста, введите корректное имя исполнителя.");
                }

                var album = new Album
                {
                    Name = albumViewModel.Name,
                    YearOfIssue = albumViewModel.YearOfIssue,
                    UrlImg = urlImg,
                    Artists = new List<Artist> { artist }
                };
                await _albumRepository.AddAsync(album);
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(albumViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(albumViewModel);
            }

            var nameController = ControllerHelper.GetName<AlbumController>();
            return RedirectToAction(nameof(Index), nameController);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var album = await _albumRepository.GetByIdAsync(id);
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
            try
            {
                var editAlbum = await _albumRepository.GetByIdAsync(albumViewModel.Id);
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

                var artist = await _artistRepository.GetByNameAsync(albumViewModel.ArtistName);
                if (artist == null)
                {
                    throw new Exception("Исполнитель не найден. Пожалуйста, введите корректное имя исполнителя.");
                }

                editAlbum.Artists = new List<Artist> { artist };

                await _albumRepository.EditAsync(editAlbum);
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(albumViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(albumViewModel);
            }

            var nameController = ControllerHelper.GetName<AlbumController>();
            return RedirectToAction(nameof(Index), nameController);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _albumRepository.RemoveByIdAsync(id);

            var nameController = ControllerHelper.GetName<AlbumController>();
            return RedirectToAction(nameof(Index), nameController);
        }
    }
}
