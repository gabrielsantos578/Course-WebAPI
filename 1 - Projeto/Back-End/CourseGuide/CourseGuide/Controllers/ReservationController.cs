using CourseGuide.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CourseGuide.Objects.DTOs.Entities;
using System.Dynamic;
using CourseGuide.Objects.Contracts;
using System.ComponentModel.DataAnnotations;
using CourseGuide.Objects.Utilities;
using CourseGuide.Objects.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CourseGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITableService _tableService;
        private readonly IReservationService _reservationService;
        private readonly Response _response;

        public ReservationController(IUserService userService, ITableService tableService, IReservationService reservationService)
        {
            _userService = userService;
            _tableService = tableService;
            _reservationService = reservationService;

            _response = new Response();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ReservationDTO>>> GetAll()
        {
            try
            {
                var reservationsDTO = await _reservationService.GetAll();
                _response.SetSuccess();
                _response.Message = reservationsDTO.Any() ?
                    "Lista da(s) Reserva(s) obtida com sucesso." :
                    "Nenhuma Reserva encontrada.";
                _response.Data = reservationsDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível adquirir a lista da(s) Reserva(s)!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<ReservationDTO>> GetById(int id)
        {
            try
            {
                var reservationDTO = await _reservationService.GetById(id);
                if (reservationDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Reserva não encontrada!";
                    _response.Data = reservationDTO;
                    return NotFound(_response);
                };

                _response.SetSuccess();
                _response.Message = "Reserva para " + reservationDTO.DateReservation.Substring(0, 10) + " obtida com sucesso.";
                _response.Data = reservationDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível adquirir a Reserva informada!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] ReservationDTO reservationDTO)
        {
            if (reservationDTO is null)
            {
                _response.SetInvalid();
                _response.Message = "Dado(s) inválido(s)!";
                _response.Data = reservationDTO;
                return BadRequest(_response);
            }
            reservationDTO.Id = 0;
            reservationDTO.DefaultState();

            try
            {
                dynamic errors = new ExpandoObject();
                var hasErrors = false;

                var userDTO = await _userService.GetById(reservationDTO.IdUser);
                if (userDTO is null) { errors.errorIdUser = "O Usuário informado não existe!"; hasErrors = true; }

                var tableDTO = await _tableService.GetById(reservationDTO.IdTable);
                if (tableDTO is null) { errors.errorIdTable = "A Mesa informada não existe!"; hasErrors = true; }

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return NotFound(_response);
                }

                var reservationsRelatedTableDTO = await _reservationService.GetReservationsRelatedTable(reservationDTO.IdTable);
                CheckDatas(tableDTO, reservationsRelatedTableDTO, reservationDTO, ref errors, ref hasErrors);

                // Verifica se já existe um erro de data de reserva
                if (!(errors is IDictionary<string, object> errorDict) || !errorDict.ContainsKey("errorDateReservation"))
                {
                    if (DateTime.TryParseExact(reservationDTO.DateReservation, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime dateReservation))
                    {
                        if (dateReservation.Date < DateTime.Now.Date)
                        {
                            errors.errorDateReservation = $"A data de reserva {reservationDTO.DateReservation} é inválida! Informe uma data para o dia atual ou após.";
                            hasErrors = true;
                        }
                    }
                }

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                var reservationsRelatedUserDTO = await _reservationService.GetReservationsRelatedUser(reservationDTO.IdUser);
                CheckDuplicates(reservationsRelatedUserDTO, reservationDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                reservationDTO.DateFinish = "";
                reservationDTO.TimeDuration = "";
                reservationDTO.ValueReservation = 0;
                reservationDTO.CreateAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                reservationDTO.UpdateAt = "";

                await _reservationService.Create(reservationDTO);

                _response.SetSuccess();
                _response.Message = "Reserva para " + reservationDTO.DateReservation.Substring(0, 10) + " cadastrada com sucesso.";
                _response.Data = reservationDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível cadastrar a Reserva!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] ReservationDTO reservationDTO)
        {
            if (reservationDTO is null)
            {
                _response.SetInvalid();
                _response.Message = "Dado(s) inválido(s)!";
                _response.Data = reservationDTO;
                return BadRequest(_response);
            }

            try
            {
                var existingReservationDTO = await _reservationService.GetById(reservationDTO.Id);
                if (existingReservationDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = new { errorId = "A Reserva informada não existe!" };
                    return NotFound(_response);
                }
                else if (!existingReservationDTO.DateFinish.IsEmpty())
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = new { errorId = "A Reserva foi finalizada, não é possível alterar!" };
                    return NotFound(_response);
                }

                dynamic errors = new ExpandoObject();
                var hasErrors = false;

                var userDTO = await _userService.GetById(reservationDTO.IdUser);
                if (userDTO is null) { errors.errorIdUser = "O Usuário informado não existe!"; hasErrors = true; }

                var tableDTO = await _tableService.GetById(reservationDTO.IdTable);
                if (tableDTO is null) { errors.errorIdTable = "A Mesa informada não existe!"; hasErrors = true; }

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return NotFound(_response);
                }

                var reservationsRelatedTableDTO = await _reservationService.GetReservationsRelatedTable(reservationDTO.IdTable);
                CheckDatas(tableDTO, reservationsRelatedTableDTO, reservationDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                var reservationsRelatedUserDTO = await _reservationService.GetReservationsRelatedUser(reservationDTO.IdUser);
                CheckDuplicates(reservationsRelatedUserDTO, reservationDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                reservationDTO.DateFinish = existingReservationDTO.DateFinish;
                reservationDTO.TimeDuration = existingReservationDTO.TimeDuration;
                reservationDTO.ValueReservation = existingReservationDTO.ValueReservation;
                reservationDTO.CreateAt = existingReservationDTO.CreateAt;
                reservationDTO.UpdateAt = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                reservationDTO.Status = existingReservationDTO.Status;
                await _reservationService.Update(reservationDTO);

                _response.SetSuccess();
                _response.Message = "Reserva para " + reservationDTO.DateReservation.Substring(0, 10) + " alterada com sucesso.";
                _response.Data = reservationDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível alterar a Reserva!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id:int}/InProgress")]
        public async Task<ActionResult<ReservationDTO>> InProgress(int id)
        {
            try
            {
                var reservationDTO = await _reservationService.GetById(id);
                if (reservationDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Reserva não encontrada!";
                    _response.Data = new { errorId = "Reserva não encontrada!" };
                    return NotFound(_response);
                }
                else if (!reservationDTO.CanInProgress())
                {
                    if (reservationDTO.Status == StatusReservation.InProgress)
                    {
                        _response.SetSuccess();
                        _response.Message = $"A Reserva já está em {reservationDTO.GetState()}.";
                    }
                    else
                    {
                        _response.SetConflict();
                        _response.Message = $"A Reserva não pode ser colocada Em Andamento pois está {reservationDTO.GetState()}.";
                    }

                    _response.Data = reservationDTO;
                    return Ok(_response);
                }


                reservationDTO.PutInProgress();
                await _reservationService.Update(reservationDTO);

                _response.SetSuccess();
                _response.Message = "A Reserva está " + reservationDTO.GetState() + ".";
                _response.Data = reservationDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível colocar Em Andamento a Reserva!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id:int}/Finish")]
        public async Task<ActionResult<ReservationDTO>> Finish(int id)
        {
            try
            {
                var reservationDTO = await _reservationService.GetById(id);
                if (reservationDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Reserva não encontrada!";
                    _response.Data = new { errorId = "Reserva não encontrada!" };
                    return NotFound(_response);
                }
                else if (!reservationDTO.CanFinish())
                {
                    if (reservationDTO.Status == StatusReservation.Finished)
                    {
                        _response.SetSuccess();
                        _response.Message = $"A Reserva já foi {reservationDTO.GetState()}.";
                    }
                    else
                    {
                        _response.SetConflict();
                        _response.Message = $"A Reserva não pode ser Finalizada pois está {reservationDTO.GetState()}.";
                    }

                    _response.Data = reservationDTO;
                    return Ok(_response);
                }

                var tableDTO = await _tableService.GetById(reservationDTO.IdTable);

                CalculatePrice(ref reservationDTO, tableDTO);

                reservationDTO.Finish();
                await _reservationService.Update(reservationDTO);

                _response.SetSuccess();
                _response.Message = "A Reserva foi " + reservationDTO.GetState() + ".";
                _response.Data = reservationDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível Finalizar a Reserva!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id:int}/Block")]
        public async Task<ActionResult<ReservationDTO>> Block(int id)
        {
            try
            {
                var reservationDTO = await _reservationService.GetById(id);
                if (reservationDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Reserva não encontrada!";
                    _response.Data = new { errorId = "Reserva não encontrada!" };
                    return NotFound(_response);
                }
                else if (!reservationDTO.CanBlock())
                {
                    if (reservationDTO.Status == StatusReservation.Blocked)
                    {
                        _response.SetSuccess();
                        _response.Message = $"A Reserva já está {reservationDTO.GetState()}.";
                    }
                    else
                    {
                        _response.SetConflict();
                        _response.Message = $"A Reserva não pode ser Bloqueada pois está {reservationDTO.GetState()}.";
                    }

                    _response.Data = reservationDTO;
                    return Ok(_response);
                }

                var tableDTO = await _tableService.GetById(reservationDTO.IdTable);

                // Converte a data atual e a data de reserva para DateTime para realizar a comparação
                var currentDate = DateTime.Now;
                DateTime dateReservation = DateTime.ParseExact(reservationDTO.DateReservation, "dd/MM/yyyy HH:mm", null);

                if (dateReservation >= currentDate)
                {
                    // Atualizar a data e hora de finalização
                    reservationDTO.DateFinish = "--/--/---- --:--";

                    // Calcular o valor da reserva
                    reservationDTO.ValueReservation = 0;

                    // Incluir o número de dias na duração
                    reservationDTO.TimeDuration = $"- dia(s) --:-- hora(s)";
                }
                else
                {
                    CalculatePrice(ref reservationDTO, tableDTO);
                }

                reservationDTO.Block();
                await _reservationService.Update(reservationDTO);

                _response.SetSuccess();
                _response.Message = "A Reserva foi " + reservationDTO.GetState() + ".";
                _response.Data = reservationDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível Bloquear a Reserva!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }


        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<ReservationDTO>> Delete(int id)
        {
            try
            {
                var reservationDTO = await _reservationService.GetById(id);
                if (reservationDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado com conflito!";
                    _response.Data = new { errorId = "Reserva não encontrada!" };
                    return NotFound(_response);
                }

                await _reservationService.Delete(reservationDTO);

                _response.SetSuccess();
                _response.Message = "Reserva para " + reservationDTO.DateReservation.Substring(0, 10) + " excluída com sucesso.";
                _response.Data = reservationDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível excluir a Reserva!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        private static void CheckDatas(TableDTO table, IEnumerable<ReservationDTO> reservationsDTO, ReservationDTO reservationDTO, ref dynamic errors, ref bool hasErrors)
        {
            if (!table.AvailableTable)
            {
                errors.errorIdTable = "A Mesa " + table.CodeTable + " está indisponível para reserva!";
                hasErrors = true;

                return;
            }

            if (!DateTime.TryParseExact(reservationDTO.DateReservation, "dd/MM/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out _))
            {
                errors.errorDateReservation = "A data de reserva é inválida! Ela deve estar no formato dd/MM/yyyy HH:mm!";
                hasErrors = true;
            }

            if (!((IDictionary<string, object>)errors).ContainsKey("errorDateReservation"))
            {
                var conflictingDateReservation = reservationsDTO.FirstOrDefault(reservation =>
                    reservation.IdUser != reservationDTO.IdUser &&
                    reservation.DateFinish.IsEmpty() &&
                    ValidatorUtilitie.CompareString(reservationDTO.DateReservation.Substring(0, 10), reservation.DateReservation.Substring(0, 10)));

                if (conflictingDateReservation is not null)
                {
                    errors.errorDateReservation = "A Mesa " + table.CodeTable + " está reservada para " + conflictingDateReservation.DateReservation.Substring(0, 10) + " por outra pessoa!";
                    hasErrors = true;
                }
            }
        }

        private static void CheckDuplicates(IEnumerable<ReservationDTO> reservationsDTO, ReservationDTO reservationDTO, ref dynamic errors, ref bool hasErrors)
        {
            foreach (var reservation in reservationsDTO)
            {
                if (reservationDTO.Id == reservation.Id)
                {
                    continue;
                }

                if (reservationDTO.IdTable == reservation.IdTable && reservation.DateFinish.IsEmpty() && ValidatorUtilitie.CompareString(reservationDTO.DateReservation.Substring(0, 10), reservation.DateReservation.Substring(0, 10)))
                {
                    errors.errorDateReservation = "Você já reservou a Mesa para " + reservationDTO.DateReservation.Substring(0, 10) + " às " + reservationDTO.DateReservation.Substring(11, 5) + "h!";
                    hasErrors = true;

                    break;
                }
            }
        }

        private static void CalculatePrice(ref ReservationDTO reservationDTO, TableDTO tableDTO)
        {
            // Atualizar a data e hora de finalização
            reservationDTO.DateFinish = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            // Converter as strings de data e hora para DateTime
            DateTime dateBegin = DateTime.ParseExact(reservationDTO.DateReservation, "dd/MM/yyyy HH:mm", null);
            DateTime dateFinish = DateTime.ParseExact(reservationDTO.DateFinish, "dd/MM/yyyy HH:mm", null);

            // Calcular a duração da reserva em minutos
            TimeSpan duration = dateFinish - dateBegin;
            decimal durationInMinutes = (decimal)duration.TotalMinutes;

            // Calcular o valor por minuto
            decimal valuePerMinute = tableDTO.ValueTable / 60;

            // Calcular o valor da reserva
            reservationDTO.ValueReservation = Math.Round(durationInMinutes * valuePerMinute, 2);

            // Incluir o número de dias na duração
            reservationDTO.TimeDuration = $"{(int)duration.TotalDays} dia(s) {duration.Hours:D2}:{duration.Minutes:D2} hora(s)";
        }
    }
}