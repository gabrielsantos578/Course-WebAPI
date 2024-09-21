using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;

namespace CourseGuide.Contexts.Builders
{
    public class TableBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Configuração da entidade TableModel no Entity Framework.
            modelBuilder.Entity<TableModel>().Property(r => r.Id); // Propriedade do ID do restaurante.
            modelBuilder.Entity<TableModel>().Property(r => r.NumberTable).HasMaxLength(20).IsRequired(); // Nome do restaurante, obrigatório.
            modelBuilder.Entity<TableModel>().Property(r => r.CapacityTable).HasMaxLength(2).IsRequired(); // E-mail do restaurante, obrigatório.
            modelBuilder.Entity<TableModel>().Property(r => r.LocationTable).HasMaxLength(50).IsRequired(); // Telefone do restaurante, obrigatório.
            modelBuilder.Entity<TableModel>().Property(r => r.ValueTable).IsRequired(); // Telefone do restaurante, obrigatório.

            modelBuilder.Entity<TableModel>().HasKey(r => r.Id); // Define a chave primária como o ID.

            // Configuração do relacionamento: Usuário -> Tablees
            modelBuilder.Entity<TableModel>()
                .HasOne(b => b.RestaurantModel) // Um restaurante possui um dono.
                .WithMany(c => c.TablesModel) // Um dono pode ter muitos restaurantes.
                .HasForeignKey(b => b.IdRestaurant) // Chave estrangeira referenciando o dono.
                .OnDelete(DeleteBehavior.Cascade); // Deleção em cascata se o dono for removido.
        }
    }
}
