using DtoModel.Persona;
using Mvc.Repository.PersonaRepos.PersonaTipoSangreRepo.Contratos;

namespace Mvc.Bussnies.PersonaTipoSangreB
{
    public class PersonaTipoSangreBussnies : IPersonaTipoSangreBussnies
    {
        private readonly IPersonaTipoSangreRepository _repo;

        public PersonaTipoSangreBussnies(IPersonaTipoSangreRepository repo)
        {
            _repo = repo;
        }

        public async Task<PersonaTipoSangreDto> Create(PersonaTipoSangreDto request)
        {
            return await _repo.Create(request);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<PersonaTipoSangreDto>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<PersonaTipoSangreDto?> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<PersonaTipoSangreDto?> Update(PersonaTipoSangreDto request)
        {
            PersonaTipoSangreDto? item = await _repo.GetById(request.Id);

            if (item == null)
            {
                return null;
            }

            return await _repo.Update(request);
        }
    }
}
