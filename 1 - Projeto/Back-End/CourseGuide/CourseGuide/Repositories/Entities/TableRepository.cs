using CourseGuide.Contexts;
using CourseGuide.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Entities.Generics.Methods;

namespace CourseGuide.Repositories.Entities
{
    // Implementação do repositório de mesas, herda de GetByIdIntRepository para suporte a ID int.
    public class TableRepository : GetByIdIntRepository<TableModel>, ITableRepository<TableModel>
    {
        // Construtor que inicializa o contexto do banco de dados.
        public TableRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        // Método para buscar mesas pelo restaurante.
        public async Task<IEnumerable<TableModel>> GetByRestaurant(int idRestaurant)
        {
            // Busca as mesas com o id do restaurante fornecido.
            return await _dbSet.AsNoTracking().Where(t => t.IdRestaurant == idRestaurant).ToListAsync();
        }
    }
}
