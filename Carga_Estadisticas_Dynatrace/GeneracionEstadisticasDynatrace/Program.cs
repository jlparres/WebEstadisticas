using System;
using System.IO;

namespace GeneracionEstadisticasDynatrace
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string directorio = @"C:\Dynatrace\Ficheros";
                bool seguir = true;
                int operacion = -1;
                int anyo = 0;
                int mes = 0;
                int periodo = 0;
                int iteracion = 0;

                do
                {
                    Registro.Mensaje(string.Format("Comprobando si existen ficheros en la ruta {0}", directorio));
                    //Comprobar si hay ficheros
                    string[] ficheros = Directory.GetFiles(@directorio, "*.xlsx");
                    if (ficheros.Length > 0)
                    {
                        operacion = 0;
                    }
                    else
                    {
                        operacion = 1;
                    }

                    switch (operacion)
                    {
                        case 0:
                            operacion = -1;
                            Registro.Mensaje(string.Format("Cargando Estadísticas de la ruta {0}", directorio));
                            CargaEstadisticas.Carga(0, directorio);
                            break;
                        case 1:

                            //int periodo = calc.GetUltimaSemanaHistorificada(out anyo);
                            if (Historificacion.GetUltimaSemanaHistorificada(out periodo, out anyo))
                            {
                                operacion = 1;
                                Registro.Mensaje(string.Format("Historificando semana {0} del año {1}", periodo, anyo));
                                Historificacion.Calcula(operacion, periodo, anyo);
                            }
                            else
                            {
                                operacion = 2;
                                if (Historificacion.GetUltimoMesHistorificado(out mes, out anyo))
                                {
                                    Registro.Mensaje(string.Format("Historificando mes {0} del año {1}", mes, anyo));
                                    Historificacion.Calcula(operacion, mes, anyo);
                                }
                                else
                                {
                                    iteracion++;
                                }
                            }
                            operacion = -1;
                            break;
                        default:
                            //Registro.Mensaje("Operacion no implementada. Solo se permite 0, 1, 2 y 99 para Salir.");
                            break;
                    }
                    if (iteracion > 5)
                    {
                        Registro.Mensaje("Ha finalizado correctamente la carga de datos e historificación de los mismos.");
                        seguir = false;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(15000);
                    }
                } while (seguir);
            }
            catch (Exception ex)
            {
                Registro.Mensaje(ex.Message);
            }
        }
    }
}
