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
        public Album GetById(int id)
        {
            var album = context.Albums
           .Include(album => album.Artists)
           .FirstOrDefault(album => album.Id == id);
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
        public void Add(Album album)
        {
            context.Albums.Add(album);
            context.SaveChanges();
        }

        public void Edit(Album album)
        {
            if (album != null)
            {
                context.Albums.Update(album);
                context.SaveChanges();
            }
        }
        public void RemoveById(int id)
        {
            var album = GetById(id);
            if (album != null)
            {
                context.Albums.Remove(album);
                context.SaveChanges();
            }
        }
    }
}
