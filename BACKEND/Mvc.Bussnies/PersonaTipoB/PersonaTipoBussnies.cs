using DtoModel.Persona;
using Mvc.Repository.PersonaRepos.PersonaTipoRepo.Contratos;

namespace Mvc.Bussnies.PersonaTipoB
{
    public class PersonaTipoBussnies : IPersonaTipoBussnies
    {
        private readonly IPersonaTipoRepository _repo;

        public PersonaTipoBussnies(IPersonaTipoRepository repo)
        {
            _repo = repo;
        }

        public async Task<PersonaTipoDto> Create(PersonaTipoDto request)
        {
            return await _repo.Create(request);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }

        public async Task<List<PersonaTipoDto>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<PersonaTipoDto?> GetById(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task<PersonaTipoDto?> Update(PersonaTipoDto request)
        {
            PersonaTipoDto? item = await _repo.GetById(request.Id);

            if (item == null)
            {
                return null;
            }

            return await _repo.Update(request);
        }
    }
}
