import { Component, OnInit } from '@angular/core';
import { VehicleDetailService } from '../../services/vehicle-detail.service';
import { VehicleDetail } from '../../shared/vehicle-detail.model';
import { ToastrService } from 'ngx-toastr';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { VehicleDetailFormComponent } from '../vehicle-detail-form/vehicle-detail-form.component';
import { RentalRequestsComponent } from '../rental-requests/rental-requests.component';
import { ReturnRequestsComponent } from '../return-requests/return-requests.component';
import { MatDialog } from '@angular/material/dialog';
import { VehicleHistoryModalComponent } from '../vehicle-history-modal/vehicle-history-modal.component';

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
    ReturnRequestsComponent,
  ],
})
export class VehiclesDetailsComponent implements OnInit {
  searchTerm: string = '';
  filteredList: VehicleDetail[] = [];
  currentFilter: string = 'all';

  constructor(
    public service: VehicleDetailService,
    private toastr: ToastrService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.service.refreshList();
    setTimeout(() => {
      this.filteredList = this.service.list;
      this.filterList();
    }, 100);
  }

  showAllCars() {
    this.currentFilter = 'all';
    this.filterList();
  }

  showRentedCars() {
    this.service.rentlist = [];
    this.currentFilter = 'rented';
    this.filterList();
  }

  filterList() {
    const term = this.searchTerm.toLowerCase().trim();
    if (term === '') {
      if (this.currentFilter == 'all') {
        this.filteredList = this.service.list;
      } else {
        this.filteredList = this.service.rentlist;
      }
    } else {
      if (this.currentFilter == 'all') {
        this.filteredList = this.service.list.filter((pd) =>
          pd.vin.toLowerCase().startsWith(term)
        );
      } else {
        this.filteredList = this.service.rentlist.filter((pd) =>
          pd.vin.toLowerCase().startsWith(term)
        );
      }
    }
  }

  populateForm(selectedRecord: VehicleDetail) {
    this.service.formData = Object.assign({}, selectedRecord);
    this.service.isEdit = true;
  }

  onDelete(vin: string) {
    if (confirm('Are you sure to delete this Car?'))
      this.service.deleteVehicleDetail(vin).subscribe({
        next: () => {
          this.toastr.error('Deleted successfully', 'Car Detail Register');
          this.service.refreshList();
          this.filteredList = this.service.list;
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

  onShowHistory(vin: string): void {
    this.dialog.open(VehicleHistoryModalComponent, {
      width: '600px',
      data: vin,
    });
  }
}
