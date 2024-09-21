using AutoMapper;
using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;

namespace CourseGuide.Objects.DTOs.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entidades de Usuário:
            CreateMap<UserDTO, UserModel>().ReverseMap();
        }
    }
}
