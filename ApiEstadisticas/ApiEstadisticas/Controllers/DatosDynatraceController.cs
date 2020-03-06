using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ApiEstadisticas.Models;
using ApiEstadisticas.ViewModels;

namespace ApiEstadisticas.Controllers
{
    public class DatosDynatraceController : ApiController
    {
        private DATA_DYNAEntities db = new DATA_DYNAEntities();

        // GET: api/DatosDynatrace
        public IQueryable<DatosDynatrace> GetDatosDynatrace()
        {
            return db.DatosDynatrace;
        }

        // GET: api/DatosDynatrace/5
        [ResponseType(typeof(DatosDynatrace))]
        public IHttpActionResult GetDatosDynatrace(int id)
        {
            DatosDynatrace datosDynatrace = db.DatosDynatrace.Find(id);
            if (datosDynatrace == null)
            {
                return NotFound();
            }

            return Ok(datosDynatrace);
        }

        // PUT: api/DatosDynatrace/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDatosDynatrace(int id, DatosDynatrace datosDynatrace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != datosDynatrace.ID)
            {
                return BadRequest();
            }

            db.Entry(datosDynatrace).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatosDynatraceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/DatosDynatrace
        [ResponseType(typeof(DatosDynatrace))]
        public IHttpActionResult PostDatosDynatrace(DatosDynatrace datosDynatrace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DatosDynatrace.Add(datosDynatrace);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = datosDynatrace.ID }, datosDynatrace);
        }

        // DELETE: api/DatosDynatrace/5
        [ResponseType(typeof(DatosDynatrace))]
        public IHttpActionResult DeleteDatosDynatrace(int id)
        {
            DatosDynatrace datosDynatrace = db.DatosDynatrace.Find(id);
            if (datosDynatrace == null)
            {
                return NotFound();
            }

            db.DatosDynatrace.Remove(datosDynatrace);
            db.SaveChanges();

            return Ok(datosDynatrace);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DatosDynatraceExists(int id)
        {
            return db.DatosDynatrace.Count(e => e.ID == id) > 0;
        }

        [HttpGet]
        [Route("api/Estadisticas/GetDataByFilter")]
        public IEnumerable<DynatraceData> GetDataByFilter(string metrica, string canal, string fechaInicio, string fechaFin)
        {
            metrica = string.IsNullOrEmpty(metrica) ? "WS8_ObtenerDatosCliente" : metrica;
            canal = string.IsNullOrEmpty(metrica) ? "TODOS" : canal;
            fechaInicio = string.IsNullOrEmpty(metrica) ? "01/01/1900" : fechaInicio;
            fechaFin = string.IsNullOrEmpty(metrica) ? DateTime.Today.ToShortDateString() : fechaFin;

            DateTime ini = DateTime.Parse(fechaInicio);
            DateTime fin = DateTime.Parse(fechaFin);

            var result = from s in db.DatosDynatrace
                         where s.Fecha_dato >= ini
                            && s.Fecha_dato <= fin
                            && s.Metrica.Contains(metrica)
                            && s.Canal.Contains(canal)
                         orderby s.Fecha_dato ascending
                         select new DynatraceData
                         {
                             Fecha = s.Fecha_dato,
                             Volumetria = s.NumPromedio,
                             Promedio = s.Promedio,
                             Percentil = s.NumPercentil,
                             Percentil95 = s.Percentil95,
                             Excepciones = s.Excepciones,
                             //PorcientoExcepciones = s.NumPromedio > 0 ? Math.Round((((double)s.Excepciones / (double)s.NumPromedio) * 100), 2) : 0,
                             Canal = s.Canal,
                             Metrica = s.Metrica
                         };

            if (result == null)
            {
                return null;
            }

            return result.ToList();
        }

        [HttpGet]
        [Route("api/Estadisticas/GetData")]
        public IEnumerable<DynatraceData> GetData(string metrica, string canal)
        {
            metrica = string.IsNullOrEmpty(metrica) ? string.Empty : metrica.Trim(' ');
            canal = string.IsNullOrEmpty(metrica) ? string.Empty : canal.Trim(' ');

            var result = from s in db.DatosDynatrace
                         where s.Metrica.Contains(metrica)
                            && s.Canal.Contains(canal)
                         orderby s.Fecha_dato ascending
                         select new DynatraceData
                         {
                             Fecha = s.Fecha_dato,
                             Volumetria = s.NumPromedio,
                             Promedio = s.Promedio,
                             Percentil = s.NumPercentil,
                             Percentil95 = s.Percentil95,
                             Excepciones = s.Excepciones,
                             Canal = s.Canal,
                             Metrica = s.Metrica
                         };

            if (result == null)
            {
                return null;
            }

            return result.ToList();
        }

        [HttpGet]
        [Route("api/Estadisticas/GetMetricas")]
        public IEnumerable<string> GetMetricaList()
        {
            return db.DatosDynatrace.Select(x => x.Metrica).Distinct().ToList();
        }

        [HttpGet]
        [Route("api/Estadisticas/GetCanales")]
        public IEnumerable<string> GetCanalesList()
        {
            return db.DatosDynatrace.Select(x => x.Canal).Distinct().ToList();
        }

        [HttpGet]
        [Route("api/Estadisticas/GetCanalesByMetrica")]
        public IEnumerable<string> GetCanalesByMetrica(string metrica)
        {
            return db.DatosDynatrace.Where(x => x.Metrica.Contains(metrica)).Select(x => x.Canal).Distinct().ToList();
        }
    }
}