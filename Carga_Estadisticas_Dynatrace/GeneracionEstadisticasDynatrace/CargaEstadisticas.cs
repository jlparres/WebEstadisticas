using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetLight;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;


namespace GeneracionEstadisticasDynatrace
{
    public static class CargaEstadisticas
    {
        public static void Carga(int operacion, string ruta)
        {
            //string[] ficheros = Directory.GetFiles(@"E:\ExcelDynatrace\", "*.xlsx");
            string[] ficheros = Directory.GetFiles(@ruta, "*.xlsx");
            foreach (string fichero in ficheros)
            {
                string nombreFichero = fichero.Replace(ruta, "").Replace("\\", "");

                Registro.Mensaje(string.Format("Inicio de la carga del fichero {0} ...", nombreFichero));

                IFormatProvider culture = new CultureInfo("es-ES", true);
                DateTime fecha = DateTime.ParseExact(nombreFichero.Substring(7, 10), "yyyy-MM-dd", culture).AddDays(-1);

                using (var db = new DATA_DYNAEntities())
                {
                    //Verificamos si ya se ha cargado ese día
                    var cargado = db.DatosDynatrace.Where(m => m.Fecha_dato == fecha).FirstOrDefault();
                    if (cargado != null) //Ya se ha cargado
                    {
                        Registro.Mensaje(string.Format("El día {0} ya ha sido cargado", fecha.ToShortDateString()));

                        string rutaDuplicado = Path.Combine(ruta, "Duplicados");
                        string nombreDuplicado = Path.GetFileName(nombreFichero);
                        string destino = Path.Combine(rutaDuplicado, nombreDuplicado);

                        if (!Directory.Exists(destino))
                        {
                            Directory.CreateDirectory(rutaDuplicado);
                        }

                        if (File.Exists(destino))
                        {
                            File.Delete(destino);
                        }

                        File.Move(fichero, destino);

                        return;
                    }

                    //Procesamos la pestaña Chart (métodos)
                    SLDocument sl = new SLDocument(fichero, "Chart");
                    int iRow = 7;
                    int excepciones = 0;
                    float promedio = 0;
                    int numpromedio = 0;
                    string tipoMetrica = "";

                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                    {
                        string level = sl.GetCellValueAsString(iRow, 1);
                        string measure = sl.GetCellValueAsString(iRow, 2);
                        string aggregation = sl.GetCellValueAsString(iRow, 4);
                        float average = (float)sl.GetCellValueAsDecimal(iRow, 5);
                        int count = sl.GetCellValueAsInt32(iRow, 7);

                        if (level == "1")
                        {
                            string[] submetricas = measure.Replace(" ", "").Split('-');
                            string metrica = submetricas[1].Trim();
                            string tipo = submetricas[2];


                            if (aggregation == "Average" & tipo.Substring(0, 3) == "Exc")
                            {
                                excepciones = count;
                                tipoMetrica = "EXCEPCIONES|" + metrica;
                            }

                            else if (aggregation == "Average" & tipo.Substring(0, 3) == "Pur")
                            {
                                promedio = average;
                                numpromedio = count;
                                tipoMetrica = "PROMEDIOS|" + metrica;
                            }

                            else if (aggregation == "95th Percentile" & tipo.Substring(0, 3) == "Pur")
                            {
                                var oMiExcel = new DatosDynatrace();
                                oMiExcel.Metrica = metrica.Trim();
                                string canal = "TODOS";
                                oMiExcel.Canal = canal;
                                oMiExcel.Excepciones = excepciones;
                                oMiExcel.Promedio = promedio;
                                oMiExcel.Percentil95 = average;
                                oMiExcel.NumPromedio = numpromedio;
                                oMiExcel.NumPercentil = count;
                                oMiExcel.Fecha_dato = fecha;
                                tipoMetrica = "PERCENTILES|" + metrica;

                                db.DatosDynatrace.Add(oMiExcel);
                                db.SaveChanges();

                            }
                        }
                        else //level=2
                        {
                            //separamos tipoMetrica para saber el tipo de metrica y la metrica

                            string[] paso = tipoMetrica.Split('|');
                            string tipoVariable = paso[0];
                            string varMetrica = paso[1].Trim();

                            var resultado = db.DatosDynatrace.Where(m => m.Canal == measure && m.Metrica == varMetrica && m.Fecha_dato == fecha).FirstOrDefault<DatosDynatrace>();

                            if (resultado == null) //no existe registro
                            {
                                var oMiExcelCanal = new DatosDynatrace();
                                oMiExcelCanal.Metrica = varMetrica.Trim();
                                oMiExcelCanal.Canal = measure;
                                oMiExcelCanal.Fecha_dato = fecha;

                                if (tipoVariable == "EXCEPCIONES")
                                {
                                    oMiExcelCanal.Excepciones = count;
                                    oMiExcelCanal.Promedio = 0;
                                    oMiExcelCanal.Percentil95 = 0;
                                    oMiExcelCanal.NumPromedio = 0;
                                    oMiExcelCanal.NumPercentil = 0;
                                }
                                else if (tipoVariable == "PROMEDIOS")
                                {
                                    oMiExcelCanal.Excepciones = 0;
                                    oMiExcelCanal.Promedio = average;
                                    oMiExcelCanal.Percentil95 = 0;
                                    oMiExcelCanal.NumPromedio = count;
                                    oMiExcelCanal.NumPercentil = 0;
                                }
                                else if (tipoVariable == "PERCENTILES")
                                {
                                    oMiExcelCanal.Excepciones = 0;
                                    oMiExcelCanal.Promedio = 0;
                                    oMiExcelCanal.Percentil95 = average;
                                    oMiExcelCanal.NumPromedio = 0;
                                    oMiExcelCanal.NumPercentil = count;
                                }
                                //Insertamos el registro nuevo
                                db.DatosDynatrace.Add(oMiExcelCanal);
                                db.SaveChanges();
                            }
                            else //hay registro ya en bbdd para esa métrica y tipo
                            {
                                if (tipoVariable == "EXCEPCIONES")
                                {
                                    resultado.Excepciones = count;
                                }
                                else if (tipoVariable == "PROMEDIOS")
                                {
                                    resultado.Promedio = average;
                                    resultado.NumPromedio = count;

                                }
                                else if (tipoVariable == "PERCENTILES")
                                {
                                    resultado.Percentil95 = average;
                                    resultado.NumPercentil = count;
                                }
                                //Actualizamos el registro con el dato modificado
                                db.DatosDynatrace.Add(resultado);
                                db.Entry(resultado).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }

                        }
                        iRow++;
                    }

                    //Procesamos las WebRequests
                    sl = new SLDocument(fichero, "Web Requests");
                    iRow = 9;
                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
                    {
                        string uri = sl.GetCellValueAsString(iRow, 4);
                        string canal = sl.GetCellValueAsString(iRow, 5);
                        float tasaFallo = (float)sl.GetCellValueAsDecimal(iRow, 6);
                        int count = sl.GetCellValueAsInt32(iRow, 8);

                        var oMiExcel = new WebRequestsDynatrace();
                        oMiExcel.URI = uri;
                        oMiExcel.Canal = canal;
                        oMiExcel.TasaFallo = tasaFallo;
                        oMiExcel.Numero = count;
                        oMiExcel.Fecha_dato = fecha;

                        try
                        {
                            db.WebRequestsDynatrace.Add(oMiExcel);
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            Registro.Mensaje(string.Format("Se ha truncado algún valor: {0}", ex.Data));
                            oMiExcel.URI = "URI Truncada: " + uri.Substring(0, 110);
                            db.WebRequestsDynatrace.Add(oMiExcel);
                            db.SaveChanges();
                        }
                        iRow++;
                    }

                    int contador = db.DatosDynatrace.Where(x => x.Fecha_dato == fecha).Count();

                    Registro.Mensaje(string.Format("Se han cargado {0} registros del fichero {1}", contador, nombreFichero));
                    Registro.Mensaje(string.Format("Fin de la carga del fichero {0}", nombreFichero));
                }

                //Movemos el fichero a la carpeta de procesados

                //string rutaDestino = @"E:\ExcelDynatrace\Procesados";
                string rutaDestino = Path.Combine(ruta, "Procesados");
                string nombre_fichero = Path.GetFileName(fichero);
                string ficheroDestino = Path.Combine(rutaDestino, nombre_fichero);

                if (!Directory.Exists(rutaDestino))
                {
                    Directory.CreateDirectory(rutaDestino);
                }

                if (File.Exists(ficheroDestino))
                {
                    File.Delete(ficheroDestino);
                }
                File.Move(fichero, ficheroDestino);

                //Registro.Mensaje(string.Format("Se ha cargado la información del fichero {0}", nombreFichero));
            }
        }
    }
}
