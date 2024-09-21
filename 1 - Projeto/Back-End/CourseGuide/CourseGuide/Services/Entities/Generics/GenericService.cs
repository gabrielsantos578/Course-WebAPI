using AutoMapper;
using CourseGuide.Repositories.Interfaces.Generics;
using CourseGuide.Services.Interfaces.Generics;

namespace CourseGuide.Services.Entities.Generics
{
    // Implementação genérica de serviços para manipulação de entidades e DTOs.
    public class GenericService<TDTO, TEntity> : IGenericService<TDTO, TEntity> where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository; // Repositório para operações de CRUD.
        protected readonly IMapper _mapper; // Mapper para conversão entre DTOs e entidades.

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository; // Inicializa o repositório.
            _mapper = mapper; // Inicializa o mapper.
        }

        // Obtém todas as entidades e as converte em DTOs.
        public virtual async Task<IEnumerable<TDTO>> GetAll()
        {
            var entities = await _repository.GetAll(); // Obtém todas as entidades do repositório.

            return _mapper.Map<IEnumerable<TDTO>>(entities); // Mapeia as entidades para DTOs.
        }

        // Cria uma nova entidade a partir de um DTO.
        public virtual async Task<TDTO> Create(TDTO data)
        {
            var entity = _mapper.Map<TEntity>(data); // Converte o DTO em uma entidade.
            var createdEntity = await _repository.Create(entity); // Cria a entidade no repositório.

            return _mapper.Map<TDTO>(createdEntity); // Retorna o DTO correspondente à entidade criada.
        }

        // Atualiza uma entidade existente com dados de um DTO.
        public virtual async Task<TDTO> Update(TDTO data)
        {
            var entity = _mapper.Map<TEntity>(data); // Converte o DTO em uma entidade.
            var updatedEntity = await _repository.Update(entity); // Atualiza a entidade no repositório.

            return _mapper.Map<TDTO>(updatedEntity); // Retorna o DTO correspondente à entidade atualizada.
        }

        // Exclui uma entidade com base em um DTO.
        public virtual async Task<TDTO> Delete(TDTO data)
        {
            var entity = _mapper.Map<TEntity>(data); // Converte o DTO em uma entidade.
            var deletedEntity = await _repository.Delete(entity); // Remove a entidade do repositório.

            return _mapper.Map<TDTO>(deletedEntity); // Retorna o DTO correspondente à entidade excluída.
        }
    }
}
