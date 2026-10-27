using System;
namespace ProyectoClubHorizonteV._
{
    public class InscriptoSocio : Inscripto
    {
        private double cuotaSocial;

        public InscriptoSocio(string nombre, string apellido, string direccion, int edad, DateTime fechaNacimiento, int dni, double cuotaSocial)
            : base(nombre, apellido, direccion, edad, fechaNacimiento, dni)
        {
            this.cuotaSocial = cuotaSocial;
        }

        public double CuotaSocial
        {
        	get { return cuotaSocial; }
            set { cuotaSocial = value; }
        }

        public override double calcularArancel(Actividad actividad)
        {
        	return actividad.ArancelMensual * (1 - actividad.DescuentoSocios);
        }
    }
}