
using Microsoft.OpenApi.Models;

namespace tp02_26003_api
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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebApi",
                    Version = "v1",
                    Description = "Exemplo Web Api com documentação OpenAPI",
                    Contact = new OpenApiContact
                    {
                        Name = "Integração de Sistemas de Informação 2023/24",
                        Email = string.Empty,
                        Url = new Uri("https://www.ipca.pt/"),
                    },
                });
                // -------------------------------------------------------------------------
                var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "WebApi.xml");
                c.IncludeXmlComments(filePath);
                //---------------------------------------------------------------------------
            });
        

        var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
