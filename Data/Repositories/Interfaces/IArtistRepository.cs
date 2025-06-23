using Microsoft.EntityFrameworkCore;
using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync();
        Task<Artist> GetArtistDetailsByIdAsync(int id);
        public void Add(Artist artist);
        Artist GetById(int id);
        void RemoveById(int id);
    }
}
