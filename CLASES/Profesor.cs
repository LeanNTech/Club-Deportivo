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
		private DateTime HorarioAtencion { get; set; }
		private List<Actividad> ActividadesAcargo { get; set; }
		
		
		
		public Profesor(string nom, string ape, int matri, DateTime hoaten)
		{
			nombre = nom;
			apellido = ape;
			matricula = matri;
			HorarioAtencion = hoaten;
			ActividadesAcargo = new List<Actividad>();
		}
		
		public string Nombre 
		{
			
			get { return nombre;}
			
		}
		
		public string Apellido 
		{
			
			get { return apellido;}
			
		}
		
		
		
		public int Matricula 
		{
			
			get { return matricula;}
			
		}
		
		
		public void ActualizarDatos (DateTime horarioAtencion)
		{
			
			HorarioAtencion = horarioAtencion;
			
		}
		
		public int CantidadAlumnosAcargo()
		{
			int total = 0;
			
			foreach(Actividad a in ActividadesAcargo)
			{
				
				total += a.CantidadDeInscriptos();
				
			}
			return total;
			
		}
		
		public int CantidadActividadesACargo(){
			
			return ActividadesAcargo.Count;
			
		}
		
		
		public override string ToString()
		{
			return string.Format("[Profesor Nombre: {0}, Apellido: {1}, Matricula: {2}]", nombre, apellido, matricula);
		}
 
		
		
		
	}
}
