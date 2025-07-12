using Music.Models;

namespace Music.Data.Repositories.Interfaces
{
    public interface ISearchRepository
    {
        Task<List<T>> SearchAsync<T>(string name) where T : class, ISearchable;
        Task<List<Album>> SearchAlbumsByArtistIdAsync(int artistId, string name);
    }
}
