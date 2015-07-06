/*
 * 
 * Usuario: Fer.d.minguela@gmail.com
 * Fecha: 23/07/2013
 * Hora: 18:14
 * 
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.IO;

namespace nanDesktop.agent
{
	/// <summary>
	/// Interaction logic for VentanaPrincipal.xaml
	/// </summary>
	public partial class VentanaPrincipal : Window
	{
		public VentanaPrincipal()
		{
			InitializeComponent();
            this.Loaded += new RoutedEventHandler(VentanaPrincipal_Loaded);
		}

        void VentanaPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
            comun.ventanaInferiorDerechaPantalla(this);
            this.Hide();
        }

        private void fondo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

        }

        private void label2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void lblVerConfiguracion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string programa = Path.Combine(comun.DirectorioActual, @"notepad\notepadpp.exe");
            string fichero = Path.Combine(comun.DirectorioActual, "tareas.xml");
            Process.Start(programa, fichero);
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void lblFinalizarAgente_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            App.CerrarPrograma();
        }

        private void lblLanzarNaNDesktop_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string programa = Path.Combine(comun.DirectorioActual, "nanDesktop.exe");
            Process.Start(programa);
            this.Visibility = System.Windows.Visibility.Hidden;
        }
		
	}
}