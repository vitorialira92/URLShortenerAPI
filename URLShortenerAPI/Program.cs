
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using URLShortenerAPI.Data;
using URLShortenerAPI.Filters;
using URLShortenerAPI.Repositories;
using URLShortenerAPI.Services;

namespace URLShortenerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUrlRepository, UrlRepository>();
            builder.Services.AddScoped<UrlService>();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
            });

            builder.Services.AddDbContext<ApiContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("UrlShortenerSqlite"));
            });

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
