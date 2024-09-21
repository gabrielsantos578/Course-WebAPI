using CourseGuide.Objects.Generics;

namespace CourseGuide.Services.Interfaces.Generics.Methods
{
    // Interface para serviços que trabalham com entidades identificadas por int, estendendo IGenericService.
    public interface IGetByIdIntService<TDTO, TEntity> : IGenericService<TDTO, TEntity> where TEntity : class, IEntityInt
    {
        Task<TDTO> GetById(int id);  // Método para obter uma entidade pelo seu ID do tipo int, retornando um DTO.
    }
}
