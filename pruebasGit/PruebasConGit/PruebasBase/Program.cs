using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebasBase
{
    class Program
    {
        static Commit comit1, comit2;

        static void Main(string[] args)
        {
            string ruta = @"C:\Users\developer\Documents\GitHub\libgit2sharp"; 
            var repo = new Repository(ruta);

            //Listar(repo);
            int numCommits = 0;
            int programadores = 8;
            int años = 2014 - 2010; //desde 2010
            Console.WriteLine("Numero de commits por branch:");

            foreach (var rama in repo.Branches)
            {                
                    Console.WriteLine("Rama: " + rama.Name + " tiene " + rama.Commits.Count() + " commits");
                    numCommits += rama.Commits.Count();
                
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\r\nRamas listadas " + repo.Branches.Count());
            Console.WriteLine("Total commits: " + numCommits);
            float lineas = ((numCommits/programadores)/(años*365));

            Console.WriteLine("Lineas media por dia: " + lineas);
            //ObtenerComits(repo);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\r\nFin");
            Console.Read();
        }

        private static void ObtenerComits(Repository repo)
        {
            /*Obtener dos commits, el comit1 sera el ultimo comit realizado
             y el comit2 sera el elemento padre (el anterior) del comit actual
             La cola esta de forma inversa, es decir, cuando accedemos a First() 
             estamos accediendo al ultimo commit guardado
             */
            try
            {
                comit1 = repo.Lookup<Commit>(repo.Commits.First().Id.Sha);
                comit2 = comit1.Parents.First();
                Console.WriteLine("\r\nCommit: \r\n\tpadre " + comit1.Id + " \r\n\thijo " + comit2.Id + "; ");

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static void Listar(Repository repo)
        {
            /*Listar los commits guardados en el control de versiones*/
            Console.WriteLine("Listar commits (versiones guardadas)");

            foreach (Commit comit in repo.Commits)
            {
                Console.WriteLine("\t" + comit.MessageShort.ToString() + " " + comit.Id.ToString());
            }
        }
    }
}
