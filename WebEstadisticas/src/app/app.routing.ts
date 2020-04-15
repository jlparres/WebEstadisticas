import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { EstadisticasComponent } from './components/estadisticas/estadisticas.component';
import { ContactComponent } from './components/contact/contact.component';
import { CertificatesComponent } from './components/certificates/certificates.component';

const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'home', component: HomeComponent },
    { path: 'estadisticas', component: EstadisticasComponent },
    { path: 'certificados', component: CertificatesComponent },
    { path: 'contacto', component: ContactComponent }

];

export const appRoutingProviders: any[] = [];
export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);


