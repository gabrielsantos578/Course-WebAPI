using AutoMapper;
using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces;
using CourseGuide.Services.Interfaces;
using CourseGuide.Services.Entities.Generics;

namespace CourseGuide.Services.Entities
{
    // Serviço específico para a entidade Table, derivando de IntService.
    public class TableService : IntService<TableDTO, TableModel>, ITableService
    {
        private readonly ITableRepository<TableModel> _tableRepository; // Repositório específico para mesas.

        // Construtor que inicializa o repositório e o mapper.
        public TableService(ITableRepository<TableModel> tableRepository, IMapper mapper) : base(tableRepository, mapper)
        {
            _tableRepository = tableRepository;
        }

        // Método para buscar mesas pelo restaurante.
        public async Task<IEnumerable<TableDTO>> GetByRestaurant(int idRestaurant)
        {
            var tables = await _tableRepository.GetByRestaurant(idRestaurant); // Consulta as mesas vinculados ao restaurante.
            return _mapper.Map<IEnumerable<TableDTO>>(tables); // Mapeia a lista de TableModel para TableDTO.
        }
    }
}
