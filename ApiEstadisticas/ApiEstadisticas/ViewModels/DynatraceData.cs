using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiEstadisticas.ViewModels
{
    public class DynatraceData
    {
        public DateTime Fecha { get; set; }
        public int Volumetria { get; set; }
        public float Promedio { get; set; }
        public int Percentil { get; set; }
        public float Percentil95 { get; set; }
        public int Excepciones { get; set; }
        //public double PorcientoExcepciones { get; set; }
        public string Canal { get; set; }
        public string Metrica { get; set; }

        public double PorcientoExcepciones => this.Volumetria > 0 ? Math.Round((((double)Excepciones / (double)Volumetria) * 100), 2) : 0;
        public string SFecha => Fecha.ToString("dd/MM/yyyy");
    }
}
