using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class SongRepository(MusicDbContext context): ISongRepository
    {
        public async Task<List<Song>> GetAllAsync()
        {
            var songs = await context.Songs.AsNoTracking().ToListAsync();
            return songs;
        }
    }
}
