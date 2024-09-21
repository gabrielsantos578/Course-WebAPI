using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Services.Interfaces.Generics.Methods;

namespace CourseGuide.Services.Interfaces
{
    // Interface de serviço para operações relacionadas a usuários, estendendo IGetByIdIntService.
    public interface IRestaurantService : IGetByIdIntService<RestaurantDTO, RestaurantModel>
    {
        Task<IEnumerable<RestaurantDTO>> GetByOwner(int idOwner);  // Método para buscar restaurantes pelo dono.
    }
}
