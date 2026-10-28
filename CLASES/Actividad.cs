/*
 * Created by SharpDevelop.
 * User: Brian
 * Date: 20/9/2026
 * Time: 13:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace ProyectoClubHorizonteV._
{
    /// <summary>
    /// Description of Actividad.
    /// </summary>
    public class Actividad
    {
        public string nombre;
        private int edadMinima;
        private int edadMaxima;
        private int cupoMaximo;
        private string dia;
        private DateTime horario;
        private double arancelMensual;
        private double descuentoSocios = 0.15;
        private Profesor profesor;
        public List<Inscripcion> inscripciones;


        public double DescuentoSocios
        {
            get { return descuentoSocios; }
            set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentException("El descuento debe estar entre 0 y 1 (por ejemplo 0.15 para 15%).");
                descuentoSocios = value;
            }
        }
        public Actividad(string nombre, int edadMinima, int edadMaxima, int cupoMaximo, string dia, DateTime horario, double arancelMensual, Profesor profesor)
        {
            Nombre = nombre;
            EdadMinima = edadMinima;
            EdadMaxima = edadMaxima;
            CupoMaximo = cupoMaximo;
            Dia = dia;
            Horario = horario;
            ArancelMensual = arancelMensual;
            this.profesor = profesor;
            inscripciones = new List<Inscripcion>();

        }


        // METODOS


        public int CantidadInscriptos
        {
            get { return inscripciones.Count; }
        }



        public bool TieneCupoDisponible()
        {
            return inscripciones.Count < cupoMaximo;
        }

        public bool PermiteEdad(Inscripto alumno)
        {
            if (alumno == null)
                return false;

            return alumno.Edad >= edadMinima && alumno.Edad <= edadMaxima;
        }

        public bool MismoHorario(Actividad otra)
        {
            if (otra == null)
                return false;

            return String.Equals(Dia.Trim(), otra.Dia.Trim(),
                                 StringComparison.OrdinalIgnoreCase)
                   && Horario.TimeOfDay == otra.Horario.TimeOfDay;
        }

        private void AsignarProfesor(Profesor nuevoProfesor)
        {
            profesor = nuevoProfesor;
        }

        public void AgregarInscripcion(Inscripcion inscripcion)
        {
            if (inscripcion == null)
                throw new ArgumentNullException("inscripcion");

            if (!inscripciones.Contains(inscripcion))
                inscripciones.Add(inscripcion);
        }

        public void QuitarInscripcion(Inscripcion inscripcion)
        {
            if (inscripcion != null)
                inscripciones.Remove(inscripcion);
        }


        // PROPIEDADES


        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre de la actividad no puede estar vacío.");
                nombre = value;
            }
        }

        public int EdadMinima
        {
            get { return edadMinima; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("La edad mínima no puede ser negativa.");
                edadMinima = value;
            }
        }

        public int EdadMaxima
        {
            get { return edadMaxima; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("La edad máxima no puede ser negativa.");
                edadMaxima = value;
            }
        }

        public int CupoMaximo
        {
            get { return cupoMaximo; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El cupo máximo debe ser mayor a cero.");
                if (inscripciones != null && value < inscripciones.Count)
                    throw new InvalidOperationException(
                        "El cupo no puede ser menor que la cantidad actual de inscriptos.");
                cupoMaximo = value;
            }
        }

        public string Dia
        {
            get { return dia; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El día no puede estar vacío.");
                dia = value;
            }
        }

        public DateTime Horario
        {
            get { return horario; }
            set { horario = value; }
        }

        public double ArancelMensual
        {
            get { return arancelMensual; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("El arancel no puede ser negativo.");
                arancelMensual = value;
            }
        }


        public Profesor Profesor
        {
            get { return profesor; }
        }

        public List<Inscripcion> Inscripciones
        {
            get { return new List<Inscripcion>(inscripciones); }
        }

    }
}
