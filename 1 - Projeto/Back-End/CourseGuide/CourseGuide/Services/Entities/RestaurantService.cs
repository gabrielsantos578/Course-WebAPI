using AutoMapper;
using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces;
using CourseGuide.Services.Interfaces;
using CourseGuide.Services.Entities.Generics;

namespace CourseGuide.Services.Entities
{
    // Serviço específico para a entidade Restaurant, derivando de IntService.
    public class RestaurantService : IntService<RestaurantDTO, RestaurantModel>, IRestaurantService
    {
        private readonly IRestaurantRepository<RestaurantModel> _restaurantRepository; // Repositório específico para restaurantes.

        // Construtor que inicializa o repositório e o mapper.
        public RestaurantService(IRestaurantRepository<RestaurantModel> restaurantRepository, IMapper mapper) : base(restaurantRepository, mapper)
        {
            _restaurantRepository = restaurantRepository;
        }

        // Método para buscar restaurantes pelo dono.
        public async Task<IEnumerable<RestaurantDTO>> GetByOwner(int idOwner)
        {
            var restaurants = await _restaurantRepository.GetByOwner(idOwner); // Consulta os restaurantes vinculados ao dono.
            return _mapper.Map<IEnumerable<RestaurantDTO>>(restaurants); // Mapeia a lista de RestaurantModel para RestaurantDTO.
        }
    }
}
