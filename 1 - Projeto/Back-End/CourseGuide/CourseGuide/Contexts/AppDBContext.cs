using CourseGuide.Contexts.Builders;
using CourseGuide.Objects.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseGuide.Contexts
{
    public class AppDBContext : DbContext
    {
        // Mapeamento Relacional dos Objetos no Bando de Dados
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        // Conjunto: Usuário
        public DbSet<UserModel> Users { get; set; }

        // Conjunto: Restaurante
        public DbSet<RestaurantModel> Restaurants { get; set; }

        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entidades de Usuário:
            UserBuilder.Build(modelBuilder);

            // Entidades de Restaurante:
            RestaurantBuilder.Build(modelBuilder);
        }
    }
}
