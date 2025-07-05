using Music.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
    }
}
