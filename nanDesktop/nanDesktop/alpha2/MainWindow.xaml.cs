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
using System.Windows.Shapes;
using nanDesktop.logic;
using System.Windows.Media.Effects;
using Feed = FeedBackManager;


namespace nanDesktop.alpha2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int TAM_PANEL_CONFIGURACION = 179;
        private const int TAM_PANEL_DAR_OPINION = 480;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = nanDesktop.logic.Constantes.APP_NAME + " " + nanDesktop.logic.Constantes.APP_VERSION;
            lblTitle.Text = this.Title.ToUpper();

            if (logicaUsuario.ExisteCarpetaConfiguraciones() == false)
            {
                NavegarHacia("Configuraciones");
            }
            else NavegarHacia("Principal");

            initEventHandlers();
        }

        private void initEventHandlers()
        {
            this.lblConfiguraciones.MouseEnter += Efectos.Label_MouseEnter;
            this.lblOpinionIdeas.MouseEnter += Efectos.Label_MouseEnter;
            this.lblPrincipal.MouseEnter += Efectos.Label_MouseEnter;
            this.lblPrincipal.MouseLeave += Efectos.Label_MouseLeave;
            this.lblOpinionIdeas.MouseLeave += Efectos.Label_MouseLeave;
            this.lblConfiguraciones.MouseLeave += Efectos.Label_MouseLeave;
        }

        private void clickCerrar(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void botonRestaurar1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
            else this.WindowState = System.Windows.WindowState.Maximized;
        }

        private void moverVentana(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MostrarOcultarMenu();
        }

        private void MostrarOcultarMenu()
        {
            //mostrar zona configuracion
            if (gridMenu.Visibility == System.Windows.Visibility.Hidden)
            {
                gridMenu.Visibility = System.Windows.Visibility.Visible;
                for (int i = 1; i < TAM_PANEL_CONFIGURACION; i += 3)
                {
                    gridMenu.Height = i;
                    logic.Constantes.DoEvents(this.Dispatcher);
                    //System.Threading.Thread.Sleep(1);						
                }

            }
            else
            {
                for (int i = TAM_PANEL_CONFIGURACION; i >= 1; i -= 2)
                {
                    gridMenu.Height = i;
                    logic.Constantes.DoEvents(this.Dispatcher);
                }
                gridMenu.Visibility = System.Windows.Visibility.Hidden;

            }
        }

       

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Label label = sender as Label;
            NavegarHacia(label.Name.ToString().Replace("lbl",""));
            MostrarOcultarMenu();
        }

        public void NavegarHacia(string clave)
        {
            switch (clave)
            {
                case "OpinionIdeas":
                    Feed.Logs.WriteText("Ir Dar Opinion", "Fue a dar opinion");
                    navegador.Navigate(new alpha2.Paginas.pgDarOpinion(this));
                    break;
                case "Principal":
                    Feed.Logs.WriteText("Ir Principal", "Fue a la pantalla principal");
                    navegador.Navigate(new alpha2.Paginas.pgPrincipal(this));
                    break;
                case "Configuraciones":
                    Feed.Logs.WriteText("Ir Configuracion", "Fue a la pantalla configuracion");
                    navegador.Navigate(new alpha2.Paginas.pgConfiguracion(this));
                    break;
                
            }
        }
    }
}
