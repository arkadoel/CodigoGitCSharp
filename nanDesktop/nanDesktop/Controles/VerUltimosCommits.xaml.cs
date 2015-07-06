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
    /// Interaction logic for VerUltimosCommits.xaml
    /// </summary>
    public partial class VerUltimosCommits : UserControl
    {
        private logicaGIT Proyecto;
        private StackPanel vparent;

        public VerUltimosCommits(logicaGIT _pro, StackPanel _vparent)
        {
            InitializeComponent();
            Proyecto = _pro;
            vparent = _vparent;
            this.Height = vparent.Height;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            List<logicaGIT.CommitShortInfo> lista = Proyecto.listarCommits(100);
            ElementoCommit ele = null;
            gridCommits.Height = vparent.Height;

            foreach (logicaGIT.CommitShortInfo c in lista)
            {
                ele = new ElementoCommit(c, vparent);
                Constantes.DoEvents(this.Dispatcher);
                gridCommits.Children.Add(ele);
            }
        }
    }
}
