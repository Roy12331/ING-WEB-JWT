using DtoModel.Persona;
using Mvc.Repository.General.Contratos;

namespace Mvc.Repository.PersonaRepos.PersonaTipoSangreRepo.Contratos
{
    public interface IPersonaTipoSangreRepository : ICrudRepository<PersonaTipoSangreDto>, IDisposable
    {
    }
}
