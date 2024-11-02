import { Component } from '@angular/core';
import { VehicleDetailService } from '../../shared/vehicle-detail.service';
import { NgForm } from '@angular/forms';
import { VehicleDetail } from '../../shared/vehicle-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vehicle-detail-form',
  templateUrl: './vehicle-detail-form.component.html',
  styleUrl: './vehicle-detail-form.component.css',
})
export class VehicleDetailFormComponent {
  constructor(
    public service: VehicleDetailService,
    private toastr: ToastrService
  ) {}

  onSubmit(form: NgForm) {
    //this.service.formSubmitted = true
    if (form.valid) {
      if (this.service.formData.carId == 0) this.insertRecord(form);
      else this.updateRecord(form);
    }
  }

  insertRecord(form: NgForm) {
    this.service.postVehicleDetail().subscribe({
      next: (res) => {
        this.service.list = res as VehicleDetail[];
        this.service.resetForm(form);
        this.toastr.success('Inserted successfully', 'Car Detail Register');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  updateRecord(form: NgForm) {
    this.service.putVehicleDetail().subscribe({
      next: (res) => {
        this.service.list = res as VehicleDetail[];
        this.service.resetForm(form);
        this.toastr.info('updated successfully', 'Car Detail Register');
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
