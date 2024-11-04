import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component'; // Upewnij się, że ścieżka jest poprawna
import { VehiclesDetailsComponent } from './vehicles-details/vehicles-details.component';
import { AuthGuard } from './shared/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: 'protected',
    component: VehiclesDetailsComponent,
    canActivate: [AuthGuard],
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }, // Przekierowanie w przypadku nieznanej ścieżki
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
