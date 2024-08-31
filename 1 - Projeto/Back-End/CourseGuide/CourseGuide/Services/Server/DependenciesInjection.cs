using CourseGuide.Repositories.Entities;
using CourseGuide.Repositories.Interfaces;
using CourseGuide.Services.Entities;
using CourseGuide.Services.Interfaces;

namespace CourseGuide.Services.Server
{
    public static class DependenciesInjection
    {
        public static void AddUserDependencies(this IServiceCollection services)
        {
            // Dependência: Usuário
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}