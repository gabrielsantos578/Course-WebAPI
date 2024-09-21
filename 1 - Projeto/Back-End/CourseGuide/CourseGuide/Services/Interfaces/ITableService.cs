using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Services.Interfaces.Generics;

namespace CourseGuide.Services.Interfaces
{
    // Interface de serviço para operações relacionadas a usuários, estendendo IIntService.
    public interface ITableService : IIntService<TableDTO, TableModel>
    {
        Task<IEnumerable<TableDTO>> GetByRestaurant(int idRestaurant);  // Método para buscar mesas pelo restaurante.
    }
}
