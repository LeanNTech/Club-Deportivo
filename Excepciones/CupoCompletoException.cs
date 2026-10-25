/*
 * Created by SharpDevelop.
 * User: Brian
 * Date: 25/10/2026
 * Time: 14:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CupoCompletoException
{
	/// <summary>
	/// Description of CupoCompletoException.
	/// </summary>
	public class CupoCompletoException:Exception
	{
		
		
		
		public CupoCompletoException() :base("La actividad no tiene cupos disponibles.!")
		{
			
		}
		
		public CupoCompletoException(string mensaje)
			: base (mensaje)
		{
			
		}
		
	}
}
