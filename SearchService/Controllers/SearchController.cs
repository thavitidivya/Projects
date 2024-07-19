using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchService.Context;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IBookServiceclient _bookServiceClient;

        public SearchController(IBookServiceclient bookServiceClient)
        {
            _bookServiceClient = bookServiceClient;
        }
        [HttpGet("books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksfrombookservice()
        {
            try
            {
                var books = await _bookServiceClient.GetBooksAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching books:{ex.Message}");
            }
        }

        [HttpGet("byTitle")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchByTitle(string title)
        {
            var books = await _bookServiceClient.GetBooksAsync();
            var filteredBooks = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
            return Ok(filteredBooks);
        }

        [HttpGet("byAuthor")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchByAuthor(string author)
        {
            var books = await _bookServiceClient.GetBooksAsync();
            var filteredBooks = books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
            return Ok(filteredBooks);
        }

        [HttpGet("byGenre")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchByGenre(string genre)
        {
            var books = await _bookServiceClient.GetBooksAsync();
            var filteredBooks = books.Where(b => b.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));
            return Ok(filteredBooks);
        }






    }
}
    
