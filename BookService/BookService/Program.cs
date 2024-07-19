using BookService.context;
using Microsoft.EntityFrameworkCore;

namespace BookService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<BookContext>(x => x.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
