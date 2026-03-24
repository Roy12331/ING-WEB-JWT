using DtoModel.Persona;
using Mvc.Repository.General.Contratos;

namespace Mvc.Repository.PersonaRepos.PersonaTipoSexoRepo.Contratos
{
    public interface IPersonaTipoSexoRepository : ICrudRepository<PersonaTipoSexoDto>, IDisposable
    {
    }
}
