using DtoModel.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Bussnies.PersonaTipoDocumentoB
{
    public interface IPersonaTipoDocumentoBussnies
    {
        Task<List<PersonaTipoDocumentoDto>> GetAll();
        Task<PersonaTipoDocumentoDto?> GetById(int id);
        Task<PersonaTipoDocumentoDto> Create(PersonaTipoDocumentoDto request);
        Task<PersonaTipoDocumentoDto?> Update(PersonaTipoDocumentoDto request);
        Task Delete(int id);

    }
}
