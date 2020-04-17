import { Component, OnInit } from '@angular/core';
import { EstadisticasService } from '../../services/estadisticas.service';
import { IEstadisticas, IDatosDynatrace } from '../estadisticas/IDatosDynatrace';

import { StockChart } from 'angular-highcharts';
// import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
// import * as Highcharts from 'highcharts';

@Component({
  selector: 'app-estadisticas',
  templateUrl: './estadisticas.component.html',
  styleUrls: ['./estadisticas.component.css']
})

export class EstadisticasComponent implements OnInit {
  datosEstadisticas: IEstadisticas[] | undefined;
  metricas: string[] | undefined;

  // Tabla de graficos
  stock: StockChart;

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

    console.log("this.datos", this.datosEstadisticas);

    this.datosEstadisticas.forEach(function (element, index) {
      const auxDate = new Date(element["Fecha"]).getTime();
      let auxExcepcion = 0;

      if (element["Promedio"] != null && element["Promedio"] > 0 && element["Excepciones"] != null && element["Excepciones"] > 0) {
        let aux = (element["Excepciones"] / element["Promedio"]);

        auxExcepcion = parseFloat(aux.toFixed(2));
      }
      
      volumetria.push([auxDate, element["Volumetria"]]);
      promedio.push([auxDate, parseFloat(element["Promedio"].toFixed(2))]);
      percentil.push([auxDate, element["Percentil"]]);
      //percentil95.push([auxDate, parseFloat(element["Percentil95"].toFixed(2))]);
      excepcion.push([auxDate, element["Excepciones"]]);
      promedioExcepciones.push([auxDate, auxExcepcion]);
    });

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
          text: 'Peticiones / Promedios'
        }
      },
      rangeSelector: {
        selected: 1,
        inputPosition: {
          align: 'right',
          x: 0,
          y: 0
        }
      },
      title: {
        text: 'Volumetrias por Web Services'
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
          tooltip: { valueDecimals: 0 },
          name: 'Volumetria', 
          data: volumetria,
          type: undefined
        },
        {
          tooltip: { valueDecimals: 0 },
          name: 'Promedio (ms)', 
          data: promedio,
          type: undefined
        },
        {
          tooltip: { valueDecimals: 0 },
          name: 'Excepciones', 
          data: excepcion,
          type: undefined
        },
        {
          tooltip: { valueDecimals: 2 },
          name: '% Excepciones', 
          data: promedioExcepciones,
          type: undefined,
          dashStyle: 'ShortDot'
        }
      ]
    });
  
    console.log("Stock", this.stock);
  }
}
