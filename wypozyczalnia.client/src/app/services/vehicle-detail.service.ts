import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { VehicleDetail } from '../shared/vehicle-detail.model';
import { NgForm } from '@angular/forms';

@Injectable({
  providedIn: 'root',
})
export class VehicleDetailService {
  url: string = environment.apiBaseUrl + '/Vehicle';
  list: VehicleDetail[] = [];
  isEdit: boolean = false;
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
    var a = this.http.post(this.url, this.formData); 
    this.refreshList(); 
    return a;
  }

  putVehicleDetail() {
    return this.http.put(this.url + '/' + this.formData.vin, this.formData);
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.formData = new VehicleDetail();
  }

  deleteVehicleDetail(vin: string) {
    var a = this.http.delete(this.url + '/' + vin); 
    this.refreshList();
    return a;
  }
}
