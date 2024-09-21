using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;

namespace CourseGuide.Contexts.Builders
{
    public class ReservationBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Configuração da entidade ReservationModel
            modelBuilder.Entity<ReservationModel>().Property(r => r.Id);
            modelBuilder.Entity<ReservationModel>().Property(r => r.DateReservation).HasMaxLength(16).IsRequired();
            modelBuilder.Entity<ReservationModel>().Property(r => r.DateFinish).HasMaxLength(16).IsRequired();
            modelBuilder.Entity<ReservationModel>().Property(r => r.TimeDuration).HasMaxLength(23).IsRequired();
            modelBuilder.Entity<ReservationModel>().Property(r => r.ValueReservation).HasColumnType("decimal(8,2)").IsRequired();
            modelBuilder.Entity<ReservationModel>().Property(r => r.Status).IsRequired();
            modelBuilder.Entity<ReservationModel>().Property(r => r.IdUser).IsRequired();
            modelBuilder.Entity<ReservationModel>().Property(r => r.IdTable).IsRequired();
            modelBuilder.Entity<ReservationModel>().Property(r => r.CreateAt).HasMaxLength(16).IsRequired();
            modelBuilder.Entity<ReservationModel>().Property(r => r.UpdateAt).HasMaxLength(16).IsRequired();

            modelBuilder.Entity<ReservationModel>().HasKey(r => r.Id); // Define a chave primária como o ID.

            // Relacionamento: Usuário -> Reserva
            modelBuilder.Entity<ReservationModel>()
                .HasOne(r => r.UserModel)
                .WithMany(u => u.ReservationsModel)
                .HasForeignKey(r => r.IdUser)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento: Mesa -> Reserva
            modelBuilder.Entity<ReservationModel>()
                .HasOne(r => r.TableModel)
                .WithMany(t => t.ReservationsModel)
                .HasForeignKey(r => r.IdTable)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}