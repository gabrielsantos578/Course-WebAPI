namespace CourseGuide.Repositories.Interfaces.Generics
{
    // Interface genérica para repositórios, definindo operações básicas de CRUD.
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();  // Método para obter todos os registros do tipo T.
        Task<T> Create(T data);         // Método para criar um novo registro do tipo T.
        Task<T> Update(T data);         // Método para atualizar um registro existente do tipo T.
        Task<T> Delete(T data);         // Método para excluir um registro do tipo T.
    }
}
