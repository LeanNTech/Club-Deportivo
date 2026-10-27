using System;

namespace ProyectoClubHorizonteV._
{
    public class Inscripcion
    {
        private DateTime fechaInscripcion;
        private double montoAbonado;
        private Inscripto inscripto;
        private Actividad actividad;

        public Inscripcion(DateTime fecha, Inscripto inscripto, Actividad actividad)
        {
            if (inscripto == null)
                throw new ArgumentNullException("inscripto");
            if (actividad == null)
                throw new ArgumentNullException("actividad");

            fechaInscripcion = fecha;
            this.inscripto = inscripto;
            this.actividad = actividad;
            montoAbonado = 0;
        }

        // Acumula los pagos (permite pagar en cuotas)
        public void RegistrarPago(double monto)
        {
            if (monto <= 0)
                throw new ArgumentException("El monto debe ser mayor a cero.");

            montoAbonado += monto;
        }

        public double ObtenerMontoAbonado()
        {
            return montoAbonado;
        }

        public DateTime FechaInscripcion
        {
            get { return fechaInscripcion; }
        }

        public Actividad Actividad
        {
            get { return actividad; }
        }

        public Inscripto Inscripto
        {
            get { return inscripto; }
        }
    }
}
