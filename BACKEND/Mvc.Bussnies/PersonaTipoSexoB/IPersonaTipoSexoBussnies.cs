using DtoModel.Persona;

namespace Mvc.Bussnies.PersonaTipoSexoB
{
    public interface IPersonaTipoSexoBussnies
    {
        Task<List<PersonaTipoSexoDto>> GetAll();
        Task<PersonaTipoSexoDto?> GetById(int id);
        Task<PersonaTipoSexoDto> Create(PersonaTipoSexoDto request);
        Task<PersonaTipoSexoDto?> Update(PersonaTipoSexoDto request);
        Task Delete(int id);
    }
}
