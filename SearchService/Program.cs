using Microsoft.EntityFrameworkCore;
using SearchService.Context;
using SearchService.Services;

namespace SearchService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<SearchContext>(x => x.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
            builder.Services.AddHttpClient<IBookServiceclient, BookServiceClient>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:5075");
            });
           // builder.Services.AddScoped<IBookServiceclient, BookServiceClient>();
            var app = builder.Build();
           
            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.UseRouting();
            

            app.Run();
        }
    }
}
