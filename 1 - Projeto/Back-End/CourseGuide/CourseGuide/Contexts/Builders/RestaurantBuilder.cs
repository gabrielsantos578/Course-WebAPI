using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;

namespace CourseGuide.Contexts.Builders
{
    public class RestaurantBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Configuração da entidade RestaurantModel no Entity Framework.
            modelBuilder.Entity<RestaurantModel>().Property(u => u.Id); // Propriedade do id.
            modelBuilder.Entity<RestaurantModel>().Property(u => u.NameRestaurant).HasMaxLength(100).IsRequired(); // Nome do usuário, obrigatório.
            modelBuilder.Entity<RestaurantModel>().Property(u => u.EmailRestaurant).HasMaxLength(200).IsRequired(); // E-mail do usuário, obrigatório.
            modelBuilder.Entity<RestaurantModel>().Property(u => u.PhoneRestaurant).HasMaxLength(15).IsRequired(); // Telefone do usuário, obrigatório.

            modelBuilder.Entity<RestaurantModel>().HasKey(u => u.Id); // Define a chave primária.
        }
    }
}
