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
    /// Interaction logic for DirectorioItem.xaml
    /// </summary>
    public partial class DirectorioItem : UserControl
    {
        public logic.logicaGIT Proyecto { get; set; }

        public DirectorioItem(string _repoPath)
        {
            InitializeComponent();
            Proyecto = new logic.logicaGIT(_repoPath);
            lblNombre.Content = Proyecto.NombreProyecto;
        }


    }
}
