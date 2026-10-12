using System;
namespace ProyectoClubHorizonteV._
{
    public class InscriptoNoSocio : Inscripto
    {
        public InscriptoNoSocio(string nombre, string apellido, string direccion, int edad, DateTime fechaNacimiento, int dni)
            : base(nombre, apellido, direccion, edad, fechaNacimiento, dni)
        {
        }
        
        public override double calcularArancel(Actividad actividad)
        {
            return actividad.ArancelMensual;
        }
    }
}