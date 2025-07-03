using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Models;
using Music.ViewModel;

namespace Music.Data.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAllAsync();
        Task<Album> GetByIdAsync(int id);
        Task<Album> GetDetailsByIdAsync(int id);
        Task AddAsync(Album album);
        Task EditAsync(Album album);
        Task RemoveByIdAsync(int id);
    }
}
