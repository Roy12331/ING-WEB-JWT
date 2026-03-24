using DbModel.demoDb;
using DtoModel.Persona;
using Microsoft.EntityFrameworkCore;
using Mvc.Repository.PersonaRepos.PersonaTipoDocumentoRepo.Contratos;
using Mvc.Repository.PersonaRepos.PersonaTipoDocumentoRepo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Repository.PersonaRepos.PersonaTipoDocumentoRepo.Implementacion
{
    public class PersonaTipoDocumentoRepository : IPersonaTipoDocumentoRepository
    {

        private readonly _demoContext _db;

        //Estamos generando un constructor 
        //este nos permite inicializar cualquier recurso que necesitemos
        //para nuestra clase, como conexiones a bases de datos, servicios externos, etc.
        public PersonaTipoDocumentoRepository(_demoContext db)
        {
            _db = db;
        }

        public async Task<PersonaTipoDocumentoDto> Create(PersonaTipoDocumentoDto request)
        {
            PersonaTipoDocumento item = request.ToEntity();
            await _db.PersonaTipoDocumento.AddAsync(item);
            await _db.SaveChangesAsync();

            return item.ToDto();
        }

        public async Task Delete(int id)
        {
            await _db.PersonaTipoDocumento.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<List<PersonaTipoDocumentoDto>> GetAll()
        {
            List<PersonaTipoDocumentoDto> res = new List<PersonaTipoDocumentoDto>();
            List<PersonaTipoDocumento> data = await _db.PersonaTipoDocumento.ToListAsync();
            res = PersonaTipoDocumentoMapping.ToDtoList(data);
            return res;
        }

        public async Task<PersonaTipoDocumentoDto?> GetById(int id)
        {
            PersonaTipoDocumento? item = await _db.PersonaTipoDocumento.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (item == null)
            {
                return null;
            }

            return item.ToDto();
        }

        public async Task<PersonaTipoDocumentoDto> Update(PersonaTipoDocumentoDto request)
        {
            PersonaTipoDocumento? item = await _db.PersonaTipoDocumento.FindAsync(request.Id);

            if (item == null)
            {
                throw new Exception("Tipo de documento no encontrado");
            }

            item.Codigo = request.Codigo;
            item.Descripcion = request.Descripcion;

            await _db.SaveChangesAsync();

            return item.ToDto();
        }
    }
}
