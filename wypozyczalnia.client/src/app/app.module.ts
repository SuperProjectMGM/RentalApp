import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VehiclesDetailsComponent } from './components/vehicles-details/vehicles-details.component';
import { VehicleDetailFormComponent } from './components/vehicle-detail-form/vehicle-detail-form.component';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { LoginComponent } from './components/login/login.component';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    LoginComponent,
    VehicleDetailFormComponent,
    VehiclesDetailsComponent,
    BrowserAnimationsModule,
    AppComponent,
    ToastrModule.forRoot(),
    MatDialogModule,
  ],
  providers: [],
})
export class AppModule {}
