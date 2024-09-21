using AutoMapper;
using CourseGuide.Objects.Contracts;
using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces;
using CourseGuide.Services.Interfaces;
using CourseGuide.Services.Entities.Generics.Methods;

namespace CourseGuide.Services.Entities
{
    // Serviço específico para a entidade User, derivando de GetByIdIntService.
    public class UserService : GetByIdIntService<UserDTO, UserModel>, IUserService
    {
        private readonly IUserRepository<UserModel> _userRepository; // Repositório específico para usuários.

        // Construtor que inicializa o repositório e o mapper.
        public UserService(IUserRepository<UserModel> userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
        }

        // Sobrescreve o método GetAll para não expor a senha dos usuários.
        public override async Task<IEnumerable<UserDTO>> GetAll()
        {
            var users = await base.GetAll();
            foreach (var user in users) { user.PasswordUser = ""; } // Limpa a senha antes de retornar.

            return users;
        }

        // Sobrescreve o método GetById para não expor a senha do usuário.
        public override async Task<UserDTO> GetById(int id)
        {
            var user = await base.GetById(id);
            if (user != null) user.PasswordUser = ""; // Limpa a senha se o usuário existir.

            return user;
        }

        // Método para obter um usuário pelo email, sem expor a senha.
        public async Task<UserDTO> GetByEmail(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user != null) user.PasswordUser = ""; // Limpa a senha se o usuário existir.

            return _mapper.Map<UserDTO>(user); // Mapeia o usuário para um DTO.
        }

        // Método para realizar login, sem expor a senha.
        public async Task<UserDTO> Login(Login login)
        {
            var user = await _userRepository.Login(login);
            if (user != null) user.PasswordUser = ""; // Limpa a senha se o usuário existir.

            return _mapper.Map<UserDTO>(user); // Mapeia o usuário para um DTO.
        }

        // Sobrescreve o método Create para garantir que a senha não seja retornada.
        public override async Task<UserDTO> Create(UserDTO userDTO)
        {
            await base.Create(userDTO);
            userDTO.PasswordUser = ""; // Limpa a senha após a criação.

            return userDTO;
        }

        // Sobrescreve o método Update para garantir que a senha não seja retornada.
        public override async Task<UserDTO> Update(UserDTO userDTO)
        {
            await base.Update(userDTO);
            userDTO.PasswordUser = ""; // Limpa a senha após a atualização.

            return userDTO;
        }

        // Sobrescreve o método Delete para garantir que a senha não seja retornada.
        public override async Task<UserDTO> Delete(UserDTO userDTO)
        {
            await base.Delete(userDTO);
            userDTO.PasswordUser = ""; // Limpa a senha após a exclusão.

            return userDTO;
        }
    }
}
