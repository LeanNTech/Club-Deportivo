using System;
using System.Collections.Generic;

namespace ProyectoClubHorizonteV._
{
    public class ClubHorizonte

    {
        private string nombre;
        private string direccion;

        private List<Actividad> listaActividades;
        private List<Inscripto> listaInscriptos;
        private List<Profesor> listaProfesores;

        public ClubHorizonte(string nombre, string direccion)
        {
            this.nombre = nombre;
            this.direccion = direccion;

            listaActividades = new List<Actividad>();
            listaInscriptos = new List<Inscripto>();
            listaProfesores = new List<Profesor>();
        }

        // inscriPTOS

        public void RegistrarInscripto(Inscripto i)
        {
            if (BuscarInscripto(i.Dni) != null)
                throw new DniDuplicadoException("Ya existe un inscripto con el DNI " + i.Dni + ".");

            listaInscriptos.Add(i);
        }

        public Inscripto BuscarInscripto(int dni)
        {
            foreach (Inscripto inscripto in listaInscriptos)
            {
                if (inscripto.Dni == dni)
                {
                    return inscripto;
                }
            }

            return null;
        }

        public void EliminarInscripto(Inscripto i)
        {
            if (!listaInscriptos.Contains(i))
                throw new NoRegistradoException("El inscripto no está registrado en el club.");

            // libera los cupos de las actividades donde estaba anotado
            foreach (Inscripcion insc in i.Inscripciones)
            {
                insc.Actividad.QuitarInscripcion(insc);
            }

            i.Inscripciones.Clear();
            listaInscriptos.Remove(i);
        }

        // profesores

        public void RegistrarProfesor(Profesor p)
        {
            if (BuscarProfesor(p.Matricula) != null)
                throw new MatriculaDuplicadaException("Ya existe un profesor con la matrícula " + p.Matricula + ".");

            listaProfesores.Add(p);
        }

        public Profesor BuscarProfesor(int matricula)
        {
            foreach (Profesor profesor in listaProfesores)
            {
                if (profesor.Matricula == matricula)
                {
                    return profesor;
                }
            }

            return null;
        }

        public void EliminarProfesor(Profesor p)
        {
            if (!listaProfesores.Contains(p))
                throw new NoRegistradoException("El profesor no está registrado en el club.");

            if (p.Actividades.Count > 0)
                throw new ProfesorConActividadesException(
                    "No se puede eliminar al profesor porque tiene actividades asignadas.");

            listaProfesores.Remove(p);
        }

        // ACTIVIDADES

        public void AgregarActividad(Actividad a)
        {
            if (a.Profesor == null || !listaProfesores.Contains(a.Profesor))
                throw new NoRegistradoException("El profesor de la actividad no está registrado en el club.");

            if (BuscarActividad(a.Nombre) != null)
                throw new ClubException("Ya existe una actividad con el nombre '" + a.Nombre + "'.");

            listaActividades.Add(a);
            a.Profesor.AgregarActividad(a); // el profesor registra la actividad que dicta
        }

        public Actividad BuscarActividad(string nombre)
        {
            foreach (Actividad actividad in listaActividades)
            {
                if (string.Equals(actividad.Nombre, nombre, StringComparison.OrdinalIgnoreCase))
                {
                    return actividad;
                }
            }

            return null;
        }

        // INSCRIPCIONES

        public void InscribirEnActividad(Inscripto i, Actividad a)
        {
            if (!listaInscriptos.Contains(i))
                throw new NoRegistradoException("El inscripto no esta registrado en el club.");

            if (!listaActividades.Contains(a))
                throw new NoRegistradoException("La actividad no esta registrada en el club.");

            if (i.estaInscriptoEn(a))
                throw new InscripcionDuplicadaException(
                    "El inscripto ya esta anotado en la actividad '" + a.Nombre + "'.");

            if (!a.PermiteEdad(i))
                throw new EdadNoPermitidaException(
                    "La edad de " + i.Edad + " años no corresponde a la actividad '" + a.Nombre + "'.");

            if (!a.TieneCupoDisponible())
                throw new CupoCompletoException(
                    "La actividad '" + a.Nombre + "' no tiene cupo disponible.");

            foreach (Inscripcion existente in i.Inscripciones)
            {
                if (existente.Actividad.MismoHorario(a))
                    throw new HorarioSuperpuestoException(
                        "El inscripto ya tiene la actividad '" + existente.Actividad.Nombre +
                        "' en el mismo día y horario.");
            }

            Inscripcion nueva = new Inscripcion(DateTime.Now, i, a);
            a.AgregarInscripcion(nueva);
            i.Inscripciones.Add(nueva);
        }

        // REcaudacion (polimorfismo) 

        // aranceles con descuentso + cuota social (una sola vez por socio)
        public double CalcularRecaudacionSocios()
        {
            double total = 0;

            foreach (Inscripto inscripto in listaInscriptos)
            {
                InscriptoSocio socio = inscripto as InscriptoSocio;

                // Si no es socio, se salta al siguiente inscripto
                if (socio == null)
                    continue;

                total += socio.CuotaSocial;


                //  recorre las inscripciones del socio y acumula el arancel correspondiente
                foreach (Inscripcion insc in socio.Inscripciones)
                    total += socio.calcularArancel(insc.Actividad);
            }

            return total;
        }

        // Aranceles completos de los no socios
        public double CalcularRecaudacionNoSocios()
        {
            double total = 0;

            // recorre los inscriptos y acumula el arancel correspondiente de los no socios
            foreach (Inscripto inscripto in listaInscriptos)
            {
                if (inscripto is InscriptoSocio)
                    continue;

                foreach (Inscripcion insc in inscripto.Inscripciones)
                    total += inscripto.calcularArancel(insc.Actividad);
            }

            return total;
        }

        public double CalcularRecaudacionMensual()
        {
            return CalcularRecaudacionSocios() + CalcularRecaudacionNoSocios();
        }

        // reportes

        public List<Profesor> ReporteProfesores()
        {
            return new List<Profesor>(listaProfesores);
        }

        public List<Inscripto> ReporteAlumnosNoInscriptos()
        {
            List<Inscripto> noInscriptos = new List<Inscripto>();

            foreach (Inscripto inscripto in listaInscriptos)
            {
                if (inscripto.Inscripciones.Count == 0)
                {
                    noInscriptos.Add(inscripto);
                }
            }

            return noInscriptos;
        }
    }



    public class ClubException : Exception
    {
        public ClubException(string mensaje) : base(mensaje) { }
    }


    public class CupoCompletoException : ClubException
    {
        public CupoCompletoException(string mensaje) : base(mensaje) { }
    }

    public class EdadNoPermitidaException : ClubException
    {
        public EdadNoPermitidaException(string mensaje) : base(mensaje) { }
    }

    public class HorarioSuperpuestoException : ClubException
    {
        public HorarioSuperpuestoException(string mensaje) : base(mensaje) { }
    }

    public class InscripcionDuplicadaException : ClubException
    {
        public InscripcionDuplicadaException(string mensaje) : base(mensaje) { }
    }

    public class DniDuplicadoException : ClubException
    {
        public DniDuplicadoException(string mensaje) : base(mensaje) { }
    }

    public class MatriculaDuplicadaException : ClubException
    {
        public MatriculaDuplicadaException(string mensaje) : base(mensaje) { }
    }

    public class ProfesorConActividadesException : ClubException
    {
        public ProfesorConActividadesException(string mensaje) : base(mensaje) { }
    }

    public class NoRegistradoException : ClubException
    {
        public NoRegistradoException(string mensaje) : base(mensaje) { }
    }




}


