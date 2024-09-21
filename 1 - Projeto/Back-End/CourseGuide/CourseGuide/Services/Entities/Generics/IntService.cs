using AutoMapper;
using CourseGuide.Objects.Generics;
using CourseGuide.Repositories.Interfaces.Generics;
using CourseGuide.Services.Interfaces.Generics;

namespace CourseGuide.Services.Entities.Generics
{
    // Serviço genérico para entidades identificadas por int, derivando de GenericService.
    public class IntService<TDTO, TEntity> : GenericService<TDTO, TEntity>, IIntService<TDTO, TEntity> where TEntity : class, IEntityInt
    {
        protected readonly IIntRepository<TEntity> _repository; // Repositório específico para entidades com int.

        // Construtor que recebe o repositório e o mapper.
        public IntService(IIntRepository<TEntity> repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository; // Inicializa o repositório.
        }

        // Método para obter uma entidade pelo seu Id do tipo int.
        public virtual async Task<TDTO> GetById(int id)
        {
            var entity = await _repository.GetById(id); // Obtém a entidade do repositório.

            return _mapper.Map<TDTO>(entity); // Mapeia a entidade para um DTO.
        }
    }
}
