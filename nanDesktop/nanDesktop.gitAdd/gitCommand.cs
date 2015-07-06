
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

using LibGit2Sharp;

namespace nanDesktop.gitAdd
{
	/// <summary>
	/// Description of gitCommand.
	/// </summary>
	public class gitCommand
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
        }
		

        #endregion
        public string Path {get; set; }

        public gitCommand(string path)
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
		
        public RepositoryStatus getStatus()
        {
            return repo.Index.RetrieveStatus();
        }
        
        public void git_commit(string name, string email, string filePath, string message)
        {        	
        	//repo.Index.Remove(estado.FilePath,false);   
           	Signature signature = new Signature(name, email, DateTimeOffset.Now);
           	Commit newC = repo.Commit(message, signature,signature,true);

        }
        
        public void git_trackFile(string filePath)
        {
        	try{
           repo.Index.Stage(filePath);  
        	}catch{}
        	
        	//System.Threading.Thread.Sleep(1000);
        }
        
        public static void DoEvents(Dispatcher dis){
        	dis.Invoke(DispatcherPriority.Background, new Action(delegate{
        	    }));
        }
	}
}
