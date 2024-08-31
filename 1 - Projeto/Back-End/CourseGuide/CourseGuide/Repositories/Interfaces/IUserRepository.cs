using CourseGuide.Objects.Contracts;
using CourseGuide.Objects.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseGuide.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetById(int id);
        Task<UserModel> GetByEmail(string email);
        Task<UserModel> Login(Login login);
        Task<UserModel> Create(UserModel userModel);
        Task<UserModel> Update(UserModel userModel);
        Task<UserModel> Delete(UserModel userModel);
    }
}