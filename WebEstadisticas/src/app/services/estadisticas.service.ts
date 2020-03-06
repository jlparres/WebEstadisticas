import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PlatformLocation } from '@angular/common';

@Injectable()
export class EstadisticasService {
  /* localhost */
  //private apiURLByDate = (this.platformLocation as any).location.origin + "/api/Estadisticas/GetStatisticsByDate";
  //private apiURLByFilter = (this.platformLocation as any).location.origin + "/api/Estadisticas/GetStatisticsByFilter";
  //private apiGetMetricas = (this.platformLocation as any).location.origin + "/api/Estadisticas/GetMetricas";


  /* PRE */
  private apiURL = (this.platformLocation as any).location.origin + "/WebEstadisticas/api/Estadisticas";
  private apiURLByDate = (this.platformLocation as any).location.origin + "/WebEstadisticas/api/Estadisticas/GetStatisticsByDate";
  private apiURLByFilter = (this.platformLocation as any).location.origin + "/WebEstadisticas/api/Estadisticas/GetStatisticsByFilter";
  private apiGetMetricas = (this.platformLocation as any).location.origin + "/WebEstadisticas/api/Estadisticas/GetMetricas";


  private params = new HttpParams();
  private canal = "TODOS";
  private fechaInicio = "01/01/2000";
  private fechaFin = "31/12/2030";
  private metrica = "WS8_ObtenerDatosCliente";

  constructor(private http: HttpClient, private platformLocation: PlatformLocation) { }

  getEstadisticas() {
    return this.http.get(this.apiURL);
  }

  getEstadisticsByDate() {
    let fechaInicio = "01/11/2018";
    let fechaFin = "31/12/2018";
    let parametros = "?metrica=WS8_ObtenerDatosCliente&canal=TODOS";

    if (fechaInicio && fechaFin) {
      parametros += "&fechaInicio=" + fechaInicio + "&fechaFin=" + fechaFin;
    }
    else {
      if (!fechaInicio && fechaFin) {
        parametros += "&fechaInicio=''&fechaFin=" + fechaFin;
      }
      else {
        if (fechaInicio && !fechaFin) {
          parametros += "&fechaInicio=" + fechaInicio + "$fechaFin=''";
        }
        else {
          parametros += "&fechaInicio=''&fechaFin=''";
        }
      }
    }

    

    this.apiURLByDate = this.apiURLByDate + parametros;

    console.log("apiURLByDate", this.apiURLByDate);

    return this.http.get(this.apiURLByDate);
  }

  getStatisticsByFilter() {
    const dateAux = new Date();
    let fechaInicio = "01/01/2000";
    let fechaFin = "31/12/2018";
    //this.fechaFin = dateAux.getDate() + "/" + (dateAux.getMonth() + 1) + "/" + dateAux.getFullYear();

    console.log("dateAux", dateAux);
    console.log("fechaFin", fechaFin);
    let parametros = "?metrica=WS8_ObtenerDatosCliente&canal=TODOS";
    let canal = "TODOS";
    let metrica = "WS8_ObtenerDatosCliente";

    if (fechaInicio && fechaFin) {
      parametros += "&fechaInicio=" + fechaInicio + "&fechaFin=" + fechaFin;
    }
    else {
      if (!fechaInicio && fechaFin) {
        parametros += "&fechaInicio=''&fechaFin=" + fechaFin;
      }
      else {
        if (fechaInicio && !fechaFin) {
          parametros += "&fechaInicio=" + fechaInicio + "$fechaFin=''";
        }
        else {
          parametros += "&fechaInicio=''&fechaFin=''";
        }
      }
    }

    console.log("Location", (this.platformLocation as any).location);
    console.log("GET", this.apiURLByFilter);

    this.params = this.params.append('metrica', this.metrica);
    this.params = this.params.append('canal', this.canal);
    this.params = this.params.append('fechaInicio', this.fechaInicio);
    this.params = this.params.append('fechaFin', this.fechaFin);

    return this.http.get(this.apiURLByFilter, { params: this.params });
  }

  getMetricas() {
    return this.http.get(this.apiGetMetricas);
  }

  getFechaInicio() {
    return this.fechaInicio;
  }

  getFechaFin() {
    return this.fechaFin;
  }

  getMetrica() {
    return this.metrica;
  }

  getCanal() {
    return this.canal;
  }
}
