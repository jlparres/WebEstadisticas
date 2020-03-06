using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneracionEstadisticasDynatrace
{
    public static class Historificacion
    {
        public static bool GetUltimoMesHistorificado(out int mes, out int anyo)
        {
            bool result = false;
            mes = 0;
            anyo = 0;

            using (var db = new DATA_DYNAEntities())
            {
                DateTime fechaInicio = DateTime.Now;
                DateTime fechaFin = fechaInicio;
                int diasDelMes = 0;

                if (db.DatosDynatrace.Count() > 0)
                {
                    if (db.DatosDynatraceMes.Count() > 0)
                    {
                        // Ya tenemos datos historificados en la tabla de Mes.

                        // Ultimo mes historificado
                        int ultimoAnyo = db.DatosDynatraceMes.Max(x => x.Anyo);
                        int ultimoMes = db.DatosDynatraceMes.Where(x => x.Anyo == ultimoAnyo).Max(x => x.Mes) + 1;

                        if (ultimoMes > 12)
                        {
                            anyo = ultimoAnyo + 1;
                            mes = ultimoMes = 1;
                        }
                        else
                        {
                            anyo = ultimoAnyo;
                            mes = ultimoMes;
                        }

                        // Comprobamos que existen datos para historificar.
                        diasDelMes = DateTime.DaysInMonth(anyo, mes);
                        fechaInicio = new DateTime(anyo, mes, 1);
                        fechaFin = new DateTime(anyo, mes, diasDelMes);
                    }
                    else
                    {
                        // No tenemos datos mensuales historificados.
                        // Devolvemos el primer mes a historificar.

                        fechaInicio = db.DatosDynatrace.Min(x => x.Fecha_dato);
                        diasDelMes = DateTime.DaysInMonth(fechaInicio.Year, fechaInicio.Month);
                        fechaFin = new DateTime(fechaInicio.Year, fechaInicio.Month, diasDelMes);

                        mes = fechaInicio.Month;
                        anyo = fechaInicio.Year;
                    }

                    bool bFechaInicio = db.DatosDynatrace.Any(x => x.Fecha_dato == fechaInicio);
                    int iFechaInicio = db.DatosDynatrace.Where(x => x.Fecha_dato == fechaInicio).Count();

                    bool bFechaFin = db.DatosDynatrace.Any(x => x.Fecha_dato == fechaFin);
                    int iFechaFin = db.DatosDynatrace.Where(x => x.Fecha_dato == fechaFin).Count();


                    if (db.DatosDynatrace.Any(x => x.Fecha_dato == fechaInicio) &&
                            db.DatosDynatrace.Any(x => x.Fecha_dato == fechaFin))
                    {
                        result = true;
                    }
                    else
                    {
                        Registro.Mensaje(string.Format("El mes {0} del año {1} no se puede historificiar porque no ha terminado.", mes, anyo));
                        mes = 0;
                        anyo = 0;
                    }
                }
                else
                {
                    // No existen datos a historificar.
                    Registro.Mensaje("No existen datos que historificar.");
                }
            }

            return result;
        }

        public static bool GetUltimaSemanaHistorificada(out int semana, out int anyo)
        {
            bool result = false;
            anyo = 0;
            semana = 0;

            using (var db = new DATA_DYNAEntities())
            {
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                Calendar cal = dfi.Calendar;
                DateTime fechaInicio = DateTime.Now;
                DateTime fechaFin = fechaInicio;

                if (db.DatosDynatrace.Count() > 0)
                {
                    // Hay datos en base de datos y miramos si hay que historificar
                    if (db.DatosDynatraceSemana.Count() > 0)
                    {
                        // Ya existen semanas historificadas.
                        // Comprobar la última semana historificada.

                        int ultimoAnyoHistorificado = db.DatosDynatraceSemana.Max(x => x.Anyo);
                        semana = db.DatosDynatraceSemana.Where(x => x.Anyo == ultimoAnyoHistorificado).Max(x => x.Semana) + 1;

                        DateTime primerDiaSemana = FirstDateOfWeek(ultimoAnyoHistorificado, semana, CultureInfo.CurrentCulture);
                        DateTime ultimoDiaSemana = primerDiaSemana.AddDays(6);

                        int semanaPrimerDia = cal.GetWeekOfYear(primerDiaSemana, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                        int semanaUltimoDia = cal.GetWeekOfYear(ultimoDiaSemana, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

                        if (semanaPrimerDia == semanaUltimoDia)
                        {
                            // La semana que queremos historificar es OK.
                            semana = semanaPrimerDia;
                            anyo = ultimoAnyoHistorificado;
                        }
                        else
                        {
                            // Es la primera semana del año.
                            semana = 1;
                            anyo = ultimoAnyoHistorificado + 1;
                        }

                        primerDiaSemana = FirstDateOfWeek(anyo, semana, CultureInfo.CurrentCulture);
                        ultimoDiaSemana = primerDiaSemana.AddDays(6);

                        if (db.DatosDynatrace.Any(x => x.Fecha_dato == primerDiaSemana) &&
                            db.DatosDynatrace.Any(x => x.Fecha_dato == ultimoDiaSemana))
                        {
                            // Existen datos fecha inicio y fecha fin.
                            result = true;
                        }
                        else
                        {
                            Registro.Mensaje(string.Format("La semana {0} del año {1} no ha finalizado.", semana, anyo));
                        }
                    }
                    else
                    {
                        // No hay datos historificados y devolvemos la primera semana de todos los datos 
                        // para historificarla.
                        fechaInicio = db.DatosDynatrace.Min(x => x.Fecha_dato);
                        fechaFin = fechaInicio.AddDays(6);
                        anyo = fechaFin.Year;
                        semana = cal.GetWeekOfYear(fechaFin, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

                        if (db.DatosDynatrace.Any(x => x.Fecha_dato == fechaFin))
                        {
                            result = true;
                        }
                        else
                        {
                            Registro.Mensaje(string.Format("La semana {0} del año {1} no ha finalizado.", semana, anyo));
                            semana = 0;
                            anyo = 0;
                        }
                    }
                }
                else
                {
                    Registro.Mensaje("No existen datos que historificar.");
                }
            }

            return result;
        }

        public static void Calcula(int operacion, int periodo, int anyo)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            using (var db = new DATA_DYNAEntities())
            {
                if (operacion.Equals(1)) //Historificamos semanas
                {
                    int semanaActual = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                    if (semanaActual <= periodo)
                    {
                        Registro.Mensaje(string.Format("No se puede historificar la semana {0} hasta que haya acabado", semanaActual));
                        return;
                    }

                    //IFormatProvider culture = new CultureInfo("en-ES", true);

                    DateTime fechaInicial = new Week(anyo, periodo).FirstDayOfWeek;
                    DateTime fechaFin = new Week(anyo, periodo).LastDayOfWeek.AddDays(1);

                    //HISTORIFICACION DE MÉTODOS
                    var query = db.DatosDynatraceSemana.Where(s => s.Semana == periodo).FirstOrDefault<DatosDynatraceSemana>();
                    if (query != null) //Ya se ha historificado
                    {
                        Registro.Mensaje(string.Format("La semana {0} ya está historificada", periodo));
                        return;
                    }
                    else //Historificamos la semana
                    {
                        var datosBruto = db.DatosDynatrace.Where(p => p.Fecha_dato >= fechaInicial && p.Fecha_dato < fechaFin).GroupBy(s => new { s.Metrica, s.Canal }).ToList();
                        foreach (var row in datosBruto)
                        {
                            float numPromedioTotal = 0;
                            float numPercentilTotal = 0;
                            float promedioTotal = 0;
                            float percentilTotal = 0;
                            float excepcionTotal = 0;
                            int dias = 0;
                            var registroSemanal = new DatosDynatraceSemana();
                            string metrica = "";
                            string canal = "";

                            foreach (var col in row)
                            {
                                numPromedioTotal = numPromedioTotal + col.NumPromedio;
                                numPercentilTotal = numPercentilTotal + col.NumPercentil;
                                promedioTotal = promedioTotal + col.Promedio;
                                percentilTotal = percentilTotal + col.Percentil95;
                                excepcionTotal = excepcionTotal + col.Excepciones;
                                metrica = col.Metrica;
                                canal = col.Canal;

                                if (col.Promedio != 0)
                                {
                                    dias++;
                                }
                            }
                            registroSemanal.NumPromedio = numPromedioTotal;
                            registroSemanal.NumPercentil = numPercentilTotal;
                            registroSemanal.Promedio = (dias == 0) ? 0 : promedioTotal / dias;
                            registroSemanal.Percentil95 = (dias == 0) ? 0 : percentilTotal / dias;
                            registroSemanal.Excepciones = excepcionTotal;
                            registroSemanal.Metrica = metrica;
                            registroSemanal.Canal = canal;
                            registroSemanal.Semana = periodo;
                            registroSemanal.NumDiasActividad = dias;
                            registroSemanal.Anyo = anyo;
                            db.DatosDynatraceSemana.Add(registroSemanal);
                            db.SaveChanges();
                        }
                    }

                    //HISTORIFICACION DE WEBREQUEST
                    var query2 = db.WebRequestsDynatraceSemana.Where(s => s.Semana == periodo).FirstOrDefault<WebRequestsDynatraceSemana>();
                    if (query2 != null) //Ya se ha historificado
                    {
                        Registro.Mensaje(string.Format("La semana {0} ya está historificada", periodo));
                        return;
                    }
                    else //Historificamos la semana
                    {
                        var datosBruto2 = db.WebRequestsDynatrace.Where(p => p.Fecha_dato >= fechaInicial && p.Fecha_dato < fechaFin).GroupBy(s => new { s.URI, s.Canal }).ToList();
                        foreach (var row in datosBruto2)
                        {
                            float numTotal = 0;
                            float tasaFalloTotal = 0;
                            int dias = 0;
                            var registroSemanal = new WebRequestsDynatraceSemana();
                            string URI = "";
                            string canal = "";

                            foreach (var col in row)
                            {
                                numTotal = numTotal + col.Numero;
                                tasaFalloTotal = tasaFalloTotal + col.TasaFallo;
                                URI = col.URI;
                                canal = col.Canal;

                                if (col.Numero != 0)
                                {
                                    dias++;
                                }
                            }
                            registroSemanal.TasaFallo = tasaFalloTotal / dias;
                            registroSemanal.Numero = (int)numTotal;
                            registroSemanal.URI = URI;
                            registroSemanal.Canal = canal;
                            registroSemanal.Semana = periodo;
                            registroSemanal.Anyo = anyo;
                            registroSemanal.NumDiasActividad = dias;
                            db.WebRequestsDynatraceSemana.Add(registroSemanal);
                            db.SaveChanges();
                        }
                    }
                }

                else if (operacion.Equals(2)) //Historificamos meses
                {

                    //int mesActual = cal.GetMOnthOfYear(DateTime.Now, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                    int mesActual = DateTime.Now.Month;

                    if (mesActual <= periodo)
                    {
                        Registro.Mensaje(string.Format("No se puede historificar el mes {0} hasta que haya acabado", mesActual));
                        return;
                    }

                    //IFormatProvider culture = new CultureInfo("en-ES", true);

                    DateTime fechaInicial = new DateTime(anyo, periodo, 1);
                    DateTime fechaFin = fechaInicial.AddMonths(1).AddDays(-1);
                    //HISTORIFICACION DE MÉTODOS
                    var query = db.DatosDynatraceMes.Where(s => s.Mes == periodo).FirstOrDefault<DatosDynatraceMes>();
                    if (query != null) //Ya se ha historificado
                    {
                        Registro.Mensaje(string.Format("El mes {0} ya está historificado", periodo));
                        return;
                    }
                    else //Historificamos el mes
                    {
                        var datosBruto = db.DatosDynatrace.Where(p => p.Fecha_dato >= fechaInicial && p.Fecha_dato < fechaFin).GroupBy(s => new { s.Metrica, s.Canal }).ToList();
                        foreach (var row in datosBruto)
                        {
                            float numPromedioTotal = 0;
                            float numPercentilTotal = 0;
                            float promedioTotal = 0;
                            float percentilTotal = 0;
                            float excepcionTotal = 0;
                            int dias = 0;
                            var registroMensual = new DatosDynatraceMes();
                            string metrica = "";
                            string canal = "";

                            foreach (var col in row)
                            {
                                numPromedioTotal = numPromedioTotal + col.NumPromedio;
                                numPercentilTotal = numPercentilTotal + col.NumPercentil;
                                promedioTotal = promedioTotal + col.Promedio;
                                percentilTotal = percentilTotal + col.Percentil95;
                                excepcionTotal = excepcionTotal + col.Excepciones;
                                metrica = col.Metrica;
                                canal = col.Canal;

                                if (col.Promedio != 0)
                                {
                                    dias++;
                                }
                            }
                            registroMensual.NumPromedio = numPromedioTotal;
                            registroMensual.NumPercentil = numPercentilTotal;
                            registroMensual.Promedio = (dias == 0) ? 0 : promedioTotal / dias;
                            registroMensual.Percentil95 = (dias == 0) ? 0 : percentilTotal / dias;
                            registroMensual.Excepciones = excepcionTotal;
                            registroMensual.Metrica = metrica;
                            registroMensual.Canal = canal;
                            registroMensual.Mes = periodo;
                            registroMensual.NumDiasActividad = dias;
                            registroMensual.Anyo = anyo;
                            db.DatosDynatraceMes.Add(registroMensual);
                            db.SaveChanges();
                        }
                    }

                    //HISTORIFICACION DE WEBREQUEST
                    var query2 = db.WebRequestsDynatraceMes.Where(s => s.Mes == periodo).FirstOrDefault<WebRequestsDynatraceMes>();
                    if (query2 != null) //Ya se ha historificado
                    {
                        Registro.Mensaje(string.Format("El mes {0} ya está historificado", periodo));
                        return;
                    }

                    else //Historificamos el mes
                    {
                        var datosBruto2 = db.WebRequestsDynatrace.Where(p => p.Fecha_dato >= fechaInicial && p.Fecha_dato < fechaFin).GroupBy(s => new { s.URI, s.Canal }).ToList();
                        foreach (var row in datosBruto2)
                        {
                            float numTotal = 0;
                            float tasaFalloTotal = 0;
                            int dias = 0;
                            var registroMensual = new WebRequestsDynatraceMes();
                            string URI = "";
                            string canal = "";

                            foreach (var col in row)
                            {
                                numTotal = numTotal + col.Numero;
                                tasaFalloTotal = tasaFalloTotal + col.TasaFallo;
                                URI = col.URI;
                                canal = col.Canal;

                                if (col.Numero != 0)
                                {
                                    dias++;
                                }
                            }
                            registroMensual.TasaFallo = tasaFalloTotal / dias;
                            registroMensual.Numero = numTotal;
                            registroMensual.URI = URI;
                            registroMensual.Canal = canal;
                            registroMensual.Mes = periodo;
                            registroMensual.NumDiasActividad = dias;
                            registroMensual.Anyo = anyo;
                            db.WebRequestsDynatraceMes.Add(registroMensual);
                            db.SaveChanges();
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }
    }
}
