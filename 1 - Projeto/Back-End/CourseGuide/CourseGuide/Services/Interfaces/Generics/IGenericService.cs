namespace CourseGuide.Services.Interfaces.Generics
{
    // Interface genérica para serviços, onde TDTO é o objeto de transferência de dados e TEntity é a entidade do modelo.
    public interface IGenericService<TDTO, TEntity> where TEntity : class
    {
        Task<IEnumerable<TDTO>> GetAll();  // Método para obter todos os registros, retorna uma lista de DTOs.
        Task<TDTO> Create(TDTO data);      // Método para criar uma nova entidade, recebendo um DTO e retornando o DTO criado.
        Task<TDTO> Update(TDTO data);      // Método para atualizar uma entidade existente, recebendo um DTO e retornando o DTO atualizado.
        Task<TDTO> Delete(TDTO data);      // Método para deletar uma entidade, recebendo um DTO e retornando o DTO deletado.
    }
}
