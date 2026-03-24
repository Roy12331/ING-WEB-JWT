using DbModel.demoDb;
using DtoModel.Persona;

namespace Mvc.Repository.PersonaRepos.PersonaTipoSangreRepo.Mapping
{
    public static class PersonaTipoSangreMapping
    {
        public static PersonaTipoSangreDto ToDto(this PersonaTipoSangre item)
        {
            return new PersonaTipoSangreDto
            {
                Id = item.Id,
                Codigo = item.Codigo,
                Descripcion = item.Descripcion
            };
        }

        public static PersonaTipoSangre ToEntity(this PersonaTipoSangreDto item)
        {
            return new PersonaTipoSangre
            {
                Id = item.Id,
                Codigo = item.Codigo,
                Descripcion = item.Descripcion
            };
        }

        public static List<PersonaTipoSangreDto> ToDtoList(this List<PersonaTipoSangre> list)
        {
            return list.Select(x => x.ToDto()).ToList();
        }
    }
}
