using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nanDesktop.Controles
{
    /// <summary>
    /// Interaction logic for DatosProyecto.xaml
    /// </summary>
    public partial class DatosProyecto : UserControl
    {
        private logic.logicaGIT Proyecto;
        private StackPanel vparent;
        private MainWindow padre;

        public DatosProyecto(logic.logicaGIT _proyecto, MainWindow _mainWindow)
        {
            InitializeComponent();
            Proyecto = _proyecto;
            vparent = _mainWindow.pnlNavegacion;
            padre = _mainWindow;

            lblNombre.Content = Proyecto.NombreProyecto;
            if(Proyecto.Repositorio == null){
            	gridFaltaGIT.Visibility = Visibility.Visible;            	
            }
            else gridFaltaGIT.Visibility = Visibility.Hidden;
        }

        private void lblVerCommits_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VerUltimosCommits v = new VerUltimosCommits(Proyecto, this.vparent);
           
            v.Height = 450;
            this.vparent.Children.Clear();
            this.vparent.Children.Add(v);
        }

        private void lblVerCambiosPendientes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VerCambiosPendientes v = new VerCambiosPendientes(Proyecto, padre);
            logic.Constantes.DoEvents(this.Dispatcher);
            v.Height = 450;
            this.vparent.Children.Clear();
            logic.Constantes.DoEvents(padre.pnlNavegacion.Dispatcher);
            this.vparent.Children.Add(v);
        }

        private void lblLanzarConsola_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("\"PortableGit\\GitBash.vbs\"", "\"" + Proyecto.Path + "\"");
        }
		
		void button1_Click(object sender, RoutedEventArgs e)
		{
			Proyecto.initRepoForThisProject();
			gridFaltaGIT.Visibility = Visibility.Hidden;
		}
		
		void btnQuitarProyecto_Click(object sender, RoutedEventArgs e)
		{
			string filePath =logic.Constantes.USER_DIRECTORY_LIST + @"\" + Proyecto.NombreProyecto;
			if(System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
				padre.pnlNavegacion.Children.Clear();
				padre.cargarListaDirectorios();
			}
		}
		
		void LblHacerCommit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			VerHacerCommit v = new VerHacerCommit(Proyecto);
            logic.Constantes.DoEvents(this.Dispatcher);
            v.Height = 450;
            this.vparent.Children.Clear();
            this.vparent.Children.Add(v);
		}
    }
}
