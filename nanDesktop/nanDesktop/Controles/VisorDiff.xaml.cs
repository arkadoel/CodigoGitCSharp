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
    /// Interaction logic for VisorDiff.xaml
    /// </summary>
    public partial class VisorDiff : UserControl
    {
        private logicaGIT logica = null;
        private TreeChanges cambios = null;

        public VisorDiff(logicaGIT.CommitShortInfo _commit)
        {
            InitializeComponent();
            logica = new logicaGIT(_commit.RepoPath);

            Commit cNuevo = logica.getCommitByID(_commit.ID);
            Commit cPadre = null;

            if (cNuevo.Parents.Count() > 0)
            {
                cPadre = cNuevo.Parents.First();
               
            }
            else
            {
                cPadre = cNuevo;
            }

            cambios = logica.VerCambios(cPadre.Id.Sha, cNuevo.Id.Sha);
            foreach (TreeEntryChanges cambio in cambios)
            {
                if (logic.logicaGIT.extensionesProhibidas(cambio.Path))
                {
                    cmbFicheros.Items.Add(cambio.Path);
                }
            }

        }

        public VisorDiff(logicaGIT.CommitShortInfo _cAncient, logicaGIT.CommitShortInfo _cNew)
        {
            InitializeComponent();

        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            var cam = from u in cambios
                      where u.Path == cmbFicheros.Text
                      select u;

            TreeEntryChanges cambio = cam.First();
            txt.SelectAll();
            txt.Selection.Text = cambio.Patch;
            

            detectarColoreado(cambio);
        }

        private void detectarColoreado(TreeEntryChanges cambio)
        {
            List<int> lineasVerdes = new List<int>();
            List<int> lineasRojas = new List<int>();

            string strCambios = cambio.Patch;
            int numlinea = 0;
            String linea = "";
            bool paraEscritura = false;


            for (int i = 0; i < strCambios.Length - 1; i++)
            {
                char c = strCambios[i];

                if (c.ToString() == "@" && strCambios[i + 1].ToString() == "@")
                {
                    paraEscritura = !paraEscritura;
                }

                if (((int)c) != 10)
                {
                    if (paraEscritura == false)
                    {
                        linea += c.ToString();
                    }
                }
                else
                {
                    if (linea[0].ToString() == "-")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        lineasRojas.Add(numlinea);
                    }
                    if (linea[0].ToString() == "+")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        lineasVerdes.Add(numlinea);
                    }

                    linea = linea.Replace("@@", "");
                    Console.WriteLine(linea);
                    linea = "";
                    numlinea++;
                    // Console.ForegroundColor = normal;
                }
            }
            //txt.SelectAll();
            numlinea++;

            txt.CaretPosition =  txt.CaretPosition.DocumentStart;


            for (int nlinea = 0; nlinea < numlinea; nlinea++) 
            {
                txt.LineDown();
                if (lineasVerdes.Contains(nlinea))
                {
                    colorearLinea(nlinea, Colors.Green);
                }
                else if (lineasRojas.Contains(nlinea))
                {
                    colorearLinea(nlinea, Colors.Red);
                }
                else colorearLinea(nlinea, Colors.Gray);
            }

            /*foreach (int n in lineasRojas)
            {
                colorearRojaLinea(n);
            }*/
        }


        public void colorearLinea(int LineNumber, Color color)
        {
            try
            {

                TextPointer start = txt.CaretPosition.GetLineStartPosition(LineNumber);
                TextPointer stop = txt.CaretPosition.GetLineStartPosition(LineNumber + 1);
                TextRange textrange = new TextRange(start, stop);
                textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(color));
            }catch{}
        }
       
    }
}
