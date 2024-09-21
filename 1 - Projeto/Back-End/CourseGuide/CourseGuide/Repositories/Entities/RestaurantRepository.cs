using CourseGuide.Contexts;
using CourseGuide.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Entities.Generics.Methods;

namespace CourseGuide.Repositories.Entities
{
    // Implementação do repositório de restaurantes, herda de GetByIdIntRepository para suporte a ID int.
    public class RestaurantRepository : GetByIdIntRepository<RestaurantModel>, IRestaurantRepository<RestaurantModel>
    {
        // Construtor que inicializa o contexto do banco de dados.
        public RestaurantRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        // Método para buscar restaurantes pelo dono.
        public async Task<IEnumerable<RestaurantModel>> GetByOwner(int idOwner)
        {
            // Busca os restaurantes com o id do dono fornecido.
            return await _dbSet.AsNoTracking().Where(r => r.IdOwner == idOwner).ToListAsync();
        }
    }
}
