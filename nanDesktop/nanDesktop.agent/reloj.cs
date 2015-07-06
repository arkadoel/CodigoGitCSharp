using System;
using System.Threading;
using System.Linq;
using System.IO;
using nanDesktop.logic;
using System.Collections.Generic;

namespace nanDesktop.agent
{
	/// <summary>
	/// Description of reloj.
	/// </summary>
	public class reloj 
	{
		public static int intervalo = 40; //40 segundos
        public const int PASADAS = 20;
				
		public static void ejecutarReloj(){
			int pasadas = PASADAS;
			
			while(true){
				Console.Write(".");
				//comprobar si es hora de copia
                string minuto = "";
                string hora = "";

                if (DateTime.Now.Minute < 10)
                {
                    minuto = "0" + DateTime.Now.Minute.ToString();
                }
                else minuto = DateTime.Now.Minute.ToString();
                if (DateTime.Now.Hour < 10)
                {
                    hora = "0" + DateTime.Now.Hour.ToString();
                }
                else hora = DateTime.Now.Hour.ToString();

				string horaActual = hora + ":" + minuto;
				string diaSemana = DateTime.Today.DayOfWeek.ToString();
				bool tocaEjecutar =false;
				
				var listaTareas = from u in comun.Tareas
					where u.Hora == horaActual
					select u;
				
				if(listaTareas.Count()>0){
					//hay tareas a ejecutar					
					foreach(Tarea ta in listaTareas)
					{
						//revisar si toca ejecutar hoy
						tocaEjecutar =false;
						
						switch(diaSemana){
							case "Monday":
								if(ta.lunes == true) tocaEjecutar = true;
								break;
							case "Tuesday":
								if(ta.martes == true) tocaEjecutar = true;
								break;
							case "Wednesday":
								if(ta.miercoles == true) tocaEjecutar = true;
								break;
							case "Thursday":
								if(ta.jueves == true) tocaEjecutar = true;
								break;
							case "Friday":
								if(ta.viernes == true) tocaEjecutar = true;
								break;	
							case "Saturday":
								if(ta.sabado == true) tocaEjecutar = true;
								break;
							case "Sunday":
								if(ta.domingo == true) tocaEjecutar = true;
								break;
						}
						
						if(tocaEjecutar)
						{
							FeedBackManager.Logs.WriteText("Ejecutando tarea programada", "Ejecutando" + ta.Nombre.ToString() + " hora " + horaActual + "");
							//ACCIONES A EJECUTAR
                            logicaUsuario.getActiveUser();

                            StreamReader fich = new StreamReader(logic.Constantes.CONFIG_DIR + @"\user.config");
                            logic.Constantes.GIT_USER = fich.ReadLine();
                            logic.Constantes.GIT_EMAIL = fich.ReadLine();
                            fich.Close();
                            List<string> proyectos = logicaUsuario.listarProyectosUsuario();

                            foreach (string projectPath in proyectos)
                            {
                                logicaGIT proyecto = new logicaGIT(projectPath);
                                comun.IconoSistema.verMensaje("Guardado automatico", "Realizando guardado de proyecto: " + proyecto.NombreProyecto);
                                FeedBackManager.Logs.WriteText("Tarea automatica", "Realizando guardado de proyecto: " + proyecto.NombreProyecto);
                                if (proyecto.esRepositorioIniciado())
                                {
                                    proyecto.git_stage_all();
                                    Thread.Sleep(1000);
                                    proyecto.git_autoCommit();
                                    FeedBackManager.Logs.WriteText("Tarea automatica", "Fin guardado");
                                }
                                else
                                {
                                    FeedBackManager.Logs.WriteText("Tarea automatica","Sin control de versiones iniciado. ");
                                }
                            }

						}
					}
				}
					
			    //Recargar las tareas por si se ha modificado el archivo
                if (pasadas >= PASADAS)
                {
                    pasadas = 0;
                    comun.cargarXMLtareas();
                }
                else pasadas++;
				
				Thread.Sleep(reloj.intervalo * 1000);
			}
		}
	}
}
