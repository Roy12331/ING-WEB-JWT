using DtoModel.Persona;
using Mvc.Repository.General.Contratos;

namespace Mvc.Repository.PersonaRepos.PersonaTipoRepo.Contratos
{
    public interface IPersonaTipoRepository : ICrudRepository<PersonaTipoDto>, IDisposable
    {
    }
}
