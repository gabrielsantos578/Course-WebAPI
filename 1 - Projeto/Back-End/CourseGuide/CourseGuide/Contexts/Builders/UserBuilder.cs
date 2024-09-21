using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;

namespace CourseGuide.Contexts.Builders
{
    public class UserBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Configuração da entidade UserModel no Entity Framework.
            modelBuilder.Entity<UserModel>().Property(u => u.Id); // Propriedade do id.
            modelBuilder.Entity<UserModel>().Property(u => u.ImageProfile); // Propriedade opcional para imagem de perfil.
            modelBuilder.Entity<UserModel>().Property(u => u.NameUser).HasMaxLength(100).IsRequired(); // Nome do usuário, obrigatório.
            modelBuilder.Entity<UserModel>().Property(u => u.EmailUser).HasMaxLength(200).IsRequired(); // E-mail do usuário, obrigatório.
            modelBuilder.Entity<UserModel>().Property(u => u.PasswordUser).HasMaxLength(256).IsRequired(); // Senha do usuário, obrigatória.
            modelBuilder.Entity<UserModel>().Property(u => u.PhoneUser).HasMaxLength(15).IsRequired(); // Telefone do usuário, obrigatório.

            modelBuilder.Entity<UserModel>().HasKey(u => u.Id); // Define a chave primária.

            // Inserção de dados iniciais na tabela de Usuários.
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1, // ID do usuário.
                    NameUser = "Master", // Nome do usuário.
                    EmailUser = "master@development.com", // E-mail do usuário.
                    PasswordUser = "99db87c3278f5eaa517260eaaa2b4b376be63d7f8a79c0f43992a493a3de8fc9", // Senha já hasheada, utilizando SHA256.
                    PhoneUser = "", // Telefone (opcional).
                    ImageProfile = "" // Imagem de perfil (opcional).
                }
            );
        }
    }
}
