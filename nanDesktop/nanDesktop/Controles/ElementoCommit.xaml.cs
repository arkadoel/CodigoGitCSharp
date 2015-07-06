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

namespace nanDesktop.Controles
{
    /// <summary>
    /// Interaction logic for ElementoCommit.xaml
    /// </summary>
    public partial class ElementoCommit : UserControl
    {
        StackPanel vparent;
        logicaGIT.CommitShortInfo commitActual;

        public ElementoCommit(logicaGIT.CommitShortInfo commit, StackPanel _vparent)
        {
            InitializeComponent();
            commitActual = commit;
            lblAutor.Content = commit.Autor;
            lblFecha.Content = commit.Fecha.ToShortDateString() + " " + commit.Fecha.ToShortTimeString();
            lblID.Text = commit.ID;
            txtMensaje.Text = commit.Mensaje;
            vparent = _vparent;
        }

        private void VerDiferencia(object sender, RoutedEventArgs e)
        {
            vparent.Children.Clear();
            VisorDiff visor = new VisorDiff(commitActual);
            
            vparent.Children.Add(visor);
            visor.Height = vparent.Height;
        }
    }
}
