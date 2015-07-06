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
using LibGit2Sharp;

namespace nanDesktop.Controles
{
    /// <summary>
    /// Interaction logic for VerCambiosPendientes.xaml
    /// </summary>
    public partial class VerCambiosPendientes : UserControl
    {
        private logicaGIT Proyecto;
        private MainWindow vparent;

        public VerCambiosPendientes(logicaGIT _royecto, MainWindow _parent)
        {
            InitializeComponent();
            Proyecto = _royecto;
            vparent = _parent;

            this.Loaded += new RoutedEventHandler(VerCambiosPendientes_Loaded);
        }

        void VerCambiosPendientes_Loaded(object sender, RoutedEventArgs e)
        {
            Label lbl = null;
            RepositoryStatus status = Proyecto.getStatus();
            
            Constantes.DoEvents(vparent.Dispatcher);
            Constantes.DoEvents(vparent.pnlNavegacion.Dispatcher);
            logic.Constantes.DoEvents(this.Dispatcher);
            
            System.Diagnostics.Process.Start("\"nanDesktop.gitAdd.exe\"", "\"" + Proyecto.Path + "\"");
            
            foreach (var archivo in status)
            {
                lbl = new Label();
                lbl.Content = archivo.FilePath;
                lbl.MinWidth=350;
                lbl.MinHeight=25;                
                //(SolidColorBrush)(new BrushConverter().ConvertFrom("#ffaacc"));                
                 Constantes.DoEvents(vparent.Dispatcher);
            	Constantes.DoEvents(vparent.pnlNavegacion.Dispatcher);
            	logic.Constantes.DoEvents(this.Dispatcher);

                if (archivo.State == FileStatus.Added)
                {
                    lbl.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFAAEB81"));
                }
                else if (archivo.State == FileStatus.Modified)
                {
                    lbl.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFC5DB50"));
                }
                else if (archivo.State == FileStatus.Removed)
                { //#FFDB7878
                    lbl.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDB7878"));
                }
                else if (archivo.State == FileStatus.Ignored)
                {
                    lbl.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF3FAAB1"));
                }
                else if (archivo.State == FileStatus.Untracked)
                {
                    lbl.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFDB7878"));
                }
                    
                lbl.Content = "[" + archivo.State.ToString() + "] " + lbl.Content;


                if (archivo.State != FileStatus.Ignored && archivo.State != FileStatus.Missing && archivo.State != FileStatus.Untracked)
                {                	
                	//Proyecto.git_trackFile(archivo.FilePath);
                    stkArchivos.Children.Add(lbl);
                }
                else
                {
                    stkArchivosIgnorados.Children.Add(lbl);
                }
                
            }
            
           
        }
    }
}
