using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using FeedBackManager;

namespace nanDesktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Logs.initLog();

            try
            {

                Logs.WriteText("Inicio", "Aplicacion iniciada sin problemas");
                Logs.WriteText("Carga usuario GIT", "Carga de usuario git para el inicio");

                logic.logicaUsuario.getActiveUser();

                new alpha2.MainWindow().Show();

            }
            catch (Exception ex)
            {
                Logs.WriteError("error al iniciar", ex);
            }

           
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            FeedBackManager.Logs.WriteText("Cierre", "Cerrado por usuario"); 
        }
    }
}
