using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace nanDesktop.logic
{
    public class Constantes
    {
        //constantes de la parte visual
        public const string APP_NAME = "nanDesktop";
        public const string APP_VERSION = "alpha 2";

        //variables comunes a toda la aplicacion
        public static string USER_PROFILE_DIR {get; set; }
        public static string CONFIG_DIR { get; set; }
        public static string LOCAL_REPO_DIR { get; set; }
        public static string USER_DIRECTORY_LIST { get; set; }

        public static string GIT_USER { get; set; }
        public static string GIT_EMAIL { get; set; }

        public static void DoEvents(Dispatcher dis){
        	dis.Invoke(DispatcherPriority.Background, new Action(delegate{
        	    }));
        }

    }
}
