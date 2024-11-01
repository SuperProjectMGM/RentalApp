import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { VehicleDetail } from './vehicle-detail.model';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class VehicleDetailService {
  url: string = environment.apiBaseUrl + '/VehiclesDetail';
  list: VehicleDetail[] = [];
  formData: VehicleDetail = new VehicleDetail();
  constructor(private http: HttpClient) {}

  refreshList() {
    this.http.get(this.url).subscribe({
      next: (res) => {
        this.list = res as VehicleDetail[];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
  postVehicleDetail() {
    return this.http.post(this.url, this.formData);
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.formData = new VehicleDetail();
  }
}
