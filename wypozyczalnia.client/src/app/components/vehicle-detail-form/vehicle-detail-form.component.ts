import { Component, EventEmitter, Output } from '@angular/core';
import { VehicleDetailService } from '../../services/vehicle-detail.service';
import { FormsModule, NgForm } from '@angular/forms';
import { VehicleDetail } from '../../shared/vehicle-detail.model';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-vehicle-detail-form',
  standalone: true,
  templateUrl: './vehicle-detail-form.component.html',
  styleUrl: './vehicle-detail-form.component.css',
  imports: [CommonModule, FormsModule],
})
export class VehicleDetailFormComponent {
  @Output() vehicleUpdated = new EventEmitter<void>();

  constructor(
    public service: VehicleDetailService,
    private toastr: ToastrService
  ) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      if (!this.service.isEdit)
      { 
        this.insertRecord(form);
      }
      else 
      {
        this.updateRecord(form);
        this.service.isEdit = false;
      }
    }
  }

  insertRecord(form: NgForm) {
    this.service.postVehicleDetail().subscribe({
      next: () => {
        this.service.resetForm(form);
        this.toastr.success('Inserted successfully', 'Car Detail Register');
        this.vehicleUpdated.emit();
        this.service.refreshList(); 
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  updateRecord(form: NgForm) {
    this.service.putVehicleDetail().subscribe({
      next: () => {
        this.service.resetForm(form);
        this.toastr.info('updated successfully', 'Car Detail Register');
        this.vehicleUpdated.emit();
        this.service.refreshList();
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onPhotoSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      console.log('Selected file:', file);
      // TODO: Możesz tutaj zapisać plik do zmiennej lub wysłać do serwera
    }
  }
}
