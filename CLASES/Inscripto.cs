using System;
using System.Collections.Generic;

namespace ProyectoClubHorizonteV._
{
    public abstract class Inscripto
    {
        protected string nombre;
        protected string apellido;
        protected string direccion;
        protected DateTime fechaNacimiento;
        protected int dni;
        protected List<Inscripcion> inscripciones;

        public Inscripto(string nombre, string apellido, string direccion,DateTime fechaNacimiento, int dni)
        {
            this.nombre = nombre;
            this.apellido = apellido;
            this.direccion = direccion;
            this.fechaNacimiento = fechaNacimiento;
            this.dni = dni;
            this.inscripciones = new List<Inscripcion>();
        }

        public string Nombre { 
        	get { return nombre; } }
        public string Apellido { 
        	get { return apellido; } }
        public string Direccion { 
        	get { return direccion; } }
        public DateTime FechaNacimiento { 
        	get { return fechaNacimiento; } }
        public int Dni { 
        	get { return dni; } }
        public List<Inscripcion> Inscripciones { 
        	get { return inscripciones; } }

        public void setNombre(string nombre) { 
        	this.nombre = nombre; }
        public void setApellido(string apellido) { 
        	this.apellido = apellido; }
        public void setDireccion(string direccion) { 
        	this.direccion = direccion; }
        public void setFechaNacimiento(DateTime fechaNacimiento) { 
        	this.fechaNacimiento = fechaNacimiento; }

        
        public abstract double calcularArancel(Actividad actividad);

        public bool estaInscriptoEn(Actividad actividad)
        {
            foreach (var insc in inscripciones)
            {
                if (insc.Actividad == actividad)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format("[Inscripto DNI: {0}, Nombre: {1}, Apellido: {2}]", dni, nombre, apellido);
        }
    }
}