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
            CreateMap<ReservationDTO, ReservationModel>().ReverseMap();

            // Entidades de Restaurante:
            CreateMap<RestaurantDTO, RestaurantModel>().ReverseMap();
            CreateMap<TableDTO, TableModel>().ReverseMap();
        }
    }
}
