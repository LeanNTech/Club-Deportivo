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
    /// Description of Profesor.
    /// </summary>
    public class Profesor
    {
        private string nombre;
        private string apellido;
        private int matricula;
        private DateTime horarioAtencion;
        private List<Actividad> actividades;



        public Profesor(string nom, string ape, int matri, DateTime hoaten)
        {
            Nombre = nom;
            Apellido = ape;
            Matricula = matri;
            HorarioAtencion = hoaten;
            actividades = new List<Actividad>();
        }



        // PROPIEDADES

        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre no puede estar vacío.");
                nombre = value;
            }
        }

        public string Apellido
        {
            get { return apellido; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El apellido no puede estar vacío.");
                apellido = value;
            }
        }

        public int Matricula
        {
            get { return matricula; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("La matrícula debe ser mayor a cero.");
                matricula = value;
            }
        }

        public DateTime HorarioAtencion
        {
            get { return horarioAtencion; }
            set { horarioAtencion = value; }
        }

        public List<Actividad> Actividades
        {
            get { return new List<Actividad>(actividades); }
        }


        // METODOS

        public void AgregarActividad(Actividad actividad)
        {
            if (actividad == null)
                throw new ArgumentNullException("actividad");

            if (!actividades.Contains(actividad))
                actividades.Add(actividad);
        }


        private void QuitarActividad(Actividad actividad)
        {
            if (actividad != null)
                actividades.Remove(actividad);
        }


        public int CantidadAlumnosTotales()
        {
            int total = 0;

            foreach (Actividad actividad in actividades)
                total += actividad.CantidadInscriptos;

            return total;
        }


        public override string ToString()
        {
            return Matricula + " - " + Nombre + " " + Apellido +
                   " | Actividades: " + actividades.Count;
        }




    }
}
