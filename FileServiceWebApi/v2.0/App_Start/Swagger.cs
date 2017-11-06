using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace FileServiceWebApi.App_Start
{
    public static class Swagger
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Title = "File Service Web API",
                        Version = "v1",
                        Description = "A simple example ASP.NET Core File Service Web API",
                    });
                    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                    var xmlPath = Path.Combine(basePath, "FileServiceWebApi.xml");
                    c.IncludeXmlComments(xmlPath);
                });
                
            return services;
        }

        public static IApplicationBuilder RegisterSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "File Service Web API V1");
            });

            return app;
        }
    }
}