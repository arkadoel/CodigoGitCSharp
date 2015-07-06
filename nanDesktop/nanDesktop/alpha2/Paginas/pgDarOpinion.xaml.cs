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
    /// Interaction logic for pgDarOpinion.xaml
    /// </summary>
    public partial class pgDarOpinion : Page
    {
        private alpha2.MainWindow vParent;

        public pgDarOpinion(alpha2.MainWindow _win)
        {
            InitializeComponent();
            vParent = _win;
        }

        private void btnEnviarOpinion_Click(object sender, RoutedEventArgs e)
        {
            string mensaje = "Nombre: " + logic.Constantes.GIT_USER;
            mensaje += "\r\nEmail: " + logic.Constantes.GIT_EMAIL;
            mensaje += "\r\nFecha: " + DateTime.Today.ToShortDateString();
            mensaje += " " + DateTime.Now.ToShortTimeString();
            mensaje += "\r\n\r\n" + txtDarOpinion.Text;

            new System.Threading.Thread(delegate()
            {
                logic.logicaEmails.emailForMe(mensaje, "nanDesktop feedback alpha 2");
            }).Start();

            FeedBackManager.Logs.WriteText("Enviando email", "Pulso para enviar un email dando opinion");

            vParent.NavegarHacia("Principal");
        }
    }
}
