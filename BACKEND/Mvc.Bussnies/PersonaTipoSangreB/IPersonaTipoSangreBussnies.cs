using DtoModel.Persona;

namespace Mvc.Bussnies.PersonaTipoSangreB
{
    public interface IPersonaTipoSangreBussnies
    {
        Task<List<PersonaTipoSangreDto>> GetAll();
        Task<PersonaTipoSangreDto?> GetById(int id);
        Task<PersonaTipoSangreDto> Create(PersonaTipoSangreDto request);
        Task<PersonaTipoSangreDto?> Update(PersonaTipoSangreDto request);
        Task Delete(int id);
    }
}
