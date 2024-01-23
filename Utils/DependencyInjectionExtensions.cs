using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;

namespace Utils;

public static class DependencyInjectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}