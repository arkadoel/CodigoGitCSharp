/*
 * 
 * Usuario: Fer.d.minguela@gmail.com
 * Fecha: 09/26/2013
 * Hora: 11:34
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
using LibGit2Sharp;
using System.Linq;
using System.Diagnostics;

namespace nanDesktop.gitAdd
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}
		string[] args = null;
		
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			 args=Environment.GetCommandLineArgs();
			 gitCommand.DoEvents(this.Dispatcher);
			 if(args.Count()>0)			 	
			 {
				 this.Title="Agregando archivos al indice";
				 gitCommand g = new gitCommand(args[1].ToString());
				RepositoryStatus status =g.getStatus();
				
				var estados = from y in status
					where y.State == FileStatus.Untracked
					select y;
					
				int total = estados.Count();
				if(total ==0){
					this.Close();
				}
				else lblEstado.Content += total.ToString();
				
			 }
			
		}
		
		
		
		void button1_Click(object sender, RoutedEventArgs e)
		{
			 gitCommand g = new gitCommand(args[1].ToString());
				RepositoryStatus status =g.getStatus();
				
				var estados = from y in status
					where y.State == FileStatus.Untracked
					select y;
					
				int total = estados.Count();
				int i=0;
				
				foreach (var archivo in estados)
	            {
					lblEstado.Content = i.ToString() + " de " +  total.ToString();
					gitCommand.DoEvents(this.Dispatcher);
					if(archivo.State == FileStatus.Untracked){
						g.git_trackFile(archivo.FilePath);
					}
					i++;
				}
				this.Close();
		}
		
		void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}
	}
}