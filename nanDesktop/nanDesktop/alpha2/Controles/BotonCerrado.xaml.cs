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
    /// Interaction logic for BotonCerrado.xaml
    /// </summary>
    public partial class BotonCerrado : UserControl
    {
        public BotonCerrado()
        {
            InitializeComponent();
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            colorFondo.Opacity = 1;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            colorFondo.Opacity = 0.5;
        }
    }
}
