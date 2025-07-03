using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class ArtistRepository(MusicDbContext context): IArtistRepository
    {
        public async Task<List<Artist>> GetAllAsync()
        {
        var artists = await context.Artists.AsNoTracking().ToListAsync();
        return artists;
        }
        public async Task<Artist> GetByIdAsync(int id)
        {
            var artist = await context.Artists.FirstOrDefaultAsync(artist => artist.Id == id);
            return artist;
        }
        public async Task<Artist> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var artist = await context.Artists.FirstOrDefaultAsync(artist => artist.Name.ToLower() == name.ToLower());

            return artist;
        }
        public async Task AddASync(Artist artist)
        {
           context.Artists.Add(artist);
           await context.SaveChangesAsync();
        }
        public async Task<Artist> GetArtistDetailsByIdAsync(int id)
        {
            var artist = await context.Artists
                .AsNoTracking()
                .Include(artist => artist.Albums)
                .FirstAsync(x => x.Id == id);
            return artist;
        }
        public async Task EditAsync(Artist artist)
        {
            if (artist != null)
            {
                context.Artists.Update(artist);
                await context.SaveChangesAsync();
            }
        }
        public async Task RemoveByIdAsync(int id)
        {
            var artist = await GetByIdAsync(id);
            if (artist != null)
            {
                context.Artists.Remove(artist);
                await context.SaveChangesAsync();
            }
        }
    }
}
