
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Implementation;
using UrlShortener.Interface;

namespace UrlShortener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<UrlShortenerContext>(
               options =>
               {
                   //options.UseLazyLoadingProxies();
                   options.UseSqlServer("Data Source=DESKTOP-DM3DDUO\\SQLEXPRESS;Initial Catalog=urlshortener;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", s =>
                   {
                       //  s.MigrationsAssembly("");
                       s.EnableRetryOnFailure(3);
                   });
               });
            builder.Services.AddTransient<IUrlService, UrlService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
