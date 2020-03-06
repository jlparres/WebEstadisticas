import { Component, OnInit } from '@angular/core';
import { EstadisticasService } from '../services/estadisticas.service';
import { IEstadisticas, IDatosDynatrace } from '../estadisticas/IDatosDynatrace';
// import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
// import { StockChart } from 'angular-highcharts';
import * as Highcharts from 'highcharts';

@Component({
  selector: 'app-estadisticas',
  templateUrl: './estadisticas.component.html',
  styleUrls: ['./estadisticas.component.css']
})

export class EstadisticasComponent implements OnInit {
  datosEstadisticas: IEstadisticas[] | undefined;
  metricas: string[] | undefined;

  // Tabla de graficos
  // stock: StockChart;

  constructor(private estadisticasService: EstadisticasService) { }

  ngOnInit() {
    this.estadisticasService.getStatisticsByFilter()
      .subscribe(y => {
        this.datosEstadisticas = y as IEstadisticas[];
        this.getGraphics();
      },
        error => console.log("Error", error)
    );

    this.estadisticasService.getMetricas()
      .subscribe(x => {
        this.metricas = x as string[];
        console.log("metricas", x);
      },
        error => console.log("Error Metricas", error)
    );
  }

  getGraphics() {
    let filtro = "volumetria";
    let volumetria = [];
    let promedio = [];
    let percentil = [];
    let percentil95 = [];
    let excepcion = [];
    let promedioExcepciones = [];

    //(Excepciones / NumPromedio) * 100

    //console.log("this.datos", this.datosEstadisticas);

    this.datosEstadisticas.forEach(function (element, index) {
      const auxDate = new Date(element["fecha"]).getTime();
      let auxExcepcion = 0;

      if (element["promedio"] != null && element["promedio"] > 0 && element["excepcion"] != null && element["excepcion"] > 0) {
        let aux = (element["excepcion"] / element["promedio"]);

        auxExcepcion = parseFloat(aux.toFixed(2));
      }
      
      volumetria.push([auxDate, element["volumetria"]]);
      promedio.push([auxDate, parseFloat(element["promedio"].toFixed(2))]);
      percentil.push([auxDate, element["percentil"]]);
      percentil95.push([auxDate, parseFloat(element["percentil95"].toFixed(2))]);
      excepcion.push([auxDate, element["excepcion"]]);
      promedioExcepciones.push([auxDate, auxExcepcion]);
    });
/*
    this.stock = new StockChart({
      chart: {
        type: 'line', //line, bar,
        zoomType: 'x'
      },
      xAxis: {
        type: 'datetime',
        title: {
          text: 'Días'
        }
      },
      yAxis: {
        title: {
          text: 'Núm Peticiones'
        }
      },
      rangeSelector: {
        selected: 1,
        inputPosition: {
          align: 'left',
          x: 0,
          y: 0
        }
      },
      title: {
        text: 'Peticiones Dynatrace'
      },
      legend: {
        enabled: true,
        align: 'right',
        verticalAlign: 'top',
        layout: 'vertical',
        x: 0,
        y: 100
      },
      series: [
      {
        name: 'Volumetria',
        data: volumetria
      },
      {
        name: 'Percentil',
        data: percentil
      },
      {
        name: 'Percentil95',
        data: percentil95
      },
      {
        name: 'Excepciones',
        data: excepcion
      },
      {
        name: '% Excepciones',
        data: promedioExcepciones
      }],
    });
    */
  }
}
