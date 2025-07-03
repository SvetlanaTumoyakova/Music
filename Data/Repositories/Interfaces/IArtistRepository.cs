using Microsoft.EntityFrameworkCore;
using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<List<Artist>> GetAllAsync();
        Task<Artist> GetByIdAsync(int id);
        Task<Artist> GetByNameAsync(string name);
        Task<Artist> GetArtistDetailsByIdAsync(int id);
        Task AddASync(Artist artist);
        Task EditAsync(Artist artist);
        Task RemoveByIdAsync(int id);
    }
}
