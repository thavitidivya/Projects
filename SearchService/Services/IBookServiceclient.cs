using SearchService.Models;

namespace SearchService.Services
{
    public interface IBookServiceclient
    {
        Task<IEnumerable<Book>> GetBooksAsync();
    }
}

