import { Component, OnInit } from '@angular/core';
import { VehicleDetailService } from '../../services/vehicle-detail.service';
import { VehicleDetail } from '../../shared/vehicle-detail.model';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { VehicleDetailFormComponent } from '../vehicle-detail-form/vehicle-detail-form.component';
import { RentalRequestsComponent } from '../rental-requests/rental-requests.component';

@Component({
  selector: 'app-vehicles-details',
  standalone: true,
  templateUrl: './vehicles-details.component.html',
  styleUrl: './vehicles-details.component.css',
  imports: [
    CommonModule,
    FormsModule,
    VehicleDetailFormComponent,
    RentalRequestsComponent,
  ],
})
export class VehiclesDetailsComponent implements OnInit {
  searchTerm: string = '';
  filteredList: VehicleDetail[] = [];

  constructor(
    public service: VehicleDetailService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.service.refreshList();
    setTimeout(() => {
      this.filteredList = this.service.list; // Ustaw pełną listę po odświeżeniu
    }, 100); // Opóźnienie, aby upewnić się, że dane są załadowane
  }

  filterList() {
    const term = this.searchTerm.toLowerCase().trim();
    if (term === '') {
      this.filteredList = this.service.list;
    } else {
      this.filteredList = this.service.list.filter((pd) =>
        pd.vinId.toLowerCase().startsWith(term)
      );
    }
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
          this.filterList();
        },
        error: (err) => {
          console.log(err);
        },
      });
  }

  onVehicleUpdated() {
    this.filterList();
  }
}
