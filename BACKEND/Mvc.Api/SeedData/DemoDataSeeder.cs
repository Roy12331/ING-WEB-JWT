using DbModel.demoDb;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Api.SeedData;

public static class DemoDataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<_demoContext>();

        if (!await context.Database.CanConnectAsync())
        {
            throw new InvalidOperationException("No fue posible conectarse a la base de datos demo_db.");
        }

        if (!await context.PersonaTipo.AnyAsync())
        {
            context.PersonaTipo.AddRange(
                new PersonaTipo { Codigo = "NAT", Descripcion = "Natural" },
                new PersonaTipo { Codigo = "JUR", Descripcion = "Juridica" },
                new PersonaTipo { Codigo = "EXT", Descripcion = "Extranjero" }
            );
        }

        if (!await context.PersonaTipoDocumento.AnyAsync())
        {
            context.PersonaTipoDocumento.AddRange(
                new PersonaTipoDocumento { Codigo = "DNI", Descripcion = "Documento Nacional" },
                new PersonaTipoDocumento { Codigo = "PAS", Descripcion = "Pasaporte" },
                new PersonaTipoDocumento { Codigo = "RUC", Descripcion = "Registro Unico" },
                new PersonaTipoDocumento { Codigo = "CE", Descripcion = "Carnet Extranjeria" }
            );
        }

        if (!await context.PersonaTipoSexo.AnyAsync())
        {
            context.PersonaTipoSexo.AddRange(
                new PersonaTipoSexo { Codigo = "M", Descripcion = "Masculino" },
                new PersonaTipoSexo { Codigo = "F", Descripcion = "Femenino" },
                new PersonaTipoSexo { Codigo = "O", Descripcion = "Otro" }
            );
        }

        if (!await context.PersonaTipoSangre.AnyAsync())
        {
            context.PersonaTipoSangre.AddRange(
                new PersonaTipoSangre { Codigo = "A+", Descripcion = "A Positivo" },
                new PersonaTipoSangre { Codigo = "A-", Descripcion = "A Negativo" },
                new PersonaTipoSangre { Codigo = "B+", Descripcion = "B Positivo" },
                new PersonaTipoSangre { Codigo = "O+", Descripcion = "O Positivo" },
                new PersonaTipoSangre { Codigo = "AB+", Descripcion = "AB Positivo" }
            );
        }

        await context.SaveChangesAsync();

        if (await context.Persona.AnyAsync())
        {
            return;
        }

        var tiposPersona = await context.PersonaTipo.Select(x => x.Id).ToListAsync();
        var tiposDocumento = await context.PersonaTipoDocumento.Select(x => x.Id).ToListAsync();
        var tiposSexo = await context.PersonaTipoSexo.Select(x => x.Id).ToListAsync();
        var tiposSangre = await context.PersonaTipoSangre.Select(x => x.Id).ToListAsync();

        if (!tiposPersona.Any() || !tiposDocumento.Any() || !tiposSexo.Any() || !tiposSangre.Any())
        {
            throw new InvalidOperationException("No hay datos base suficientes para poblar la tabla persona.");
        }

        var nombres = new[]
        {
            "Carlos", "Maria", "Lucia", "Jose", "Ana", "Pedro", "Rosa", "Miguel", "Sofia", "Diego"
        };
        var apellidos = new[]
        {
            "Gomez", "Lopez", "Martinez", "Perez", "Torres", "Sanchez", "Vargas", "Diaz", "Rojas", "Flores"
        };

        var personas = new List<Persona>();
        for (var i = 0; i < 20; i++)
        {
            personas.Add(new Persona
            {
                IdPersonaTipo = tiposPersona[i % tiposPersona.Count],
                IdTipoDocumento = tiposDocumento[i % tiposDocumento.Count],
                IdTipoSexo = tiposSexo[i % tiposSexo.Count],
                IdTipoSangre = tiposSangre[i % tiposSangre.Count],
                Nombres = nombres[i % nombres.Length],
                ApellidoPaterno = apellidos[i % apellidos.Length],
                ApellidoMaterno = apellidos[(i + 3) % apellidos.Length],
                Direccion = $"Calle Demo {100 + i}",
                Telefono = $"999000{(10 + i):00}",
                UserCreate = 1,
                UserUpdate = 1
            });
        }

        context.Persona.AddRange(personas);
        await context.SaveChangesAsync();
    }
}
