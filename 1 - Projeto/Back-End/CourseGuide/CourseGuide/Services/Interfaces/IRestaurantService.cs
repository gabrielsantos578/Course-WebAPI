using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Services.Interfaces.Generics;

namespace CourseGuide.Services.Interfaces
{
    // Interface de serviço para operações relacionadas a usuários, estendendo IIntService.
    public interface IRestaurantService : IIntService<RestaurantDTO, RestaurantModel>
    {
        Task<IEnumerable<RestaurantDTO>> GetByOwner(int idOwner);  // Método para buscar restaurantes pelo dono.
    }
}
