using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneracionEstadisticasDynatrace
{
    public class sftp
    {
        public static void UploadSFTPFile(string host, string username, string password, string sourcefile, string destinationpath, int port)
        {
            using (SftpClient client = new SftpClient(host, port, username, password))
            {
                client.Connect();
                Registro.Mensaje(string.Format("Conectando al SFTP {0}@{1}:{2}.", username, host, port));
                client.ChangeDirectory(destinationpath);
                Registro.Mensaje(string.Format("Conectando al directorio: {0}.", destinationpath));
                using (FileStream fs = new FileStream(sourcefile, FileMode.Open))
                {
                    Registro.Mensaje(string.Format("Cargando fichero {0} en {1}.", sourcefile, destinationpath));
                    client.BufferSize = 4 * 1024;
                    client.UploadFile(fs, Path.GetFileName(sourcefile));
                    Registro.Mensaje(string.Format("Fichero cargado correctamente en {0}.", destinationpath));
                }

                client.Disconnect();
                Registro.Mensaje(string.Format("Cerrando conexión al SFTP {0}@{1}:{2}.", username, host, port));
            }
        }
    }
}
