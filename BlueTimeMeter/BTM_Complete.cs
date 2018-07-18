using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlueTimeMeter
{
    class BTM_Complete
    {
        private const string DefaultFilePath = "../../../Tiempos.log";
        private BTM_In BtmIn { get; set; }
        private BTM_Out BtmOut { get; set; }

        public Double Milis()
        {
            return BtmOut.timeSpan.Subtract(BtmIn.timeSpan).TotalMilliseconds;
        }

        public BTM_Complete(string name, string filePath, string methodName, int codeLine, BTM_In btmIn, string filePathLog)
        {
            BtmOut = new BTM_Out(name, filePath, methodName, codeLine);
            BtmIn = btmIn;

            var formatedTitleText = $"{DateTime.Now} | Clase: {filePath?.Split('/')?.Last() ?? string.Empty}  | Metodo: {methodName ?? string.Empty} | Linea: {codeLine} ";
            var formatedBodyText = $"    Nombre: {name}  -> Tiempo Transcurrido : {BtmOut.timeSpan.Subtract(BtmIn.timeSpan).TotalMilliseconds}";
            var nombreFichero = filePathLog ?? DefaultFilePath;
            File.AppendAllLines(nombreFichero, new List<string>() { formatedTitleText, formatedBodyText, string.Empty });
        }
    }
}