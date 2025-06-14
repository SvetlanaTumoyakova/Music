using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class SongRepository(MusicDbContext context): ISongRepository
    {
        public async Task<Album> GetAllAsync()
        {
            var album = await context.Albums
                .AsNoTracking()
                .Include(album => album.Songs)
                .FirstAsync(x => x.Id == 1);
            return album;
        }
    }
}
