using DbModel.demoDb;
using DtoModel.Persona;

namespace Mvc.Repository.PersonaRepos.PersonaTipoSexoRepo.Mapping
{
    public static class PersonaTipoSexoMapping
    {
        public static PersonaTipoSexoDto ToDto(this PersonaTipoSexo item)
        {
            return new PersonaTipoSexoDto
            {
                Id = item.Id,
                Codigo = item.Codigo,
                Descripcion = item.Descripcion
            };
        }

        public static PersonaTipoSexo ToEntity(this PersonaTipoSexoDto item)
        {
            return new PersonaTipoSexo
            {
                Id = item.Id,
                Codigo = item.Codigo,
                Descripcion = item.Descripcion
            };
        }

        public static List<PersonaTipoSexoDto> ToDtoList(this List<PersonaTipoSexo> list)
        {
            return list.Select(x => x.ToDto()).ToList();
        }
    }
}
