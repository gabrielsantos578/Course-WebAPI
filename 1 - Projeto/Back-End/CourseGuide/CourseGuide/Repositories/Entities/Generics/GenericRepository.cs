using CourseGuide.Contexts;
using Microsoft.EntityFrameworkCore;
using CourseGuide.Repositories.Interfaces.Generics;

namespace CourseGuide.Repositories.Entities.Generics
{
    // Implementação genérica de um repositório para entidades.
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDBContext _dbContext;  // Contexto do banco de dados.
        protected readonly DbSet<T> _dbSet;  // Conjunto de entidades do tipo T.

        // Construtor que recebe o contexto do banco de dados.
        public GenericRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();  // Inicializa o conjunto de entidades.
        }

        // Método para obter todas as entidades do tipo T, sem rastreamento.
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        // Método para criar uma nova entidade.
        public virtual async Task<T> Create(T data)
        {
            _dbSet.Add(data);  // Adiciona a entidade ao conjunto.
            await _dbContext.SaveChangesAsync();  // Salva as alterações no banco.

            return data;  // Retorna a entidade criada.
        }

        // Método para atualizar uma entidade existente.
        public virtual async Task<T> Update(T data)
        {
            _dbSet.Update(data);  // Atualiza a entidade no conjunto.
            await _dbContext.SaveChangesAsync();  // Salva as alterações.

            return data;  // Retorna a entidade atualizada.
        }

        // Método para excluir uma entidade.
        public virtual async Task<T> Delete(T data)
        {
            _dbSet.Remove(data);  // Remove a entidade do conjunto.
            await _dbContext.SaveChangesAsync();  // Salva as alterações.

            return data;  // Retorna a entidade excluída.
        }
    }
}
