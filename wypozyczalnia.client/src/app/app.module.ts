import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { VehiclesDetailsComponent } from './vehicles-details/vehicles-details.component';
import { VehicleDetailFormComponent } from './vehicle-detail-form/vehicle-detail-form.component';
import { FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
  ],
  providers: [],
})
export class AppModule {}
