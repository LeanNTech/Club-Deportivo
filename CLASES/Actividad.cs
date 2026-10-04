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
		private string nombre { get; set; }
		private int edadMinima { get; set; }
		private int edadMaxima { get; set; }
		private int cupoMaximo { get; set; }
		private string dia { get; set; }
		private TimeSpan horarioInicio { get; set; }
		private TimeSpan horarioFin { get; set; }
		private double arancelMensual { get; set; }
		private double descuentoSocios = 0.15;
		private Profesor Profesor { get; set; }
		public List<Inscripcion> Inscripciones { get; set; }
		
		
		public Actividad(string nom, int edadM, int edadMax, int cupoMax, string di, TimeSpan horaIni, TimeSpan horaFini, double arancel, Profesor profesor)
		{
			nombre = nom;
			edadMinima = edadM;
			cupoMaximo = edadMax;
			dia = di;
			horarioInicio = horaIni;
			horarioFin = horaFini;
			arancelMensual = arancel;
			Profesor = profesor;
			Inscripciones = new List<Inscripcion>();
			
		}
		
		
		public bool TieneCupoDisponible()
		{
			
			return Inscripciones.Count < cupoMaximo;
		}
		
		public bool PermiteEdad(int edad)
		{
			
			return edad>= edadMinima && edad <= edadMaxima;
			
		}
		
		
		public bool SeSuperponeCon(Actividad otra)
		{
			if (!string.Equals(dia, otra.dia, StringComparison.OrdinalIgnoreCase))
			{
				
				return false;
			}
			return horarioInicio < otra.horarioFin && otra.horarioInicio < horarioFin;
		}
		
		
		public int CantidadDeInscriptos()
		{
			
			return Inscripciones.Count;
			
		}
		
		public void ActualizarDatos (string nom, int? edadMini, int? edadMax, int? cupoMax, string di, TimeSpan? horaIni, TimeSpan? horaFini, double? arancel)
		{
			
			
			if (!string.IsNullOrEmpty(nom))
			{
				nombre = nom;
				
			}
			if(edadMini.HasValue)
			{
				edadMinima = edadMini.Value;
				
			}
			if(edadMax.HasValue)
			{
				edadMaxima = edadMax.Value;
				
			}
			if(!string.IsNullOrEmpty(dia))
			{
				dia = di;
				
			}
			if(horaIni.HasValue)
			{
				horarioInicio = horaIni.Value;
				
			}
			if(horaFini.HasValue)
			{
				horarioFin = horaFini.Value;
				
			}
			if(arancel.HasValue)
			{
				arancelMensual = arancel.Value;
				
			}
			
			
		}
		
	}
}
