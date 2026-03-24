using DtoModel.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.PersonaTipoSexoB;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonaTipoSexoController : ControllerBase
    {
        private readonly IPersonaTipoSexoBussnies _bussnies;

        public PersonaTipoSexoController(IPersonaTipoSexoBussnies bussnies)
        {
            _bussnies = bussnies;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonaTipoSexoDto>>> GetAll()
        {
            return Ok(await _bussnies.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaTipoSexoDto>> GetById(int id)
        {
            PersonaTipoSexoDto? item = await _bussnies.GetById(id);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de sexo no encontrado" });
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<PersonaTipoSexoDto>> Create([FromBody] PersonaTipoSexoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaTipoSexoDto item = await _bussnies.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<ActionResult<PersonaTipoSexoDto>> Update([FromBody] PersonaTipoSexoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaTipoSexoDto? item = await _bussnies.Update(request);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de sexo no encontrado" });
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            PersonaTipoSexoDto? item = await _bussnies.GetById(id);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de sexo no encontrado" });
            }

            await _bussnies.Delete(id);
            return NoContent();
        }
    }
}
