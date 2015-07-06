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

namespace nanDesktop.alpha2.Controles
{
    /// <summary>
    /// Interaction logic for DirVistaIcono.xaml
    /// </summary>
    public partial class DirVistaIcono : UserControl
    {
        public logic.logicaGIT Proyecto { get; set; }

        public DirVistaIcono(string _repoPath)
        {
            InitializeComponent();
            Resaltado.Visibility = System.Windows.Visibility.Hidden;
            Proyecto = new logic.logicaGIT(_repoPath);
            lblNombre.Text = Proyecto.NombreProyecto;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Resaltado.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Resaltado.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
