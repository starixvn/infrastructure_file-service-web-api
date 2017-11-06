using FileServiceWebApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FileServiceWebApi.App_Start
{
    public static class RegisterService
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<FileService, FileServiceImp>();
            return services;
        }
    }
}