using CourseGuide.Contexts;
using CourseGuide.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Entities.Generics.Methods;

namespace CourseGuide.Repositories.Entities
{
    // Implementação do repositório de reservas, herda de GetByIdIntRepository para suporte a ID int.
    public class ReservationRepository : GetByIdIntRepository<TableModel>, IReservationRepository<TableModel>
    {
        // Construtor que inicializa o contexto do banco de dados.
        public ReservationRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ReservationModel>> GetReservationsRelatedUser(int idUser)
        {
            return await _dbContext.Reservations.AsNoTracking().Where(r => r.IdUser == idUser).ToListAsync();
        }

        public async Task<IEnumerable<ReservationModel>> GetReservationsRelatedTable(int idTable)
        {
            return await _dbContext.Reservations.AsNoTracking().Where(r => r.IdTable == idTable).ToListAsync();
        }
    }
}
