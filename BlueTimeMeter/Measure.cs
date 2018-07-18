using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueTimeMeter
{
    public static class Measure
    {
        private static Stack<BTM_In> StackBtm = new Stack<BTM_In>();
        private static string FilePathLog { get; set; }
        private static bool NotDebug = false;

        /// <summary>
        /// Sirve para asignar la ruta de guardado de ficheros.
        /// Solo se puede asignar una vez, las siguientes llamadas no repercutiran cambios en la ruta
        /// </summary>
        /// <param name="filePath"></param>
        public static void SetPath(string filePath)
        {
            if(string.IsNullOrEmpty(FilePathLog))
                FilePathLog = filePath;
        }

        /// <summary>
        /// Si se llama a esta funcion, activamos el modo debug, y por lo tanto las funciones Start y Stop dejaran de grabar
        /// tan solo grabarian FStart y FStop
        /// </summary>
        public static void SetDebug()
        {
            NotDebug = true;
        }
        

        /// <summary>
        /// Sirve para poner el punto de partida para empezar a tomar tiempos, cuando encuentre en ejecucion la siguiente llamada a Stop
        /// calculara el tiempo transcurrido y grabara el fichero
        /// </summary>
        /// <param name="name">Sirve para poner un nombre a esta toma de tiempo, si le establecemos un valor, para parar, ya no valdra el siguiente llamada a Stop
        /// además esa llamada a Stop debera de llevar el mismo nombre</param>
        /// <param name="methodName">No es necesario pasarlo, lo cogera automaticamente</param>
        /// <param name="sourceFilePathCaller">No es necesario pasarlo, lo cogera automaticamente</param>
        /// <param name="sourceLineNumber">No es necesario pasarlo, lo cogera automaticamente</param>
        public static async Task Start(string name = "",
            [System.Runtime.CompilerServices.CallerMemberName]
            string methodName = "",
            [System.Runtime.CompilerServices.CallerFilePath]
            string sourceFilePathCaller = "",
            [System.Runtime.CompilerServices.CallerLineNumber]
            int sourceLineNumber = 0)
        {
            if (!NotDebug)
            {
                FStart(name, methodName, sourceFilePathCaller, sourceLineNumber);
            }
        }

        /// <summary>
        /// Ignora si estamos con Debug o no y sirve para poner el punto de partida para empezar a tomar tiempos, cuando encuentre en ejecucion la siguiente llamada a Stop
        /// calculara el tiempo transcurrido y grabara el fichero
        /// </summary>
        /// <param name="name">Sirve para poner un nombre a esta toma de tiempo, si le establecemos un valor, para parar, ya no valdra el siguiente llamada a Stop
        /// además esa llamada a Stop debera de llevar el mismo nombre</param>
        /// <param name="methodName">No es necesario pasarlo, lo cogera automaticamente</param>
        /// <param name="sourceFilePathCaller">No es necesario pasarlo, lo cogera automaticamente</param>
        /// <param name="sourceLineNumber">No es necesario pasarlo, lo cogera automaticamente</param>
        public static async Task FStart(string name = "",
            [System.Runtime.CompilerServices.CallerMemberName]
            string methodName = "",
            [System.Runtime.CompilerServices.CallerFilePath]
            string sourceFilePathCaller = "",
            [System.Runtime.CompilerServices.CallerLineNumber]
            int sourceLineNumber = 0)
        {
            var btm_in = new BTM_In(name, sourceFilePathCaller, methodName, sourceLineNumber);
            StackBtm.Push(btm_in);
        }


        /// <summary>
        /// Sirve para parar un Start anteriormente abierto, grabando automaticamente en el log el tiempo transcurrido
        /// </summary>
        /// <param name="name">Sirve para nombrar la toma de tiempo, si le establecemos valor, ha de ser el misom a un start establecido con anterioridad.</param>
        /// <param name="methodName">No es necesario pasarlo, lo cogera automaticamente</param>
        /// <param name="sourceFilePathCaller">No es necesario pasarlo, lo cogera automaticamente</param>
        /// <param name="sourceLineNumber">No es necesario pasarlo, lo cogera automaticamente</param>
        public static Task<double> Stop(string name = "",
            [System.Runtime.CompilerServices.CallerMemberName]
            string methodName = "",
            [System.Runtime.CompilerServices.CallerFilePath]
            string sourceFilePathCaller = "",
            [System.Runtime.CompilerServices.CallerLineNumber]
            int sourceLineNumber = 0)
        {
            if (!NotDebug)
            {
                return FStop(name, methodName, sourceFilePathCaller, sourceLineNumber);
            }
            return new Task<double>(() => 0);
        }


        /// <summary>
        /// Ignora si estamos con Debug o no y irve para parar un Start anteriormente abierto, grabando automaticamente en el log el tiempo transcurrido
        /// </summary>
        /// <param name="name">Sirve para nombrar la toma de tiempo, si le establecemos valor, ha de ser el misom a un start establecido con anterioridad.</param>
        /// <param name="methodName">No es necesario pasarlo, lo cogera automaticamente</param>
        /// <param name="sourceFilePathCaller">No es necesario pasarlo, lo cogera automaticamente</param>
        /// <param name="sourceLineNumber">No es necesario pasarlo, lo cogera automaticamente</param>
        public static async Task<double> FStop(string name = "",
            [System.Runtime.CompilerServices.CallerMemberName]
            string methodName = "",
            [System.Runtime.CompilerServices.CallerFilePath]
            string sourceFilePathCaller = "",
            [System.Runtime.CompilerServices.CallerLineNumber]
            int sourceLineNumber = 0)
        {
            if (StackBtm.Any())
            {
                return (new BTM_Complete(name, sourceFilePathCaller, methodName, sourceLineNumber, StackBtm.Pop(), FilePathLog)).Milis();
            }
            return 0;
        }
    }
}
