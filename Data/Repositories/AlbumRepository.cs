using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class AlbumRepository(MusicDbContext context): IAlbumRepository
    {
        public async Task<List<Album>> GetAllAsync()
        {
            var albums = await context.Albums.AsNoTracking().ToListAsync();
            return albums;
        }
        public async Task<Album> GetByIdAsync(int id)
        {
            var album = await context.Albums
           .Include(album => album.Artists)
           .FirstOrDefaultAsync(album => album.Id == id);
            return album;
        }
        public async Task<Album> GetDetailsByIdAsync(int id)
        {
            var album = await context.Albums
                .AsNoTracking()
                .Include(album => album.Songs)
                .FirstAsync(x => x.Id == id);
            return album;
        }
        public async Task AddAsync(Album album)
        {
            context.Albums.Add(album);
            await context.SaveChangesAsync();
        }

        public async Task EditAsync(Album album)
        {
            if (album != null)
            {
                context.Albums.Update(album);
                await context.SaveChangesAsync();
            }
        }
        public async Task RemoveByIdAsync(int id)
        {
            var album = await GetByIdAsync(id);
            if (album != null)
            {
                context.Albums.Remove(album);
                await context.SaveChangesAsync();
            }
        }
    }
}
