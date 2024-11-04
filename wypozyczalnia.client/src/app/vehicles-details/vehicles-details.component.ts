import { Component, OnInit } from '@angular/core';
import { VehicleDetailService } from '../shared/services/vehicle-detail.service';
import { VehicleDetail } from '../shared/vehicle-detail.model';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { VehicleDetailFormComponent } from '../vehicle-detail-form/vehicle-detail-form.component';

@Component({
  selector: 'app-vehicles-details',
  standalone: true,
  templateUrl: './vehicles-details.component.html',
  styleUrl: './vehicles-details.component.css',
  imports: [CommonModule, FormsModule, VehicleDetailFormComponent],
})
export class VehiclesDetailsComponent implements OnInit {
  constructor(
    public service: VehicleDetailService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.service.refreshList();
  }

  populateForm(selectedRecord: VehicleDetail) {
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number) {
    if (confirm('Are you sure to delete this Car?'))
      this.service.deleteVehicleDetail(id).subscribe({
        next: (res) => {
          this.service.list = res as VehicleDetail[];
          this.toastr.error('Deleted successfully', 'Car Detail Register');
        },
        error: (err) => {
          console.log(err);
        },
      });
  }
}
