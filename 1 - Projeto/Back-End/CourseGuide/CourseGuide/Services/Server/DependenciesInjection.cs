using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Entities;
using CourseGuide.Repositories.Interfaces;
using CourseGuide.Services.Entities;
using CourseGuide.Services.Interfaces;

namespace CourseGuide.Services.Server
{
    public static class DependenciesInjection
    {
        // Método de extensão para registrar as dependências relacionadas a usuários.
        public static void InjectDependencies(this IServiceCollection services)
        {
            // Dependências: Usuário
            services.AddScoped<IUserRepository<UserModel>, UserRepository>();  // Registra o repositório de usuários como uma dependência Scoped.
            services.AddScoped<IUserService, UserService>();                   // Registra o serviço de usuários como uma dependência Scoped.
        }
    }
}
