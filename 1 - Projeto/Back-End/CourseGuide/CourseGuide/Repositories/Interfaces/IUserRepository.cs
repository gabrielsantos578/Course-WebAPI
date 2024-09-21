using CourseGuide.Objects.Contracts;
using CourseGuide.Objects.Generics;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces.Generics.Methods;

namespace CourseGuide.Repositories.Interfaces
{
    // Interface para o repositório de usuários, herda de IGetByIdIntRepository para suporte a operações com ID int.
    public interface IUserRepository<T> : IGetByIdIntRepository<T> where T : class, IEntityInt
    {
        Task<UserModel> GetByEmail(string email);  // Método para buscar um usuário pelo email.
        Task<UserModel> Login(Login login);        // Método para autenticar um usuário com credenciais de login.
    }
}
