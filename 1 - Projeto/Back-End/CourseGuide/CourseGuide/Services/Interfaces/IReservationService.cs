using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Services.Interfaces.Generics.Methods;

namespace CourseGuide.Services.Interfaces
{
    // Interface de serviço para operações relacionadas a usuários, estendendo IGetByIdIntService.
    public interface IReservationService : IGetByIdIntService<ReservationDTO, ReservationModel>
    {
        Task<IEnumerable<ReservationDTO>> GetReservationsRelatedUser(int idUser);
        Task<IEnumerable<ReservationDTO>> GetReservationsRelatedTable(int idTable);
    }
}
