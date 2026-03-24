using DbModel.demoDb;
using DtoModel.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.PersonaRepos.PersonaTipoDocumentoRepo.Mapping
{
    public static class PersonaTipoDocumentoMapping
    {
        /// <summary>
        /// Cambia el objeto Persona a PersonaTipoDocumentoDto para ser utilizado en la capa de servicio o presentación
        /// </summary>
        /// <param name="persona"></param>
        /// <returns></returns>
        public static PersonaTipoDocumentoDto ToDto(this PersonaTipoDocumento persona)
        {
            return new PersonaTipoDocumentoDto
            {
                Id = persona.Id,
                Codigo = persona.Codigo,
                Descripcion = persona.Descripcion
            };
        }

        /// <summary>
        /// Cambia el objeto PersonaTipoDocumentoDto a Persona para ser utilizado en la capa de acceso a datos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PersonaTipoDocumento ToEntity(this PersonaTipoDocumentoDto request)
        {
            return new PersonaTipoDocumento
            {
                Id = request.Id,
                Codigo = request.Codigo,
                Descripcion = request.Descripcion
            };
        }

        public static List<PersonaTipoDocumentoDto> ToDtoList(this List<PersonaTipoDocumento> personas)
        {
            return personas.Select(p => p.ToDto()).ToList();
        }
        public static List<PersonaTipoDocumento> ToEntityList(this List<PersonaTipoDocumentoDto> PersonaTipoDocumentoDtos)
        {
            return PersonaTipoDocumentoDtos.Select(p => p.ToEntity()).ToList();
        }

    }
}
