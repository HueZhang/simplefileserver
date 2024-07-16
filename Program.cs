
using Microsoft.Extensions.FileProviders;

namespace FileServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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

            var path = AppContext.BaseDirectory + "/wwwroot";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var physicalFileProvider = new PhysicalFileProvider(path);

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = physicalFileProvider
            });

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
