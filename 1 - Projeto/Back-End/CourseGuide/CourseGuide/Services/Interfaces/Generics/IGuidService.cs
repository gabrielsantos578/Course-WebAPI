using CourseGuide.Objects.Generics;

namespace CourseGuide.Services.Interfaces.Generics
{
    // Interface para serviços que trabalham com entidades identificadas por Guid, estendendo IGenericService.
    public interface IGuidService<TDTO, TEntity> : IGenericService<TDTO, TEntity> where TEntity : class, IEntityGuid
    {
        Task<TDTO> GetById(Guid id);  // Método para obter uma entidade pelo seu ID do tipo Guid, retornando um DTO.
    }
}
