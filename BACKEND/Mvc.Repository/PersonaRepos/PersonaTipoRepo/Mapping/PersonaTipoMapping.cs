using DbModel.demoDb;
using DtoModel.Persona;

namespace Mvc.Repository.PersonaRepos.PersonaTipoRepo.Mapping
{
    public static class PersonaTipoMapping
    {
        public static PersonaTipoDto ToDto(this PersonaTipo item)
        {
            return new PersonaTipoDto
            {
                Id = item.Id,
                Codigo = item.Codigo,
                Descripcion = item.Descripcion
            };
        }

        public static PersonaTipo ToEntity(this PersonaTipoDto item)
        {
            return new PersonaTipo
            {
                Id = item.Id,
                Codigo = item.Codigo,
                Descripcion = item.Descripcion
            };
        }

        public static List<PersonaTipoDto> ToDtoList(this List<PersonaTipo> list)
        {
            return list.Select(x => x.ToDto()).ToList();
        }
    }
}
