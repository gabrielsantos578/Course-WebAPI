using AutoMapper;
using CourseGuide.Objects.Generics;
using CourseGuide.Repositories.Interfaces.Generics.Methods;
using CourseGuide.Services.Interfaces.Generics.Methods;

namespace CourseGuide.Services.Entities.Generics.Methods
{
    // Serviço genérico para entidades identificadas por Guid, derivando de GenericService.
    public class GetByIdGuidService<TDTO, TEntity> : GenericService<TDTO, TEntity>, IGetByIdGuidService<TDTO, TEntity> where TEntity : class, IEntityGuid
    {
        protected readonly IGetByIdGuidRepository<TEntity> _repository; // Repositório específico para entidades com Guid.

        // Construtor que recebe o repositório e o mapper.
        public GetByIdGuidService(IGetByIdGuidRepository<TEntity> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository; // Inicializa o repositório.
        }

        // Método para obter uma entidade pelo seu Id do tipo Guid.
        public virtual async Task<TDTO> GetById(Guid id)
        {
            var entity = await _repository.GetById(id); // Obtém a entidade do repositório.

            return _mapper.Map<TDTO>(entity); // Mapeia a entidade para um DTO.
        }
    }
}
