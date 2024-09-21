using CourseGuide.Objects.Contracts;
using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Services.Interfaces.Generics;

namespace CourseGuide.Services.Interfaces
{
    // Interface de serviço para operações relacionadas a usuários, estendendo IIntService.
    public interface IUserService : IIntService<UserDTO, UserModel>
    {
        Task<UserDTO> GetByEmail(string email);  // Método para obter um usuário pelo seu email, retornando um DTO.
        Task<UserDTO> Login(Login login);  // Método para realizar login de um usuário, retornando um DTO.
    }
}
