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
                _response.Message = "Não foi possível adquirir a lista da(s) Mesa(s)!";
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
                    _response.Message = "Mesa não encontrada!";
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
                _response.Message = "Não foi possível adquirir a Mesa informada!";
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
                _response.Message = "Dado(s) inválido(s)!";
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
                if (restaurantDTO is null) { errors.errorIdRestaurant = "O Restaurante informado não existe!"; hasErrors = true; }

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
                _response.Message = "Não foi possível cadastrar a Mesa!";
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
                _response.Message = "Dado(s) inválido(s)!";
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
                    _response.Data = new { errorId = "A Mesa informada não existe!" };
                    return NotFound(_response);
                }
                tableDTO.AvailableTable = existingTableDTO.AvailableTable;

                dynamic errors = new ExpandoObject();
                var hasErrors = false;

                var restaurantDTO = await _restaurantService.GetById(tableDTO.IdRestaurant);
                if (restaurantDTO is null) { errors.errorIdRestaurant = "O Restaurante informado não existe!"; hasErrors = true; }

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
                _response.Message = "Não foi possível alterar a Mesa!";
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
                    _response.Data = new { errorId = "A Mesa informada não existe!" };
                    return NotFound(_response);
                }

                if (tableDTO.AvailableTable)
                {
                    _response.SetSuccess();
                    _response.Message = "A Mesa " + tableDTO.CodeTable + " já está disponível.";
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
                _response.Message = "Não foi possível disponibilizar a Mesa!";
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
                    _response.Data = new { errorId = "A Mesa informada não existe!" };
                    return NotFound(_response);
                }

                if (!tableDTO.AvailableTable)
                {
                    _response.SetSuccess();
                    _response.Message = "A Mesa " + tableDTO.CodeTable + " já está bloqueada.";
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
                _response.Message = "Não foi possível bloquear a Mesa!";
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
                    _response.Data = new { errorId = "Mesa não encontrada!" };
                    return NotFound(_response);
                }

                await _tableService.Delete(tableDTO);

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.CodeTable + " excluída com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível excluir a Mesa!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        private static void CheckDatas(TableDTO tableDTO, ref dynamic errors, ref bool hasErrors)
        {
            if (int.TryParse(tableDTO.CapacityTable, out int capacity) && capacity >= 20)
            {
                errors.errorCapacityTable = "Não é possível uma Mesa possuir capacidade para mais de 20 pessoas!";
                hasErrors = true;
            }

            if ((double)tableDTO.ValueTable >= 100000.0)
            {
                errors.errorValueTable = "A taxa por hora reservada não pode ser superior a R$ 100.000,00!";
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
                    errors.errorNomeEstado = "Já existe a Mesa " + tableDTO.CodeTable + "!";
                    hasErrors = true;

                    break;
                }
            }
        }
    }
}