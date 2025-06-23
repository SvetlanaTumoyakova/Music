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
        public void Add(Artist artist)
        {
           context.Artists.Add(artist);
           context.SaveChanges();
        }
        public async Task<Artist> GetArtistDetailsByIdAsync(int id)
        {
            var artist = await context.Artists
                .AsNoTracking()
                .Include(artist => artist.Albums)
                .FirstAsync(x => x.Id == id);
            return artist;
        }
        public Artist GetById(int id)
        {
            var artist = context.Artists.FirstOrDefault(artist => artist.Id == id);
                return artist;
        }
        public void RemoveById(int id)
        {
            var artist = GetById(id);
            if (artist != null)
            {
                context.Artists.Remove(artist);
                context.SaveChanges();
            }
        }
    }
}
