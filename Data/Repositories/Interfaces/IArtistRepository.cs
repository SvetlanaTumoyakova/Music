using Microsoft.EntityFrameworkCore;
using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync();
        Artist GetById(int id);
        Artist GetByName(string name);
        Task<Artist> GetArtistDetailsByIdAsync(int id);
        void Add(Artist artist);
        void Edit(Artist artist);
        void RemoveById(int id);
    }
}
