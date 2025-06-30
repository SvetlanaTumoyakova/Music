using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Models;
using Music.ViewModel;

namespace Music.Data.Repositories.Interfaces
{
    public interface IAlbumRepository
    {
        Task<List<Album>> GetAllAsync();
        Album GetById(int id);
        Task<Album> GetDetailsByIdAsync(int id);
        void Add(Album album);
        void Edit(Album album);
        void RemoveById(int id);
    }
}
