using DbModel.demoDb;
using DtoModel.Persona;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.PersonaRepos.PersonaTipoSangreRepo.Contratos;
using Mvc.Repository.PersonaRepos.PersonaTipoSangreRepo.Mapping;

namespace Mvc.Repository.PersonaRepos.PersonaTipoSangreRepo.Implementacion
{
    public class PersonaTipoSangreRepository : IPersonaTipoSangreRepository
    {
        private readonly _demoContext _db;

        public PersonaTipoSangreRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<PersonaTipoSangreDto> Create(PersonaTipoSangreDto request)
        {
            PersonaTipoSangre item = request.ToEntity();
            await _db.PersonaTipoSangre.AddAsync(item);
            await _db.SaveChangesAsync();

            return item.ToDto();
        }

        public async Task Delete(int id)
        {
            await _db.PersonaTipoSangre.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<PersonaTipoSangreDto>> GetAll()
        {
            List<PersonaTipoSangre> data = await _db.PersonaTipoSangre.ToListAsync();
            return data.ToDtoList();
        }

        public async Task<PersonaTipoSangreDto?> GetById(int id)
        {
            PersonaTipoSangre? item = await _db.PersonaTipoSangre.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return null;
            }

            return item.ToDto();
        }

        public async Task<PersonaTipoSangreDto> Update(PersonaTipoSangreDto request)
        {
            PersonaTipoSangre? item = await _db.PersonaTipoSangre.FindAsync(request.Id);

            if (item == null)
            {
                throw new Exception("Tipo de sangre no encontrado");
            }

            item.Codigo = request.Codigo;
            item.Descripcion = request.Descripcion;

            await _db.SaveChangesAsync();

            return item.ToDto();
        }
    }
}
