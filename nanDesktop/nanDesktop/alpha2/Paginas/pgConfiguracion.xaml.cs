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
using nanDesktop.logic;
using Feed = FeedBackManager;

namespace nanDesktop.alpha2.Paginas
{
    /// <summary>
    /// Interaction logic for pgConfiguracion.xaml
    /// </summary>
    public partial class pgConfiguracion : Page
    {
        private alpha2.MainWindow vParent;

        public pgConfiguracion(alpha2.MainWindow _win)
        {
            InitializeComponent();
            vParent = _win;
            this.Loaded += new RoutedEventHandler(pgConfiguracion_Loaded);
        }

        void pgConfiguracion_Loaded(object sender, RoutedEventArgs e)
        {
            if (logicaUsuario.ExisteCarpetaConfiguraciones() == true)
            {
                txtNombre.Text = nanDesktop.logic.Constantes.GIT_USER;
                txtEmail.Text = nanDesktop.logic.Constantes.GIT_EMAIL;
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            logicaUsuario.GuardarCrearDirectorioConfiguracion(txtNombre.Text, txtEmail.Text);
            MessageBox.Show("Datos guardados con exito.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            Feed.Logs.WriteText("Guardar nueva configuracion", "Pulso para guardar nueva configuracion de usuario");
        }
    }
}
