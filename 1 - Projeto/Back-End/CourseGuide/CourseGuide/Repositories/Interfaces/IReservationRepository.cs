using CourseGuide.Objects.Generics;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces.Generics.Methods;

namespace CourseGuide.Repositories.Interfaces
{
    // Interface para o repositório de reservas, herda de IGetByIdIntRepository para suporte a operações com ID int.
    public interface IReservationRepository<T> : IGetByIdIntRepository<T> where T : class, IEntityInt
    {
        Task<IEnumerable<ReservationModel>> GetReservationsRelatedUser(int idUser);
        Task<IEnumerable<ReservationModel>> GetReservationsRelatedTable(int idTable);
    }
}
