using DtoModel.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.PersonaTipoB;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonaTipoController : ControllerBase
    {
        private readonly IPersonaTipoBussnies _bussnies;

        public PersonaTipoController(IPersonaTipoBussnies bussnies)
        {
            _bussnies = bussnies;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonaTipoDto>>> GetAll()
        {
            return Ok(await _bussnies.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaTipoDto>> GetById(int id)
        {
            PersonaTipoDto? item = await _bussnies.GetById(id);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de persona no encontrado" });
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<PersonaTipoDto>> Create([FromBody] PersonaTipoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaTipoDto item = await _bussnies.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<ActionResult<PersonaTipoDto>> Update([FromBody] PersonaTipoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaTipoDto? item = await _bussnies.Update(request);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de persona no encontrado" });
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            PersonaTipoDto? item = await _bussnies.GetById(id);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de persona no encontrado" });
            }

            await _bussnies.Delete(id);
            return NoContent();
        }
    }
}
