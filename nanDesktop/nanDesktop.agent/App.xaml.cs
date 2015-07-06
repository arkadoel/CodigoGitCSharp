using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;
using System.Threading;
using FeedBackManager;

namespace nanDesktop.agent
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

        void Application_Startup(object sender, StartupEventArgs e)
        {
            comun.DoEvents(this.Dispatcher);

            Logs.initLog();
            Logs.WriteText("Inicio", "Aplicacion iniciada sin problemas");
            Logs.WriteText("Carga usuario GIT", "Carga de usuario git para el inicio");
            
            comun.cargarParametrosIniciales();
            comun.VentanaPrincipal = new VentanaPrincipal();
            comun.VentanaPrincipal.Show();
            comun.IconoSistema = new BandejaSistema();

            Thread hilo = new Thread(new ThreadStart(reloj.ejecutarReloj));
            comun.hiloReloj = hilo;
            comun.hiloReloj.Start();
        }

        public static void CerrarPrograma()
        {
            comun.hiloReloj.Abort();
            Application.Current.Shutdown();

        }
	}
}