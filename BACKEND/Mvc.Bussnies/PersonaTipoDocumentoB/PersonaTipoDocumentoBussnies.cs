using DtoModel.Persona;
using Mvc.Repository.PersonaRepos.PersonaTipoDocumentoRepo.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Bussnies.PersonaTipoDocumentoB
{
    public class PersonaTipoDocumentoBussnies : IPersonaTipoDocumentoBussnies
    {
        private readonly IPersonaTipoDocumentoRepository _repo;

        public PersonaTipoDocumentoBussnies(IPersonaTipoDocumentoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PersonaTipoDocumentoDto>> GetAll()
        {
            List<PersonaTipoDocumentoDto> lista = await _repo.GetAll();
            return lista;
        }

        public async Task<PersonaTipoDocumentoDto?> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<PersonaTipoDocumentoDto> Create(PersonaTipoDocumentoDto request)
        {
            return await _repo.Create(request);
        }

        public async Task<PersonaTipoDocumentoDto?> Update(PersonaTipoDocumentoDto request)
        {
            PersonaTipoDocumentoDto? item = await _repo.GetById(request.Id);

            if (item == null)
            {
                return null;
            }

            return await _repo.Update(request);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }
    }
}
