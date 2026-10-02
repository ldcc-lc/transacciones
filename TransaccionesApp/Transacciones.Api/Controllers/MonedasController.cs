using Microsoft.AspNetCore.Mvc;
using Transacciones.Application.Interfaces;
using Transacciones.Application.DTOs;

namespace Transacciones.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonedasController : ControllerBase
    {
        private readonly IMonedaRepository _monedaRepository;

        public MonedasController(IMonedaRepository monedaRepository)
        {
            _monedaRepository = monedaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var monedas = await _monedaRepository.GetAllAsync();

            var response = monedas.Select(moneda => new MonedaDto
            {
                Id = moneda.Id,
                Codigo = moneda.Codigo,
                Nombre = moneda.Nombre
            });

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var moneda = await _monedaRepository.GetByIdAsync(id);

            if (moneda is null)
            {
                return NotFound();
            }

            var response = new MonedaDto
            {
                Id = moneda.Id,
                Codigo = moneda.Codigo,
                Nombre = moneda.Nombre
            };

            return Ok(response);
        }
    }
}
