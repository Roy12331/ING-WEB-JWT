using DtoModel.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.Persona;
using Mvc.Bussnies.PersonaTipoDocumentoB;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonaTipoDocumentoController : ControllerBase
    {
        private readonly IPersonaTipoDocumentoBussnies _bussnies;

        public PersonaTipoDocumentoController(IPersonaTipoDocumentoBussnies bussnies)
        {
            _bussnies = bussnies;
        }


        [HttpGet]
        public async Task<ActionResult<List<PersonaTipoDocumentoDto>>> Get()
        {

            return Ok(await _bussnies.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaTipoDocumentoDto>> GetById(int id)
        {
            PersonaTipoDocumentoDto? item = await _bussnies.GetById(id);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de documento no encontrado" });
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<PersonaTipoDocumentoDto>> Create([FromBody] PersonaTipoDocumentoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaTipoDocumentoDto item = await _bussnies.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<ActionResult<PersonaTipoDocumentoDto>> Update([FromBody] PersonaTipoDocumentoDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaTipoDocumentoDto? item = await _bussnies.Update(request);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de documento no encontrado" });
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            PersonaTipoDocumentoDto? item = await _bussnies.GetById(id);

            if (item == null)
            {
                return NotFound(new { message = "Tipo de documento no encontrado" });
            }

            await _bussnies.Delete(id);
            return NoContent();
        }
    }
}



