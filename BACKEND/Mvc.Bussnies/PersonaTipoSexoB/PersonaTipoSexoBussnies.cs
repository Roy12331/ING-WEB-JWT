using DtoModel.Persona;
using Mvc.Repository.PersonaRepos.PersonaTipoSexoRepo.Contratos;

namespace Mvc.Bussnies.PersonaTipoSexoB
{
    public class PersonaTipoSexoBussnies : IPersonaTipoSexoBussnies
    {
        private readonly IPersonaTipoSexoRepository _repo;

        public PersonaTipoSexoBussnies(IPersonaTipoSexoRepository repo)
        {
            _repo = repo;
        }

        public async Task<PersonaTipoSexoDto> Create(PersonaTipoSexoDto request)
        {
            return await _repo.Create(request);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<PersonaTipoSexoDto>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<PersonaTipoSexoDto?> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<PersonaTipoSexoDto?> Update(PersonaTipoSexoDto request)
        {
            PersonaTipoSexoDto? item = await _repo.GetById(request.Id);

            if (item == null)
            {
                return null;
            }

            return await _repo.Update(request);
        }
    }
}
