using CourseGuide.Contexts;
using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Generics;
using CourseGuide.Repositories.Interfaces.Generics.Methods;

namespace CourseGuide.Repositories.Entities.Generics.Methods
{
    // Repositório genérico para entidades que usam int como identificador.
    public class GetByIdIntRepository<T> : GenericRepository<T>, IGetByIdIntRepository<T> where T : class, IEntityInt
    {
        protected readonly AppDBContext _dbContext;  // Contexto do banco de dados.
        protected readonly DbSet<T> _dbSet;  // Conjunto de entidades do tipo T.

        // Construtor que recebe o contexto do banco de dados e inicializa o conjunto de entidades.
        public GetByIdIntRepository(AppDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();  // Inicializa o conjunto de entidades.
        }

        // Método para obter uma entidade pelo seu ID do tipo int.
        public virtual async Task<T> GetById(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);  // Busca a entidade sem rastreamento.
        }
    }
}
