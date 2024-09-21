using CourseGuide.Contexts;
using Microsoft.EntityFrameworkCore;
using CourseGuide.Objects.Generics;
using CourseGuide.Repositories.Interfaces.Generics;

namespace CourseGuide.Repositories.Entities.Generics
{
    // Repositório genérico para entidades que usam Guid como identificador.
    public class GuidRepository<T> : GenericRepository<T>, IGuidRepository<T> where T : class, IEntityGuid
    {
        protected readonly AppDBContext _dbContext;  // Contexto do banco de dados.
        protected readonly DbSet<T> _dbSet;  // Conjunto de entidades do tipo T.

        // Construtor que recebe o contexto do banco de dados e inicializa o conjunto de entidades.
        public GuidRepository(AppDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();  // Inicializa o conjunto de entidades.
        }

        // Método para obter uma entidade pelo seu ID do tipo Guid.
        public virtual async Task<T> GetById(Guid id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);  // Busca a entidade sem rastreamento.
        }
    }
}
