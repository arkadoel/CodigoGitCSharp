using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using feed = FeedBackManager;
using FeedBackManager;

namespace nanDesktop.logic
{
    public class logicaUsuario
    {
        /// <summary>
        /// Obtiene el usuario actual viendo los datos guardados 
        /// en la carpeta personal del usuario
        /// </summary>
        public static void getActiveUser()
        {
            Constantes.USER_PROFILE_DIR = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            Constantes.CONFIG_DIR = Constantes.USER_PROFILE_DIR + @"\.nanDesktop";
            Constantes.LOCAL_REPO_DIR = Constantes.CONFIG_DIR + @"\localREPO";
            Constantes.USER_DIRECTORY_LIST = Constantes.CONFIG_DIR + @"\dirList";
        }
        
        public static List<string> listarProyectosUsuario(string filtro)
        {
            
            List<string> lista = listarProyectosUsuario();
            for (int i = lista.Count() - 1; i >= 0; i--)
            {
                if (lista[i].Contains(filtro) == false)
                {
                    lista.RemoveAt(i);
                }
            }
            feed.Logs.WriteText("Listar directorios", "Lista directorios sin filtro, " + lista.Count.ToString() + " proyectos listados con filtro: " + filtro);
            return lista;
        }

        public static List<string> listarProyectosUsuario()
        {
            
            List<string> lista = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(Constantes.USER_DIRECTORY_LIST);
            FileInfo[] files = dir.GetFiles();
            string carpeta = "";

            foreach (FileInfo file in files)
            {
                StreamReader fich = new StreamReader(file.FullName);
                carpeta = fich.ReadLine();
                lista.Add(carpeta);
                fich.Close();
            }

            feed.Logs.WriteText("Listar directorios", "Lista directorios sin filtro, " + lista.Count.ToString() + " proyectos listados");
            return lista;
        }

        public static Boolean ExisteCarpetaConfiguraciones()
        {

            if (System.IO.Directory.Exists(logic.Constantes.CONFIG_DIR))
            {
                //como SI existen, cargar los parametros iniciales
                IniciarParametrosGIT();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void GuardarCrearDirectorioConfiguracion(string nombre, string email)
        {

            if (Directory.Exists(logic.Constantes.CONFIG_DIR) == false)
            {
                feed.Logs.WriteText("Creacion configuracion", "Se crea el directorio de configuracion");
                System.IO.Directory.CreateDirectory(logic.Constantes.CONFIG_DIR);
                System.Threading.Thread.Sleep(1000); //espera un segundo
            }
            DirectoryInfo dirInfo = new DirectoryInfo(logic.Constantes.CONFIG_DIR);
            dirInfo.Attributes = FileAttributes.Hidden;
            logic.Constantes.GIT_USER = nombre;
            logic.Constantes.GIT_EMAIL = email;

            //sobreescribir o crear archivo de configuracion
            StreamWriter fich = new StreamWriter(logic.Constantes.CONFIG_DIR + @"\user.config", false);
            fich.WriteLine(logic.Constantes.GIT_USER);
            fich.WriteLine(logic.Constantes.GIT_EMAIL);
            fich.Close();

            //generamos el directorio para repositorios locales
            if (Directory.Exists(logic.Constantes.LOCAL_REPO_DIR) == false)
            {
                Directory.CreateDirectory(logic.Constantes.LOCAL_REPO_DIR);
            }

            //generar directorio con la lista de archivos de configuracion sobre los directorios personales
            if (Directory.Exists(logic.Constantes.USER_DIRECTORY_LIST) == false)
            {
                Directory.CreateDirectory(logic.Constantes.USER_DIRECTORY_LIST);
            }

            IniciarParametrosGIT();
        }

        public static void IniciarParametrosGIT()
        {
            StreamReader fich = new StreamReader(logic.Constantes.CONFIG_DIR + @"\user.config");
            logic.Constantes.GIT_USER = fich.ReadLine();
            logic.Constantes.GIT_EMAIL = fich.ReadLine();
            
            fich.Close();
        }
    }
}
