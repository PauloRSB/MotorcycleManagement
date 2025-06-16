using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentChallenge.Application.DTOs.Requests.DeliveryMan;
using RentChallenge.Application.Interfaces.APIs;

namespace RentChallenge.API.Controllers
{
    [Route("api/entregadores")]
    [ApiController]
    public class DeliveryManController(IDeliveryManApiService service) : ControllerBase
    {
        private readonly IDeliveryManApiService _service = service;
       
        [HttpPost]
        public async Task<IActionResult> RegisterDeliveryMan([FromBody] RegisterDeliveryManDTO deliveryMan)
        {
            await _service.RegisterAsync(deliveryMan);
            return Created(string.Empty, null);
        }

        [HttpPost("{identificador}/cnh")]
        public async Task<IActionResult> UploadDeliveryManCnh(string identificador, IFormFile file)
        {
            await _service.UploadCnhImage(file.OpenReadStream(), identificador, file.ContentType);
            return Created(string.Empty, null);
        }
    }
}
