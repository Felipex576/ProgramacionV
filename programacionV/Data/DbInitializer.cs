using programacionV.Models;

namespace programacionV.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        // Verificar si ya existen datos
        if (context.ProgramasAcademicos.Any())
        {
            return; // La base de datos ya fue inicializada con datos
        }

        var programas = new List<ProgramaAcademico>
        {
            new ProgramaAcademico
            {
                Nombre = "Ingeniería de Sistemas",
                Codigo = "ING-SIS",
                Facultad = "Facultad de Ingeniería",
                Sede = "Sede Principal"
            },
            new ProgramaAcademico
            {
                Nombre = "Medicina",
                Codigo = "MED-01",
                Facultad = "Facultad de Ciencias de la Salud",
                Sede = "Sede Salud"
            },
            new ProgramaAcademico
            {
                Nombre = "Derecho",
                Codigo = "DER-01",
                Facultad = "Facultad de Ciencias Jurídicas",
                Sede = "Sede Principal"
            }
        };

        context.ProgramasAcademicos.AddRange(programas);
        context.SaveChanges();

        var estudiantes = new List<Estudiante>
        {
            new Estudiante
            {
                Nombres = "Juan Carlos",
                Apellidos = "Pérez Gómez",
                Documento = "1020304050",
                Email = "juan.perez@universidad.edu",
                ProgramaAcademicoId = programas[0].Id
            },
            new Estudiante
            {
                Nombres = "María Camila",
                Apellidos = "Rodríguez López",
                Documento = "1030405060",
                Email = "maria.rodriguez@universidad.edu",
                ProgramaAcademicoId = programas[0].Id
            },
            new Estudiante
            {
                Nombres = "Andrés Felipe",
                Apellidos = "Martínez Silva",
                Documento = "1040506070",
                Email = "andres.martinez@universidad.edu",
                ProgramaAcademicoId = programas[1].Id
            },
            new Estudiante
            {
                Nombres = "Laura Sofía",
                Apellidos = "Castro Díaz",
                Documento = "1050607080",
                Email = "laura.castro@universidad.edu",
                ProgramaAcademicoId = programas[2].Id
            }
        };

        context.Estudiantes.AddRange(estudiantes);
        context.SaveChanges();
    }
}
