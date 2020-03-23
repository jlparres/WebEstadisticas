import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { EstadisticasService } from './services/estadisticas.service';
import { EstadisticasComponent } from './components/estadisticas/estadisticas.component';
import { FooterComponent } from './components/footer/footer.component';

import { appRoutingProviders, routing } from './app.routing';

// import { ChartModule, HIGHCHARTS_MODULES } from 'angular-highcharts';
import stock from 'highcharts/modules/stock.src';
import more from 'highcharts/highcharts-more.src';

export function highchartsModules() {
  // apply Highcharts Modules to this array
  return [stock, more];
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    EstadisticasComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    // ChartModule,
    routing
  ],
  // providers: [ EstadisticasService, { provide: HIGHCHARTS_MODULES, useFactory: highchartsModules }],
  providers: [ EstadisticasService, appRoutingProviders ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
