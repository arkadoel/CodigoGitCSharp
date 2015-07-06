using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGit2Sharp;
using System.IO;
using System.Windows;
using System.Diagnostics;
using feed = FeedBackManager;

namespace nanDesktop.logic
{
    public class logicaGIT
    {
        #region "Propiedades y subclases"
        public string NombreProyecto { get; set; }

        private Repository repo = null;
        public Repository Repositorio
        {
            get
            {
                return repo;
            }
        }


        public class CommitShortInfo
        {
            public string ID { get; set; }
            public string Autor { get; set; }
            public DateTime Fecha { get; set; }
            public string RepoPath { get; set; }
            public string Mensaje { get; set; }
        }
		

        #endregion
        public string Path {get; set; }

        public logicaGIT(string path)
        {
        	if(Directory.Exists(path + @"\.git"))
        	{
            	repo = new Repository(Repository.Init(path, false));
        	}
            //hayar el nombre del proyecto
            DirectoryInfo dir = new DirectoryInfo(path);
            NombreProyecto = dir.Name.ToString();
            Path = path;
        }

        public Boolean esRepositorioIniciado()
        {
            if (Directory.Exists(Path + @"\.git"))
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TOP">Numero maximo a coger</param>
        /// <returns></returns>
        public List<CommitShortInfo> listarCommits(int TOP)
        {
            feed.Logs.WriteText("Obtener listado commits", "Se obtiene el listado de commits de " + NombreProyecto);
            var grupo = (from c in repo.Commits
                        select new {
                            autor = c.Author.Name,
                            id = c.Id.Sha,
                            fecha = c.Committer.When,
                            mensaje = c.Message
                        }).Take(TOP);

            List<CommitShortInfo> lista = new List<CommitShortInfo>();
            CommitShortInfo cinfo = null;
           

            foreach (var commit in grupo)
            {
                cinfo = new CommitShortInfo();
                cinfo.Autor = commit.autor;
                cinfo.Fecha = commit.fecha.DateTime;
                cinfo.ID = commit.id;
                cinfo.RepoPath = this.Path;
                cinfo.Mensaje = commit.mensaje;
                lista.Add(cinfo);
            }
            return lista;
        }

        public TreeChanges VerCambios(string commitID1, string commitID2)
        {
            feed.Logs.WriteText("Ver cambios", "Se desea ver los cambios del proyecto " + NombreProyecto);
            Tree t1 = Repositorio.Lookup<Commit>(commitID1).Tree;
            Tree t2 = Repositorio.Lookup<Commit>(commitID2).Tree;

            TreeChanges changes = Repositorio.Diff.Compare(t1, t2);
            return changes;
        }
        
        public Boolean initRepoForThisProject(){
            feed.Logs.WriteText("Iniciando repositorio", "Pulso para crear un nuevo repositorio");
            repo = new Repository(Repository.Init(Path,false));
        	return true;
        }

        public Commit getCommitByID(string id)
        {
            feed.Logs.WriteText("Get commit by ID", "Se obtiene un commit de " + NombreProyecto);
            return Repositorio.Lookup<Commit>(id);
        }

        public static Boolean extensionesProhibidas(string ruta)
        {
            System.IO.FileInfo fich = new FileInfo(ruta);
            string ext = fich.Extension;


            if (ext.Contains(".exe")) return false;
            else if (ext.Contains(".dll")) return false;
            else if (ext.Contains(".pdb")) return false;

            return true;
        }

        public RepositoryStatus getStatus()
        {
            feed.Logs.WriteText("Get status", "Se obtiene el status de " + NombreProyecto);
            if (esRepositorioIniciado())
            {
                repo = new Repository(Path);
                return repo.Index.RetrieveStatus();
            }
            else return null;
        }

        public void git_stage_all()
        {
            RepositoryStatus status = getStatus();
            if (status.Modified.Count() > 0 || status.Untracked.Count() > 0)
            {
                foreach (var archivo in status)
                {
                    try
                    {
                        repo.Index.Stage(archivo.FilePath);
                    }
                    catch (Exception ex){
                        feed.Logs.WriteError("Error en git_stage_all", ex);
                    } //ignorar errores y seguir

                 }
            }
        }

        /// <summary>
        /// Hace un commit con los datos del usuario actual
        /// </summary>
        /// <param name="message"></param>
        public void git_autoCommit()
        {
            feed.Logs.WriteText("AutoCommit", "Se hace un commit automatico");

            string fecha = "Guardado automatico día: " + 
                DateTime.Today.ToShortDateString() + " " +
                DateTime.Now.ToLongTimeString();
            git_commit(Constantes.GIT_USER, Constantes.GIT_EMAIL, fecha);
        }

        public void git_commit(string name, string email, string message)
        {
            feed.Logs.WriteText("Commit", "Se hace commit normal de " + this.NombreProyecto);
            //repo.Index.Remove(estado.FilePath,false); 
            Commit newC=null;
            RepositoryStatus status = getStatus();
            
                if (status.Modified.Count() > 0)
                {
                    foreach (var archivo in status.Modified)
                    {
                        repo.Index.Stage(archivo);
                    }
                }

                if (status.Modified.Count() > 0 || status.Staged.Count() > 0 ||
                    status.Removed.Count() > 0 || status.Added.Count() > 0 || status.Missing.Count() > 0)
                {

                    Signature signature = new Signature(name, email, DateTimeOffset.Now);
                    try
                    {
                        newC = repo.Commit(message, signature, signature, false);
                        // newC.Parents
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        newC = repo.Commit(message, signature, signature, true);
                    }
                }            

        }

        public void git_trackFile(string filePath)
        {

            try
            {
                repo.Index.Stage(filePath);

            }
            catch { }
        }
        
    }
}
