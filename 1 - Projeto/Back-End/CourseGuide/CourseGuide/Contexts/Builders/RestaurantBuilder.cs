using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;

namespace CourseGuide.Contexts.Builders
{
    public class RestaurantBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Configuração da entidade RestaurantModel no Entity Framework.
            modelBuilder.Entity<RestaurantModel>().Property(r => r.Id); // Propriedade do ID do restaurante.
            modelBuilder.Entity<RestaurantModel>().Property(r => r.NameRestaurant).HasMaxLength(100).IsRequired(); // Nome do restaurante, obrigatório.
            modelBuilder.Entity<RestaurantModel>().Property(r => r.EmailRestaurant).HasMaxLength(200).IsRequired(); // E-mail do restaurante, obrigatório.
            modelBuilder.Entity<RestaurantModel>().Property(r => r.PhoneRestaurant).HasMaxLength(15).IsRequired(); // Telefone do restaurante, obrigatório.

            modelBuilder.Entity<RestaurantModel>().HasKey(r => r.Id); // Define a chave primária como o ID.

            // Configuração do relacionamento: Usuário -> Restaurantes
            modelBuilder.Entity<RestaurantModel>()
                .HasOne(b => b.OwnerModel) // Um restaurante possui um dono.
                .WithMany(c => c.RestaurantsModel) // Um dono pode ter muitos restaurantes.
                .HasForeignKey(b => b.IdOwner) // Chave estrangeira referenciando o dono.
                .OnDelete(DeleteBehavior.Cascade); // Deleção em cascata se o dono for removido.
        }
    }
}
