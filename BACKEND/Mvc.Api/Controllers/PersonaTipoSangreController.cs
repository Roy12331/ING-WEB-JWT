using DtoModel.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.PersonaTipoSangreB;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonaTipoSangreController : ControllerBase
    {
        private readonly IPersonaTipoSangreBussnies _bussnies;

        public PersonaTipoSangreController(IPersonaTipoSangreBussnies bussnies)
        {
            _bussnies = bussnies;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonaTipoSangreDto>>> GetAll()
        {
            return Ok(await _bussnies.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaTipoSangreDto>> GetById(int id)
        {
            PersonaTipoSangreDto? item = await _bussnies.GetById(id);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de sangre no encontrado" });
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<PersonaTipoSangreDto>> Create([FromBody] PersonaTipoSangreDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaTipoSangreDto item = await _bussnies.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<ActionResult<PersonaTipoSangreDto>> Update([FromBody] PersonaTipoSangreDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaTipoSangreDto? item = await _bussnies.Update(request);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de sangre no encontrado" });
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            PersonaTipoSangreDto? item = await _bussnies.GetById(id);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de sangre no encontrado" });
            }

            await _bussnies.Delete(id);
            return NoContent();
        }
    }
}
