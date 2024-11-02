import { Component, OnInit } from '@angular/core';
import { VehicleDetailService } from '../shared/vehicle-detail.service';
import { VehicleDetail } from '../shared/vehicle-detail.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vehicles-details',
  templateUrl: './vehicles-details.component.html',
  styleUrl: './vehicles-details.component.css',
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
