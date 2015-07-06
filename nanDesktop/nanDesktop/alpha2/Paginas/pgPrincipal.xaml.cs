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
using System.Windows.Forms;
using System.IO;

namespace nanDesktop.alpha2.Paginas
{
    /// <summary>
    /// Interaction logic for pgPrincipal.xaml
    /// </summary>
    public partial class pgPrincipal : Page
    {
        public class VISTAS
        {
            public const string VISTA_CONTENIDO = "VISTA CONTENIDO";
            public const string VISTA_ICONOS = "VISTA ICONOS";
        }

        private string VISTA_ACTUAL = VISTAS.VISTA_ICONOS;

        private alpha2.MainWindow vparent = null;

        public pgPrincipal(alpha2.MainWindow _win)
        {
            InitializeComponent();
            vparent = _win;
            this.Loaded += new RoutedEventHandler(pgPrincipal_Loaded);
        }

        void pgPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            cargarListaDirectorios("");
            
        }

        /// <summary>
        /// Carga la lista de proyectos
        /// </summary>
        public void cargarListaDirectorios(string filtro)
        {
            List<string> carpetas = logic.logicaUsuario.listarProyectosUsuario(filtro);
            scrolllslsl.Content = null;

            if (VISTA_ACTUAL == VISTAS.VISTA_CONTENIDO)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = System.Windows.Controls.Orientation.Vertical;
                panel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                panel.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                panel.Children.Clear();

                foreach (string carpeta in carpetas)
                {
                    Controles.DirVistaContenido directorio = new Controles.DirVistaContenido(carpeta);
                    
                    panel.Children.Add(directorio);
                    directorio.MouseLeftButtonDown += new MouseButtonEventHandler(ProyectoSeleccionado);
                    directorio.MouseDoubleClick += new MouseButtonEventHandler(directorio_MouseDoubleClick);
                }
                scrolllslsl.Content = panel;
            }
            else if (VISTA_ACTUAL == VISTAS.VISTA_ICONOS)
            {
                WrapPanel panel = new WrapPanel();
                panel.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                panel.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                panel.Children.Clear();

                foreach (string carpeta in carpetas)
                {
                    Controles.DirVistaIcono directorio = new Controles.DirVistaIcono(carpeta);
                    
                    panel.Children.Add(directorio);
                    directorio.MouseLeftButtonDown += new MouseButtonEventHandler(ProyectoSeleccionado);
                    directorio.MouseDoubleClick += new MouseButtonEventHandler(directorio_MouseDoubleClick);
                }
                scrolllslsl.Content = panel;
            }
        }

        void directorio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            logic.logicaGIT Proyecto = null;

            switch (sender.GetType().Name.ToString())
            {
                case "DirVistaIcono":
                    Controles.DirVistaIcono dir = sender as Controles.DirVistaIcono;
                    Proyecto = dir.Proyecto;
                    break;
                case "DirVistaContenido":
                    Controles.DirVistaContenido dir2 = sender as Controles.DirVistaContenido;
                    Proyecto = dir2.Proyecto;
                    break;
            }
            System.Diagnostics.Process.Start("explorer.exe", Proyecto.Path);
        }

        void ProyectoSeleccionado(object sender, MouseButtonEventArgs e)
        {
            logic.logicaGIT Proyecto = null;

            switch (sender.GetType().Name.ToString())
            {
                case "DirVistaIcono":
                    Controles.DirVistaIcono dir = sender as Controles.DirVistaIcono;
                    Proyecto = dir.Proyecto;
                    
                    break;
                case "DirVistaContenido":
                    Controles.DirVistaContenido dir2 = sender as Controles.DirVistaContenido;
                    Proyecto = dir2.Proyecto;
                    break;
            }
            vparent.navegador.Navigate(new alpha2.Paginas.pgVerProyecto(Proyecto));
        }

        private void btnVistaContenido_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VISTA_ACTUAL = VISTAS.VISTA_CONTENIDO;
            cargarListaDirectorios(txtFiltro.Text);
        }

        private void btnVistaIconos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            VISTA_ACTUAL = VISTAS.VISTA_ICONOS;
            cargarListaDirectorios(txtFiltro.Text);
        }

        private void txtFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            cargarListaDirectorios(txtFiltro.Text);
        }

        /// <summary>
        /// Agrega un nuevo directorio a la lista de directorios lateral
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCarpeta_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.RootFolder = Environment.SpecialFolder.Desktop;
            dlg.ShowDialog();

            if (dlg.SelectedPath != null && dlg.SelectedPath != "")
            {
                DirectoryInfo dir = new DirectoryInfo(dlg.SelectedPath);
                string ruta = logic.Constantes.USER_DIRECTORY_LIST + @"\" + dir.Name;

                StreamWriter fich = new StreamWriter(ruta, false);
                fich.WriteLine(dlg.SelectedPath);
                fich.Close();

                cargarListaDirectorios("");
            }
        }
    }
}
