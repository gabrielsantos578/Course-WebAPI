using CourseGuide.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CourseGuide.Objects.DTOs.Entities;
using CourseGuide.Objects.Contracts;
using CourseGuide.Objects.Utilities;
using System.Dynamic;

namespace CourseGuide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : Controller
    {
        private readonly ITableService _tableService;
        private readonly IRestaurantService _restaurantService;
        private readonly Response _response;

        public TableController(ITableService tableService, IRestaurantService restaurantService)
        {
            _tableService = tableService;
            _restaurantService = restaurantService;

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
                    "Lista do(s) Mesa(s) obtida com sucesso." :
                    "Nenhum Mesa encontrado.";
                _response.Data = tablesDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível adquirir a lista do(s) Mesa(s)!";
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
                if (tableDTO == null)
                {
                    _response.SetNotFound();
                    _response.Message = "Mesa não encontrado!";
                    _response.Data = tableDTO;
                    return NotFound(_response);
                };

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.NumberTable + " obtido com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível adquirir o Mesa informado!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<TableDTO>> Create([FromBody] TableDTO tableDTO)
        {
            if (tableDTO == null)
            {
                _response.SetInvalid();
                _response.Message = "Dado(s) inválido(s)!";
                _response.Data = tableDTO;
                return BadRequest(_response);
            }
            tableDTO.Id = 0;

            try
            {
                var restaurantDTO = await _restaurantService.GetById(tableDTO.IdRestaurant);
                if (restaurantDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "O Restaurante informado não existe!";
                    _response.Data = new { errorIdRestaurante = "O Restaurante informado não existe!" };
                    return NotFound(_response);
                }

                dynamic errors = new ExpandoObject();
                var hasErrors = false;

                CheckDatas(tableDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                var tablesDTO = await _tableService.GetByRestaurant(tableDTO.IdRestaurant);
                CheckDuplicates(tablesDTO, tableDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                await _tableService.Create(tableDTO);

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.NumberTable + " cadastrado com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível cadastrar o Mesa!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }


        [HttpPut("Update")]
        public async Task<ActionResult<TableDTO>> Update([FromBody] TableDTO tableDTO)
        {
            if (tableDTO == null)
            {
                _response.SetInvalid();
                _response.Message = "Dado(s) inválido(s)!";
                _response.Data = tableDTO;
                return BadRequest(_response);
            }

            try
            {
                var existingTableDTO = await _tableService.GetById(tableDTO.Id);
                if (existingTableDTO == null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = new { errorId = "O Mesa informado não existe!" };
                    return NotFound(_response);
                }

                var restaurantDTO = await _restaurantService.GetById(tableDTO.IdRestaurant);
                if (restaurantDTO is null)
                {
                    _response.SetNotFound();
                    _response.Message = "O Restaurante informado não existe!";
                    _response.Data = new { errorIdRestaurante = "O Restaurante informado não existe!" };
                    return NotFound(_response);
                }

                dynamic errors = new ExpandoObject();
                var hasErrors = false;

                CheckDatas(tableDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                var tablesDTO = await _tableService.GetByRestaurant(tableDTO.IdRestaurant);
                CheckDuplicates(tablesDTO, tableDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                await _tableService.Update(tableDTO);

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.NumberTable + " alterado com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível alterar o Mesa!";
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
                if (tableDTO == null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado com conflito!";
                    _response.Data = new { errorId = "Mesa não encontrado!" };
                    return NotFound(_response);
                }

                await _tableService.Delete(tableDTO);

                _response.SetSuccess();
                _response.Message = "Mesa " + tableDTO.NumberTable + " excluído com sucesso.";
                _response.Data = tableDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível excluir o Mesa!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        private static void CheckDatas(TableDTO tableDTO, ref dynamic errors, ref bool hasErrors)
        {
            // Convertendo CapacityTable de string para int para realizar a comparação
            if (int.TryParse(tableDTO.CapacityTable, out int capacity) && capacity > 12)
            {
                errors.errorCapacityTable = $"Uma mesa com capacidade de {capacity} é muito grande!";
                hasErrors = true;
            }

            if (tableDTO.ValueTable <= 0.0m)
            {
                errors.errorValueTable = "O valor não pode ser R$ 0,00 ou inferior!";
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

                if (ValidatorUtilitie.CompareString(tableDTO.NumberTable, table.NumberTable))
                {
                    errors.errorNumberTable = "A mesa " + tableDTO.NumberTable + " já existe!";
                    hasErrors = true;

                    break;
                }
            }
        }
    }
}
