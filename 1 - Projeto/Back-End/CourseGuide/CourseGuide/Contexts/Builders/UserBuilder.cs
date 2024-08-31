using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;

namespace CourseGuide.Contexts.Builders
{
    public class UserBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Builder
            modelBuilder.Entity<UserModel>().HasKey(u => u.Id);
            modelBuilder.Entity<UserModel>().Property(u => u.ImageProfile);
            modelBuilder.Entity<UserModel>().Property(u => u.NameUser).HasMaxLength(100).IsRequired();
            modelBuilder.Entity<UserModel>().Property(u => u.EmailUser).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<UserModel>().Property(u => u.PasswordUser).HasMaxLength(256).IsRequired();
            modelBuilder.Entity<UserModel>().Property(u => u.PhoneUser).HasMaxLength(15).IsRequired();


            // Inserções
            modelBuilder.Entity<UserModel>().HasData(
                new UserModel
                {
                    Id = 1,
                    NameUser = "Master",
                    EmailUser = "master@development.com",
                    PasswordUser = "99db87c3278f5eaa517260eaaa2b4b376be63d7f8a79c0f43992a493a3de8fc9",
                    PhoneUser = "",
                    ImageProfile = ""
                }
            );
        }
    }

}