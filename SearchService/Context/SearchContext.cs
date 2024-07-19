using Microsoft.EntityFrameworkCore;
using SearchService.Models;

namespace SearchService.Context
{
    public class SearchContext:DbContext
    {
        public SearchContext(DbContextOptions<SearchContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}

