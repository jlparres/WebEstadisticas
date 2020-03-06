using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneracionEstadisticasDynatrace
{
    public static class Registro
    {
        public static void Mensaje(string texto)
        {
            texto = string.Format("{0} {1}", DateTime.Now.ToString(), texto);
            string path = @"C:\\Dynatrace\\Logs\\";
            string fichero = DateTime.Today.ToString("yyyyMM") + "_log.txt";
            string fullPath = path + fichero;

            Console.WriteLine(texto);

            if(!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            if (!System.IO.File.Exists(fullPath))
            {
                using (System.IO.FileStream f = System.IO.File.Create(fullPath))
                {
                    f.Close();
                }
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath, true))
            {
                file.WriteLine(texto);
                file.Close();
            }
        }
    }
}
