using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories
{
    public class SearchRepository(MusicDbContext context) : ISearchRepository
    {
        public async Task<List<T>> SearchAsync<T>(string name) where T : class, ISearchable
        {
            return await context.Set<T>().Where(x=> x.Name.ToLower()
                                                        .Contains(name.ToLower()))
                                         .ToListAsync();
        }

        public async Task<List<Album>> SearchAlbumsByArtistIdAsync(int artistId, string name)
        {
            return await context.Albums
                .Where(album => album.Artists.Any(a => a.Id == artistId) && album.Name.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }
    }
}
