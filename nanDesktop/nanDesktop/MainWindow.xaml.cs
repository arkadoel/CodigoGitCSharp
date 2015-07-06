using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace nanDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
    	private const int TAM_PANEL_CONFIGURACION = 300;
    	private const int TAM_PANEL_DAR_OPINION = 480;
    	
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (System.IO.Directory.Exists(logic.Constantes.CONFIG_DIR) == false)
            {
                //mostrar zona configuracion
                gridConfiguracion.Height = TAM_PANEL_CONFIGURACION;
                gridConfiguracion.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                iniciarParametrosGIT();
            }
        }

        private void fondo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void cerrarVentana(object sender, MouseButtonEventArgs e)
        {
            this.Close();

        }

        private void RestairarVentana(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else this.WindowState = System.Windows.WindowState.Normal;
        }

        private void Minimizar(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        /// <summary>
        /// Guarda y genera la carpeta personal con los datos del usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
        	if(Directory.Exists(logic.Constantes.CONFIG_DIR) == false)
        	{
        		System.IO.Directory.CreateDirectory(logic.Constantes.CONFIG_DIR);
        		System.Threading.Thread.Sleep(1000); //espera un segundo
        	}
            DirectoryInfo dirInfo = new DirectoryInfo(logic.Constantes.CONFIG_DIR);
            dirInfo.Attributes = FileAttributes.Hidden;
            logic.Constantes.GIT_USER = txtNombre.Text;
            logic.Constantes.GIT_EMAIL = txtEmail.Text;

            //sobreescribir o crear archivo de configuracion
            StreamWriter fich = new StreamWriter(logic.Constantes.CONFIG_DIR + @"\user.config", false);
            fich.WriteLine(logic.Constantes.GIT_USER);
            fich.WriteLine(logic.Constantes.GIT_EMAIL);
            fich.Close();

            //generamos el directorio para repositorios locales
            if(Directory.Exists(logic.Constantes.LOCAL_REPO_DIR)== false)
            {
            	Directory.CreateDirectory(logic.Constantes.LOCAL_REPO_DIR);
            }

            //generar directorio con la lista de archivos de configuracion sobre los directorios personales
            if(Directory.Exists(logic.Constantes.USER_DIRECTORY_LIST)==false)
            {
            	Directory.CreateDirectory(logic.Constantes.USER_DIRECTORY_LIST);
            }

            iniciarParametrosGIT();
            gridConfiguracion.Visibility = System.Windows.Visibility.Hidden;
        }

        public void iniciarParametrosGIT()
        {
            StreamReader fich = new StreamReader(logic.Constantes.CONFIG_DIR + @"\user.config");
            logic.Constantes.GIT_USER = fich.ReadLine();
            logic.Constantes.GIT_EMAIL = fich.ReadLine();
            fich.Close();
            //asignar constantes como config.name y config.email de git
            //mediante consola de comandos

            lblNombreProgramador.Text = logic.Constantes.GIT_USER.ToUpper();
            
            //leer lista de directorios
            
            cargarListaDirectorios();
                        
        }
        
        /// <summary>
        /// Carga la lista lateral de proyectos
        /// </summary>
        public void cargarListaDirectorios()
        {
        	List<string> carpetas = logic.logicaUsuario.listarProyectosUsuario();
            stackDirectorios.Children.Clear();
           
            foreach(string carpeta in carpetas)
            {
	            Controles.DirectorioItem directorio = new Controles.DirectorioItem(carpeta);
	
	            stackDirectorios.Children.Add(directorio);
	            directorio.MouseLeftButtonDown += new MouseButtonEventHandler(ProyectoSeleccionado);
                directorio.MouseDoubleClick += new MouseButtonEventHandler(directorio_MouseDoubleClick);
            }
        }

        void directorio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Controles.DirectorioItem dir = sender as Controles.DirectorioItem;
            System.Diagnostics.Process.Start("explorer.exe", dir.Proyecto.Path);
        }

        void ProyectoSeleccionado(object sender, MouseButtonEventArgs e)
        {
            Controles.DirectorioItem dir = sender as Controles.DirectorioItem;
            pnlNavegacion.Children.Clear();
            pnlNavegacion.Children.Add(new Controles.DatosProyecto( dir.Proyecto, this ));
        }
        
		void BtnConfiguraciones_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
				//mostrar zona configuracion
				if(gridConfiguracion.Visibility == System.Windows.Visibility.Hidden)
				{
					gridConfiguracion.Visibility = System.Windows.Visibility.Visible;
					for(int i=1; i<TAM_PANEL_CONFIGURACION; i+=5)
					{
						gridConfiguracion.Height = i;
						logic.Constantes.DoEvents(this.Dispatcher);
						//System.Threading.Thread.Sleep(1);						
					}
	                txtNombre.Text = logic.Constantes.GIT_USER;
	                txtEmail.Text = logic.Constantes.GIT_EMAIL;
	                DropShadowEffect sombra = new DropShadowEffect();
					sombra.Direction=-90;
					sombra.ShadowDepth = 15;
					sombra.BlurRadius = 20;
					gridConfiguracion.Effect = sombra;
	                
				}
				else{
					for(int i = TAM_PANEL_CONFIGURACION; i>=1; i-=2)
					{
						gridConfiguracion.Height = i;
						logic.Constantes.DoEvents(this.Dispatcher);
					}
					gridConfiguracion.Visibility = System.Windows.Visibility.Hidden;
					gridConfiguracion.Effect = null;
					
				}
		}
        
		/// <summary>
		/// Agrega un nuevo directorio a la lista de directorios lateral
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void addNewTrackDir(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.RootFolder = Environment.SpecialFolder.Desktop;
			dlg.ShowDialog();
			
			if(dlg.SelectedPath != null && dlg.SelectedPath !="")
			{
				DirectoryInfo dir = new DirectoryInfo(dlg.SelectedPath);
				string ruta = logic.Constantes.USER_DIRECTORY_LIST + @"\" + dir.Name;
				
				StreamWriter fich = new StreamWriter(ruta, false);
				fich.WriteLine(dlg.SelectedPath);
				fich.Close();
				
				cargarListaDirectorios();
			}
			
			
		}
		
		void LblDarOpinion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if(gridOpiniones.Height < TAM_PANEL_DAR_OPINION)
				{
					for(int i=25; i<TAM_PANEL_DAR_OPINION; i+=5)
					{
						gridOpiniones.Height = i;
						logic.Constantes.DoEvents(this.Dispatcher);											
					}
					gridOpiniones.Height = TAM_PANEL_DAR_OPINION;
					
					DropShadowEffect sombra = new DropShadowEffect();
					sombra.Direction=-90;
					sombra.ShadowDepth = 15;
					sombra.BlurRadius = 20;
					
					gridOpiniones.Effect = sombra;
					
	                
				}
				else{
					for(int i = TAM_PANEL_DAR_OPINION; i>=25; i-=2)
					{
						gridOpiniones.Height = i;
						logic.Constantes.DoEvents(this.Dispatcher);
					}
					gridOpiniones.Height = 25;
					gridOpiniones.Effect =null;
				}
		}
		
		void btnEnviarOpinion_Click(object sender, RoutedEventArgs e)
		{
			string mensaje = "Nombre: " + logic.Constantes.GIT_USER;
            mensaje += "\r\nEmail: " + logic.Constantes.GIT_EMAIL;
            mensaje += "\r\nFecha: " + DateTime.Today.ToShortDateString();
			mensaje += " " + DateTime.Now.ToShortTimeString();
            mensaje += "\r\n\r\n" + txtDarOpinion.Text;
			
            new System.Threading.Thread(delegate() {
                logic.logicaEmails.emailForMe(mensaje, "nanDesktop feedback alpha 1");
            }).Start();
			
		}
    }
}
