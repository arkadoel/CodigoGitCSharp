/*
 * 
 * Usuario: Fer.d.minguela@gmail.com
 * Fecha: 09/27/2013
 * Hora: 12:35
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
using System.Linq;
using LibGit2Sharp;
using nanDesktop.logic;

namespace nanDesktop.Controles
{
	/// <summary>
	/// Interaction logic for VerHacerCommit.xaml
	/// </summary>
	public partial class VerHacerCommit : UserControl
	{
		private logicaGIT Proyecto;
		
		public VerHacerCommit(logicaGIT _pro)
		{
			InitializeComponent();
			Proyecto = _pro;
			this.Loaded += new RoutedEventHandler(VerHacerCommit_Loaded);
		}

		void VerHacerCommit_Loaded(object sender, RoutedEventArgs e)
		{
			RepositoryStatus status = Proyecto.getStatus();
			if(status.Untracked.Count()>0){
				btnVerPendientesAgregar.Visibility = Visibility.Visible;
				lblTexto.Visibility = Visibility.Visible;
			}
			else{
				btnVerPendientesAgregar.Visibility = Visibility.Hidden;
				lblTexto.Visibility = Visibility.Hidden;
			}
		}
		
		void BtnVerPendientesAgregar_Click(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("\"nanDesktop.gitAdd.exe\"", "\"" + Proyecto.Path + "\"");
		}
		
		void BtnGuardarCommit_Click(object sender, RoutedEventArgs e)
		{
			Proyecto.git_commit(Constantes.GIT_USER, Constantes.GIT_EMAIL, txtMensaje.Text);
			MessageBox.Show("Guardado realizado con exito.", "Commit", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}