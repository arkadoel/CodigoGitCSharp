using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace nanDesktop.agent
{
    public class BandejaSistema 
    {
        NotifyIcon iconoSistema = null;

        public BandejaSistema()
        {
            iconoSistema = new NotifyIcon();           
            iconoSistema.Text = comun.APP_NAME + " " + comun.APP_VERSION;
            iconoSistema.Icon = new Icon(@".\Images\logo2.ico");
            iconoSistema.Visible = true;
            iconoSistema.MouseClick += new MouseEventHandler(iconoSistema_MouseClick);
            
        }

        public void verMensaje(string titulo, string mensaje)
        {
            iconoSistema.ShowBalloonTip(10000, titulo, mensaje, ToolTipIcon.Info); 
        }

        void iconoSistema_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
              /*  if (Comun.VentanaPrincipal.Visibility == System.Windows.Visibility.Visible)
                {
                    Comun.VentanaPrincipal.Visibility = System.Windows.Visibility.Hidden;
                }
                else Comun.VentanaPrincipal.Visibility = System.Windows.Visibility.Visible;
                */
               verVentanaPrincipal();
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                	verVentanaPrincipal();
                }
            }
        }
        
        private void verVentanaPrincipal(){
        	if(comun.VentanaPrincipal.IsVisible==true){
                comun.ventanaInferiorDerechaPantalla(comun.VentanaPrincipal);
               	comun.VentanaPrincipal.Hide();
               	GC.Collect();
               }
               else comun.VentanaPrincipal.Show();
             
        }

       
    }
}
