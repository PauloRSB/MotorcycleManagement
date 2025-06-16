using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentChallenge.Application.DTOs.Requests.Motorcycle;
using RentChallenge.Application.Interfaces.APIs;
using RentChallenge.Application.Services.APIs;
using RentChallenge.Domain.Entities;
using RentChallenge.Domain.Exceptions;

namespace RentChallenge.API.Controllers
{
    [Route("api/motos")]
    [ApiController]
    public class MotorcycleController(IMotorcycleApiService service) : ControllerBase
    {
        private readonly IMotorcycleApiService _service = service;

        [HttpPost]
        public async Task<ActionResult> RegisterAsync(RegisterMotorcycleRequestDTO dto)
        {
            await _service.RegisterAsync(dto);
            return Created($"api/motorcycles/{dto.Identifier}", dto);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync(string? placa)
        {
            var motorcycles = await _service.GetAllAsync(placa);
            return Ok();
        }

        [HttpGet("{identificador}")]
        public async Task<ActionResult<Motorcycle>> GetByIdentifierAsync(string identificador)
        {
            var motorcycle = await _service.GetByIdentifierAsync(identificador);
            return Ok(motorcycle);
        }

        [HttpPut("{identificador}/placa")]
        public async Task<ActionResult> UpdateNumberPlateAsync(string identificador, [FromBody] UpdateMotorcycleNumberPlateDTO dto)
        {
            await _service.UpdateNumberPlateAsync(identificador, dto.NumberPlate);
            return Ok();

        }


        [HttpDelete("{identificador}")]
        public async Task<ActionResult> DeleteAsync(string identificador)
        {
            await _service.DeleteAsync(identificador);
            return Ok();
        }
    }
}
