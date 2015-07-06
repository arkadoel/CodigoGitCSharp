/*
 * 
 * Usuario: Fer.d.minguela@gmail.com
 * Fecha: 09/30/2013
 * Hora: 18:34
 * 
 * 
 */
using System;

namespace nanDesktop.agent
{
	/// <summary>
	/// Description of Tarea.
	/// </summary>
	public class Tarea
	{
		public string Nombre {get; set; }
		public string Hora {get; set; }
		public bool lunes {get; set; }
		public bool martes {get; set; }
		public bool miercoles {get; set; }
		public bool jueves {get; set; }
		public bool viernes {get; set; }
		public bool sabado {get; set; }
		public bool domingo {get; set; }
		
		public Tarea()
		{
		}
		
		public static bool stringToBool(string valor){
			if(valor == "true") return true;
			else return false;
		}
	}
}
