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
    public class RestaurantController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly Response _response;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;

            _response = new Response();
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<RestaurantDTO>>> GetAll()
        {
            try
            {
                var restaurantsDTO = await _restaurantService.GetAll();
                _response.SetSuccess();
                _response.Message = restaurantsDTO.Any() ?
                    "Lista do(s) Restaurante(s) obtida com sucesso." :
                    "Nenhum Restaurante encontrado.";
                _response.Data = restaurantsDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível adquirir a lista do(s) Restaurante(s)!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<ActionResult<RestaurantDTO>> GetById(int id)
        {
            try
            {
                var restaurantDTO = await _restaurantService.GetById(id);
                if (restaurantDTO == null)
                {
                    _response.SetNotFound();
                    _response.Message = "Restaurante não encontrado!";
                    _response.Data = restaurantDTO;
                    return NotFound(_response);
                };

                _response.SetSuccess();
                _response.Message = "Restaurante " + restaurantDTO.NameRestaurant + " obtido com sucesso.";
                _response.Data = restaurantDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível adquirir o Restaurante informado!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<RestaurantDTO>> Create([FromBody] RestaurantDTO restaurantDTO)
        {
            if (restaurantDTO == null)
            {
                _response.SetInvalid();
                _response.Message = "Dado(s) inválido(s)!";
                _response.Data = restaurantDTO;
                return BadRequest(_response);
            }
            restaurantDTO.Id = 0;

            try
            {
                dynamic errors = new ExpandoObject();
                var hasErrors = false;

                CheckDatas(restaurantDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                var restaurantsDTO = await _restaurantService.GetAll();
                CheckDuplicates(restaurantsDTO, restaurantDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                await _restaurantService.Create(restaurantDTO);

                _response.SetSuccess();
                _response.Message = "Restaurante " + restaurantDTO.NameRestaurant + " cadastrado com sucesso.";
                _response.Data = restaurantDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível cadastrar o Restaurante!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }


        [HttpPut("Update")]
        public async Task<ActionResult<RestaurantDTO>> Update([FromBody] RestaurantDTO restaurantDTO)
        {
            if (restaurantDTO == null)
            {
                _response.SetInvalid();
                _response.Message = "Dado(s) inválido(s)!";
                _response.Data = restaurantDTO;
                return BadRequest(_response);
            }

            try
            {
                var existingRestaurantDTO = await _restaurantService.GetById(restaurantDTO.Id);
                if (existingRestaurantDTO == null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = new { errorId = "O Restaurante informado não existe!" };
                    return NotFound(_response);
                }

                dynamic errors = new ExpandoObject();
                var hasErrors = false;

                CheckDatas(restaurantDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                var restaurantsDTO = await _restaurantService.GetAll();
                CheckDuplicates(restaurantsDTO, restaurantDTO, ref errors, ref hasErrors);

                if (hasErrors)
                {
                    _response.SetConflict();
                    _response.Message = "Dado(s) com conflito!";
                    _response.Data = errors;
                    return BadRequest(_response);
                }

                await _restaurantService.Update(restaurantDTO);

                _response.SetSuccess();
                _response.Message = "Restaurante " + restaurantDTO.NameRestaurant + " alterado com sucesso.";
                _response.Data = restaurantDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível alterar o Restaurante!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<ActionResult<RestaurantDTO>> Delete(int id)
        {
            try
            {
                var restaurantDTO = await _restaurantService.GetById(id);
                if (restaurantDTO == null)
                {
                    _response.SetNotFound();
                    _response.Message = "Dado com conflito!";
                    _response.Data = new { errorId = "Restaurante não encontrado!" };
                    return NotFound(_response);
                }

                await _restaurantService.Delete(restaurantDTO);

                _response.SetSuccess();
                _response.Message = "Restaurante " + restaurantDTO.NameRestaurant + " excluído com sucesso.";
                _response.Data = restaurantDTO;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.SetError();
                _response.Message = "Não foi possível excluir o Restaurante!";
                _response.Data = new { ErrorMessage = ex.Message, StackTrace = ex.StackTrace ?? "No stack trace available!" };
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        private static void CheckDatas(RestaurantDTO restaurantDTO, ref dynamic errors, ref bool hasErrors)
        {
            if (!ValidatorUtilitie.CheckValidPhone(restaurantDTO.PhoneRestaurant))
            {
                errors.errorPhoneRestaurant = "Número inválido!";
                hasErrors = true;
            }

            int status = ValidatorUtilitie.CheckValidEmail(restaurantDTO.EmailRestaurant);
            if (status == -1)
            {
                errors.errorEmailRestaurant = "E-mail inválido!";
                hasErrors = true;
            }
            else if (status == -2)
            {
                errors.errorEmailRestaurant = "Domínio inválido!";
                hasErrors = true;
            }
        }

        private static void CheckDuplicates(IEnumerable<RestaurantDTO> restaurantsDTO, RestaurantDTO restaurantDTO, ref dynamic errors, ref bool hasErrors)
        {
            foreach (var restaurant in restaurantsDTO)
            {
                if (restaurantDTO.Id == restaurant.Id)
                {
                    continue;
                }

                if (ValidatorUtilitie.CompareString(restaurantDTO.EmailRestaurant, restaurant.EmailRestaurant))
                {
                    errors.errorEmailRestaurant = "O e-mail " + restaurantDTO.EmailRestaurant + " já está sendo utilizado!";
                    hasErrors = true;

                    break;
                }
            }
        }
    }
}
