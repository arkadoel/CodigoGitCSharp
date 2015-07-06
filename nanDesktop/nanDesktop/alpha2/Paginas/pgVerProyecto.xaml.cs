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

namespace nanDesktop.alpha2.Paginas
{
    /// <summary>
    /// Interaction logic for pgVerProyecto.xaml
    /// </summary>
    public partial class pgVerProyecto : Page
    {
        private logic.logicaGIT Proyecto { get; set; }

        public pgVerProyecto(logic.logicaGIT _pro)
        {
            InitializeComponent();
            Proyecto = _pro;
            initEventHandlers();

        }

        private void initEventHandlers()
        {
            this.Loaded += new RoutedEventHandler(pgVerProyecto_Loaded);

            //cuando cursor entra en etiqueta
            this.lblGuardarVersion.MouseLeave += Efectos.Label_MouseLeave;
            this.lblLanzarConsola.MouseLeave += Efectos.Label_MouseLeave;
            this.lblVerCambiosPendientes.MouseLeave += Efectos.Label_MouseLeave;
            this.lblVerVersiones.MouseLeave += Efectos.Label_MouseLeave;
            this.lblAbrirExplorer.MouseLeave += Efectos.Label_MouseLeave;

            //cuando cursor sale de etiqueta
            this.lblGuardarVersion.MouseEnter += Efectos.Label_MouseEnter;
            this.lblLanzarConsola.MouseEnter += Efectos.Label_MouseEnter;
            this.lblVerCambiosPendientes.MouseEnter += Efectos.Label_MouseEnter;
            this.lblVerVersiones.MouseEnter += Efectos.Label_MouseEnter;
            this.lblAbrirExplorer.MouseEnter += Efectos.Label_MouseEnter;

            //click en etiqueta
            this.lblLanzarConsola.MouseLeftButtonDown += Label_Click;
            this.lblGuardarVersion.MouseLeftButtonDown += Label_Click;
            this.lblVerCambiosPendientes.MouseLeftButtonDown += Label_Click;
            this.lblVerVersiones.MouseLeftButtonDown += Label_Click;
            this.lblAbrirExplorer.MouseLeftButtonDown += Label_Click;
        }

        /// <summary>Controla los distintos clicks que se dan en las etiquetas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Label_Click(object sender, MouseButtonEventArgs e)
        {
            Label label = sender as Label;

            switch (label.Tag.ToString())
            {
                case "Consola":
                    System.Diagnostics.Process.Start("\"PortableGit\\GitBash.vbs\"", "\"" + Proyecto.Path + "\"");
                    break;
                case "New Commit":
                    break;
                case "Cambios pendientes":
                    break;
                case "Ver Commits":
                    break;
                case "Explorer":
                    System.Diagnostics.Process.Start("explorer.exe", Proyecto.Path);
                    break;
            }
        }

        void pgVerProyecto_Loaded(object sender, RoutedEventArgs e)
        {
            this.lblNombreProyecto.Text = Proyecto.NombreProyecto;
        }
    }
}
