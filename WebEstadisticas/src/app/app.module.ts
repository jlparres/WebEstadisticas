import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { EstadisticasService } from './services/estadisticas.service';
import { EstadisticasComponent } from './estadisticas/estadisticas.component';
import { FooterComponent } from './footer/footer.component';

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
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'estadisticas', component: EstadisticasComponent }
    ])
  ],
  // providers: [ EstadisticasService, { provide: HIGHCHARTS_MODULES, useFactory: highchartsModules }],
  providers: [ EstadisticasService ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
