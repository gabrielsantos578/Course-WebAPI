using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Services.Interfaces.Generics.Methods;

namespace CourseGuide.Services.Interfaces
{
    // Interface de serviço para operações relacionadas a usuários, estendendo IGetByIdIntService.
    public interface ITableService : IGetByIdIntService<TableDTO, TableModel>
    {
        Task<IEnumerable<TableDTO>> GetByRestaurant(int idRestaurant);  // Método para buscar mesas pelo restaurante.
    }
}
