import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideRouter } from '@angular/router';
import { importProvidersFrom } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app/app-routing.module';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter([]), // Tutaj wpisz swoje trasy
    importProvidersFrom(
      HttpClientModule,
      BrowserAnimationsModule,
      ToastrModule.forRoot(),
      AppRoutingModule
    ),
  ],
}).catch((err) => console.error(err));
