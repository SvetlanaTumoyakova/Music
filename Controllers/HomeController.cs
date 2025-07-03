using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Music.Data;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Helper;
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
        private readonly IFileRepository _photoRepository;
        public HomeController(IArtistRepository artistRepository, IFileRepository photoRepository)
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
            try
            {
                var urlImg =  await _photoRepository.UploadPhotoAsync(artistViewModel.File);
                var artist = new Artist
                {
                    Name = artistViewModel.Name,
                    UrlImg = urlImg,
                };
                await _artistRepository.AddASync(artist);
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(artistViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(artistViewModel);
            }
            var nameController = ControllerHelper.GetName<HomeController>();
            return RedirectToAction(nameof(Index), nameController);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
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
            try
            {
                var editArtist = await _artistRepository.GetByIdAsync(artistViewModel.Id);
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
                await _artistRepository.EditAsync(editArtist);
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(artistViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(artistViewModel);
            }

            var nameController = ControllerHelper.GetName<HomeController>();
            return RedirectToAction(nameof(Index), nameController);
        }

        [HttpPost]
        public async Task <IActionResult> Delete(int id)
        {
            await _artistRepository.RemoveByIdAsync(id);

            var nameController = ControllerHelper.GetName<HomeController>();
            return RedirectToAction(nameof(Index), nameController);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
