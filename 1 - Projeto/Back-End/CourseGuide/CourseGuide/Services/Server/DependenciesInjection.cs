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
            services.AddScoped<IUserService, UserService>();  // Registra o serviço de usuários como uma dependência Scoped.

            // Dependências: Restaurante
            services.AddScoped<IRestaurantRepository<RestaurantModel>, RestaurantRepository>();  // Registra o repositório de restaurantes como uma dependência Scoped.
            services.AddScoped<IRestaurantService, RestaurantService>();  // Registra o serviço de restaurantes como uma dependência Scoped.

            // Dependências: Mesa
            services.AddScoped<ITableRepository<TableModel>, TableRepository>();  // Registra o repositório de restaurantes como uma dependência Scoped.
            services.AddScoped<ITableService, TableService>();  // Registra o serviço de restaurantes como uma dependência Scoped.

            // Dependências: Reserva
            services.AddScoped<IReservationRepository<ReservationModel>, ReservationRepository>();  // Registra o repositório de restaurantes como uma dependência Scoped.
            services.AddScoped<IReservationService, ReservationService>();  // Registra o serviço de restaurantes como uma dependência Scoped.
        }
    }
}
