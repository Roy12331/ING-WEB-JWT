using DbModel.demoDb;
using DtoModel.Persona;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.PersonaRepos.PersonaTipoRepo.Contratos;
using Mvc.Repository.PersonaRepos.PersonaTipoRepo.Mapping;

namespace Mvc.Repository.PersonaRepos.PersonaTipoRepo.Implementacion
{
    public class PersonaTipoRepository : IPersonaTipoRepository
    {
        private readonly _demoContext _db;

        public PersonaTipoRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<PersonaTipoDto> Create(PersonaTipoDto request)
        {
            PersonaTipo item = request.ToEntity();
            await _db.PersonaTipo.AddAsync(item);
            await _db.SaveChangesAsync();

            return item.ToDto();
        }

        public async Task Delete(int id)
        {
            await _db.PersonaTipo.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<PersonaTipoDto>> GetAll()
        {
            List<PersonaTipo> data = await _db.PersonaTipo.ToListAsync();
            return data.ToDtoList();
        }

        public async Task<PersonaTipoDto?> GetById(int id)
        {
            PersonaTipo? item = await _db.PersonaTipo.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return null;
            }

            return item.ToDto();
        }

        public async Task<PersonaTipoDto> Update(PersonaTipoDto request)
        {
            PersonaTipo? item = await _db.PersonaTipo.FindAsync(request.Id);

            if (item == null)
            {
                throw new Exception("Tipo de persona no encontrado");
            }

            item.Codigo = request.Codigo;
            item.Descripcion = request.Descripcion;

            await _db.SaveChangesAsync();

            return item.ToDto();
        }
    }
}
