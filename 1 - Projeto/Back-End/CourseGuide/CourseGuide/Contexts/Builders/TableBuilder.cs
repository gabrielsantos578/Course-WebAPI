using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;

namespace CourseGuide.Contexts.Builders
{
    public class TableBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Configuração da entidade TableModel no Entity Framework.
            modelBuilder.Entity<TableModel>().Property(t => t.Id); // Propriedade do ID do restaurante.
            modelBuilder.Entity<TableModel>().Property(t => t.CodeTable).HasMaxLength(20).IsRequired(); // Nome do restaurante, obrigatório.
            modelBuilder.Entity<TableModel>().Property(t => t.CapacityTable).HasMaxLength(2).IsRequired(); // E-mail do restaurante, obrigatório.
            modelBuilder.Entity<TableModel>().Property(t => t.LocationTable).HasMaxLength(50).IsRequired(); // Telefone do restaurante, obrigatório.
            modelBuilder.Entity<TableModel>().Property(t => t.AvailableTable).IsRequired();
            modelBuilder.Entity<TableModel>().Property(t => t.ValueTable).IsRequired(); // Telefone do restaurante, obrigatório.

            modelBuilder.Entity<TableModel>().HasKey(t => t.Id); // Define a chave primária como o ID.

            // Configuração do relacionamento: Usuário -> Tablees
            modelBuilder.Entity<TableModel>()
                .HasOne(t=> t.RestaurantModel) // Um restaurante possui um dono.
                .WithMany(r => r.TablesModel) // Um dono pode ter muitos restaurantes.
                .HasForeignKey(t => t.IdRestaurant) // Chave estrangeira referenciando o dono.
                .OnDelete(DeleteBehavior.Cascade); // Deleção em cascata se o dono for removido.
        }
    }
}
