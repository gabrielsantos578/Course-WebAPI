using CourseGuide.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CourseGuide.Objects.DTOs.Entities;
using System.Dynamic;
using CourseGuide.Objects.Contracts;
using System.ComponentModel.DataAnnotations;
using CourseGuide.Objects.Utilities;

namespace CourseGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ITableService _tableService;
        private readonly Response _response;

        public TableController(IRestaurantService restaurantService, ITableService tableService)
        {
            _restaurantService = restaurantService;
            _tableService = tableService;

            _response = new Response();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<TableDTO>>> GetAll()
        {
            try
            {
                var tablesDTO = await _tableService.GetAll();
                _response.SetSuccess();
                _response.Message = tablesDTO.Any() ?
                    "Lista da(s) Mesa(s) obtida com sucesso." :
                    "Nenhuma Mesa encontrada.";
                _response.Data = tablesDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "N�o foi poss�vel adquirir a lista da(s) Mesa(s)!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<TableDTO>> GetById(int id)
        {
            try
            {
                var tableDTO = await _tableService.GetById(id);
                if (tableDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Mesa n�o encontrada!";
                    _response.Data = tableDTO;
                    return NotFound(_response);
                };

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.CodeTable + " obtida com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "N�o foi poss�vel adquirir a Mesa informada!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> Create([FromBody] TableDTO tableDTO)
        {
            if (tableDTO is null)
            {
                _response.SetInvalid();
                _response.Message = "Dado(s) inv�lido(s)!";
                _response.Data = tableDTO;
                return BadRequest(_response);
            }
            tableDTO.Id = 0;
            tableDTO.AvailableTable = true;

            try
            {
                dynamic errors = new ExpandoObject();
                var hasErrors = false;

                var restaurantDTO = await _restaurantService.GetById(tableDTO.IdRestaurant);
                if (restaurantDTO is null) { errors.errorIdRestaurant = "O Restaurante informado n�o existe!"; hasErrors = true; }

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return NotFound(_response);
                }

                CheckDatas(tableDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                var tablesRelatedRestaurantDTO = await _tableService.GetByRestaurant(tableDTO.IdRestaurant);
                CheckDuplicates(tablesRelatedRestaurantDTO, tableDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                await _tableService.Create(tableDTO);

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.CodeTable + " cadastrada com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "N�o foi poss�vel cadastrar a Mesa!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] TableDTO tableDTO)
        {
            if (tableDTO is null)
            {
                _response.SetInvalid();
                _response.Message = "Dado(s) inv�lido(s)!";
                _response.Data = tableDTO;
                return BadRequest(_response);
            }

            try
            {
                var existingTableDTO = await _tableService.GetById(tableDTO.Id);
                if (existingTableDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = new { errorId = "A Mesa informada n�o existe!" };
                    return NotFound(_response);
                }
                tableDTO.AvailableTable = existingTableDTO.AvailableTable;

                dynamic errors = new ExpandoObject();
                var hasErrors = false;

                var restaurantDTO = await _restaurantService.GetById(tableDTO.IdRestaurant);
                if (restaurantDTO is null) { errors.errorIdRestaurant = "O Restaurante informado n�o existe!"; hasErrors = true; }

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return NotFound(_response);
                }

                CheckDatas(tableDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                var tablesRelatedRestaurantDTO = await _tableService.GetByRestaurant(tableDTO.IdRestaurant);
                CheckDuplicates(tablesRelatedRestaurantDTO, tableDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                await _tableService.Update(tableDTO);

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.CodeTable + " alterada com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "N�o foi poss�vel alterar a Mesa!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPatch("{id:int}/AvailableTable")]
        public async Task<ActionResult> AvailableTable(int id)
        {
            try
            {
                var tableDTO = await _tableService.GetById(id);
                if (tableDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = new { errorId = "A Mesa informada n�o existe!" };
                    return NotFound(_response);
                }

                if (tableDTO.AvailableTable)
                {
                    _response.SetSuccess();
                    _response.Message = "A Mesa " + tableDTO.CodeTable + " j� est� dispon�vel.";
                    _response.Data = tableDTO;
                    return Ok(_response);
                }

                tableDTO.AvailableTable = true;
                await _tableService.Update(tableDTO);

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.CodeTable + " disponibilizada com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "N�o foi poss�vel disponibilizar a Mesa!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPatch("{id:int}/BlockTable")]
        public async Task<ActionResult> BlockTable(int id)
        {
            try
            {
                var tableDTO = await _tableService.GetById(id);
                if (tableDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = new { errorId = "A Mesa informada n�o existe!" };
                    return NotFound(_response);
                }

                if (!tableDTO.AvailableTable)
                {
                    _response.SetSuccess();
                    _response.Message = "A Mesa " + tableDTO.CodeTable + " j� est� bloqueada.";
                    _response.Data = tableDTO;
                    return Ok(_response);
                }

                tableDTO.AvailableTable = false;
                await _tableService.Update(tableDTO);

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.CodeTable + " bloqueada com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "N�o foi poss�vel bloquear a Mesa!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<TableDTO>> Delete(int id)
        {
            try
            {
                var tableDTO = await _tableService.GetById(id);
                if (tableDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado com conflito!";
                    _response.Data = new { errorId = "Mesa n�o encontrada!" };
                    return NotFound(_response);
                }

                await _tableService.Delete(tableDTO);

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.CodeTable + " exclu�da com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "N�o foi poss�vel excluir a Mesa!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        private static void CheckDatas(TableDTO tableDTO, ref dynamic errors, ref bool hasErrors)
        {
            if (int.TryParse(tableDTO.CapacityTable, out int capacity) && capacity >= 20)
            {
                errors.errorCapacityTable = "N�o � poss�vel uma Mesa possuir capacidade para mais de 20 pessoas!";
                hasErrors = true;
            }

            if ((double)tableDTO.ValueTable >= 100000.0)
            {
                errors.errorValueTable = "A taxa por hora reservada n�o pode ser superior a R$ 100.000,00!";
                hasErrors = true;
            }
        }

        private static void CheckDuplicates(IEnumerable<TableDTO> tablesDTO, TableDTO tableDTO, ref dynamic errors, ref bool hasErrors)
        {
            foreach (var table in tablesDTO)
            {
                if (tableDTO.Id == table.Id)
                {
                    continue;
                }

                if (ValidatorUtilitie.CompareString(tableDTO.CodeTable, table.CodeTable))
                {
                    errors.errorNomeEstado = "J� existe a Mesa " + tableDTO.CodeTable + "!";
                    hasErrors = true;

                    break;
                }
            }
        }
    }
}