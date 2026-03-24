using DtoModel.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.Persona;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonaController : ControllerBase
    {

        private readonly IPersonaBussnies _personaBussnies;

        public PersonaController(IPersonaBussnies personaBussnies)
        {
            _personaBussnies = personaBussnies;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonaDto>>> GetAll()
        {

            //double a = 1, b = 2, c = 3, d = 0;

            // Esto causará una excepción de división por cero
            // error matematico
            //if (d == 0)
            //{
                //throw new Exception("Error matematico: División por cero");
            //}

            List<PersonaDto> list = await _personaBussnies.GetAll();

            return Ok(list);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PersonaDto>> GetById(int id)
        {
            PersonaDto? persona = await _personaBussnies.GetById(id);

            if (persona == null)
            {
                return NotFound(new { message = "Persona no encontrada" });
            }

            return Ok(persona);
        }

        [HttpPost]
        public async Task<ActionResult<PersonaDto>> Create([FromBody] PersonaDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaDto persona = await _personaBussnies.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = persona.Id }, persona);
        }

        [HttpPut]
        public async Task<ActionResult<PersonaDto>> Update([FromBody] PersonaDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PersonaDto? persona = await _personaBussnies.Update(request);

            if (persona == null)
            {
                return NotFound(new { message = "Persona no encontrada" });
            }

            return Ok(persona);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            PersonaDto? persona = await _personaBussnies.GetById(id);

            if (persona == null)
            {
                return NotFound(new { message = "Persona no encontrada" });
            }

            await _personaBussnies.Delete(id);

            return NoContent();
        }


    }
}
