using AutoMapper;
using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Models.Entities;
using CourseGuide.Repositories.Interfaces;
using CourseGuide.Services.Interfaces;
using CourseGuide.Services.Entities.Generics.Methods;

namespace CourseGuide.Services.Entities
{
    // Serviço específico para a entidade Reservation, derivando de GetByIdIntService.
    public class ReservationService : GetByIdIntService<ReservationDTO, ReservationModel>, IReservationService
    {
        private readonly IReservationRepository<ReservationModel> _reservationRepository; // Repositório específico para mesas.

        // Construtor que inicializa o repositório e o mapper.
        public ReservationService(IReservationRepository<ReservationModel> reservationRepository, IMapper mapper) : base(reservationRepository, mapper)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationsRelatedUser(int idUser)
        {
            var reservationsModel = await _reservationRepository.GetReservationsRelatedUser(idUser);
            return _mapper.Map<IEnumerable<ReservationDTO>>(reservationsModel);
        }

        public async Task<IEnumerable<ReservationDTO>> GetReservationsRelatedTable(int idTable)
        {
            var reservationsModel = await _reservationRepository.GetReservationsRelatedTable(idTable);
            return _mapper.Map<IEnumerable<ReservationDTO>>(reservationsModel);
        }
    }
}
