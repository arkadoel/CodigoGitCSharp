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
    /// Interaction logic for BotonRestaurar.xaml
    /// </summary>
    public partial class BotonRestaurar : UserControl
    {
        public BotonRestaurar()
        {
            InitializeComponent();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            colorFondo.Opacity = 0.1;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            colorFondo.Opacity = 1;
        }
    }
}
