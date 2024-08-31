using AutoMapper;
using CourseGuide.Objects.Contracts;
using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces;
using CourseGuide.Services.Interfaces;

namespace CourseGuide.Services.Entities
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var usersModel = await _userRepository.GetAll();

            usersModel.ToList().ForEach(u => u.PasswordUser = "");
            return _mapper.Map<IEnumerable<UserDTO>>(usersModel);
        }

        public async Task<UserDTO> GetById(int id)
        {
            var userModel = await _userRepository.GetById(id);

            if (userModel != null) userModel.PasswordUser = "";
            return _mapper.Map<UserDTO>(userModel);
        }

        public async Task<UserDTO> GetByEmail(string email)
        {
            var userModel = await _userRepository.GetByEmail(email);

            if (userModel != null) userModel.PasswordUser = "";
            return _mapper.Map<UserDTO>(userModel);
        }

        public async Task<UserDTO> Login(Login login)
        {
            var userModel = await _userRepository.Login(login);

            if (userModel != null) userModel.PasswordUser = "";
            return _mapper.Map<UserDTO>(userModel);
        }

        public async Task Create(UserDTO userDTO)
        {
            var userModel = _mapper.Map<UserModel>(userDTO);
            await _userRepository.Create(userModel);

            userDTO.Id = userModel.Id;
            userDTO.PasswordUser = "";
        }

        public async Task Update(UserDTO userDTO)
        {
            var userModel = _mapper.Map<UserModel>(userDTO);
            await _userRepository.Update(userModel);

            userDTO.PasswordUser = "";
        }

        public async Task Delete(UserDTO userDTO)
        {
            var userModel = _mapper.Map<UserModel>(userDTO);
            await _userRepository.Delete(userModel);

            userDTO.PasswordUser = "";
        }
    }
}