using DtoModel.Persona;

namespace Mvc.Bussnies.PersonaTipoB
{
    public interface IPersonaTipoBussnies
    {
        Task<List<PersonaTipoDto>> GetAll();
        Task<PersonaTipoDto?> GetById(int id);
        Task<PersonaTipoDto> Create(PersonaTipoDto request);
        Task<PersonaTipoDto?> Update(PersonaTipoDto request);
        Task Delete(int id);
    }
}
