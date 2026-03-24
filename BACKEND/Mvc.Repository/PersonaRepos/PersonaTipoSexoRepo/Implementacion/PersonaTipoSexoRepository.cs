using DbModel.demoDb;
using DtoModel.Persona;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.PersonaRepos.PersonaTipoSexoRepo.Contratos;
using Mvc.Repository.PersonaRepos.PersonaTipoSexoRepo.Mapping;

namespace Mvc.Repository.PersonaRepos.PersonaTipoSexoRepo.Implementacion
{
    public class PersonaTipoSexoRepository : IPersonaTipoSexoRepository
    {
        private readonly _demoContext _db;

        public PersonaTipoSexoRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<PersonaTipoSexoDto> Create(PersonaTipoSexoDto request)
        {
            PersonaTipoSexo item = request.ToEntity();
            await _db.PersonaTipoSexo.AddAsync(item);
            await _db.SaveChangesAsync();

            return item.ToDto();
        }

        public async Task Delete(int id)
        {
            await _db.PersonaTipoSexo.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<PersonaTipoSexoDto>> GetAll()
        {
            List<PersonaTipoSexo> data = await _db.PersonaTipoSexo.ToListAsync();
            return data.ToDtoList();
        }

        public async Task<PersonaTipoSexoDto?> GetById(int id)
        {
            PersonaTipoSexo? item = await _db.PersonaTipoSexo.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return null;
            }

            return item.ToDto();
        }

        public async Task<PersonaTipoSexoDto> Update(PersonaTipoSexoDto request)
        {
            PersonaTipoSexo? item = await _db.PersonaTipoSexo.FindAsync(request.Id);

            if (item == null)
            {
                throw new Exception("Tipo de sexo no encontrado");
            }

            item.Codigo = request.Codigo;
            item.Descripcion = request.Descripcion;

            await _db.SaveChangesAsync();

            return item.ToDto();
        }
    }
}
