using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Xml;
using System.IO;

namespace nanDesktop.agent
{
    public class comun
    {
        public const string APP_NAME = "NaN Desktop Agent";
        public const string APP_VERSION = "Alpha 1";

        public static VentanaPrincipal VentanaPrincipal { get; set; }
        public static BandejaSistema IconoSistema { get; set; }
        public static FontFamily InconsolataFont { get; set; }
        public static String DirectorioActual { get; set; }
        public static Thread hiloReloj { get; set; }

        public static List<Tarea> Tareas { get; set; }



        public static void cargarParametrosIniciales()
        {
            //cargar directorio actual
            DirectorioActual = System.Windows.Forms.Application.StartupPath.ToString();

            //cargar fuente Inconsolata
            /*
            * List<FontFamily> fuentes = Fonts.GetFontFamilies("file:///"+ DirectorioActual +"/Fuentes/").ToList();
            * InconsolataFont = fuentes.First();
            */

            //cargar la lista de tareas a realizar
            cargarXMLtareas();


        }

        /// <summary>
        /// Carga las tareas
        /// </summary>
        public static void cargarXMLtareas()
        {
            Console.WriteLine("cargando listado tareas");
            Tarea t = null;

            comun.Tareas = new List<Tarea>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(Path.Combine(DirectorioActual, "tareas.xml"));

            XmlNodeList nodoTareas = xDoc.GetElementsByTagName("tareas");

            foreach (XmlElement elemento in nodoTareas[0].ChildNodes)
            {
                t = new Tarea();
                t.Nombre = elemento.Attributes["nombre"].Value.ToString();
                t.Hora = elemento.Attributes["hora"].Value.ToString();
                t.lunes = Tarea.stringToBool(elemento.Attributes["lunes"].Value.ToString());
                t.martes = Tarea.stringToBool(elemento.Attributes["martes"].Value.ToString());
                t.miercoles = Tarea.stringToBool(elemento.Attributes["miercoles"].Value.ToString());
                t.jueves = Tarea.stringToBool(elemento.Attributes["jueves"].Value.ToString());
                t.viernes = Tarea.stringToBool(elemento.Attributes["viernes"].Value.ToString());
                t.sabado = Tarea.stringToBool(elemento.Attributes["sabado"].Value.ToString());
                t.domingo = Tarea.stringToBool(elemento.Attributes["domingo"].Value.ToString());

                comun.Tareas.Add(t);
            }
            xDoc = null;
            GC.Collect();
        }


        /// <summary>
        /// Coloca una ventana en la zona inferior derecha de la pantalla
        /// </summary>
        /// <param name="_win"></param>
        public static void ventanaInferiorDerechaPantalla(Window _win)
        {
            _win.Top = System.Windows.SystemParameters.WorkArea.Height - _win.Height;
            _win.Left = System.Windows.SystemParameters.WorkArea.Width - _win.Width;
        }

        public static void DoEvents(Dispatcher dis)
        {
            dis.Invoke(DispatcherPriority.Background, new Action(delegate
            {
            }));
        }
    }
}
