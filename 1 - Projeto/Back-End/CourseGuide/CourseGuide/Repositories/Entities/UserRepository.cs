using CourseGuide.Contexts;
using CourseGuide.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Objects.Contracts;
using CourseGuide.Repositories.Entities.Generics;

namespace CourseGuide.Repositories.Entities
{
    // Implementação do repositório de usuários, herda de IntRepository para suporte a ID int.
    public class UserRepository : IntRepository<UserModel>, IUserRepository<UserModel>
    {
        // Construtor que inicializa o contexto do banco de dados.
        public UserRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        // Método para buscar um usuário pelo email.
        public async Task<UserModel> GetByEmail(string email)
        {
            // Busca um usuário com o email fornecido.
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.EmailUser == email);
        }

        // Método para autenticar um usuário com credenciais de login.
        public async Task<UserModel> Login(Login login)
        {
            // Busca um usuário com o email e senha fornecidos.
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(u => u.EmailUser == login.Email && u.PasswordUser == login.Password);
        }
    }
}
