import { Component, EventEmitter, Output } from '@angular/core';
import { VehicleDetailService } from '../../services/vehicle-detail.service';
import { FormsModule, NgForm } from '@angular/forms';
import { VehicleDetail } from '../../shared/vehicle-detail.model';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { AzureBlobStorageService } from '../../services/azure-blob-storage.service';

@Component({
  selector: 'app-vehicle-detail-form',
  standalone: true,
  templateUrl: './vehicle-detail-form.component.html',
  styleUrl: './vehicle-detail-form.component.css',
  imports: [CommonModule, FormsModule],
})
export class VehicleDetailFormComponent {
  @Output() vehicleUpdated = new EventEmitter<void>();
  selectedPhoto: File | null = null; // Przechowuje wybrane zdjęcie
  photoPreviewUrl: string | null = null; // For the image preview


  BaseUrl = environment.apiBaseUrl;

  constructor(
    public service: VehicleDetailService,
    private toastr: ToastrService,
    private http: HttpClient,
    private blobService: AzureBlobStorageService
  ) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
      if (!this.service.isEdit)
      { 
        this.uploadPhotoAndSubmit(form, "vehicles");
      }
      else 
      {
        this.updateRecord(form);
        this.service.isEdit = false;
      }
    }
  }

  private uploadPhotoAndSubmit(form: NgForm, folder: string): void {
    // Krok 1: Pobierz link do przesyłania zdjęcia
    this.http.get<{ sasUrl: string }>(`${this.BaseUrl}/Storage/vehicles`).subscribe({
      next: (response) => {
        const uploadUrl = response.sasUrl;
        // Krok 2: Wyślij zdjęcie do Azure Blob Storage
        if (this.selectedPhoto) {
          this.blobService.uploadImage(uploadUrl, folder, this.selectedPhoto, this.selectedPhoto.name, this.service.refreshList)
          this.service.formData.photoUrl = `${uploadUrl.split('?')[0]}/${folder}/${this.selectedPhoto.name}`;
          this.insertRecord(form)
          this.selectedPhoto = null
          this.photoPreviewUrl = null
          }
        },
      error: (err) => {
        console.error('Błąd pobierania linku do przesłania:', err);
        this.toastr.error('Nie można uzyskać linku do przesłania zdjęcia');
  }});
    };
  

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
      this.selectedPhoto = input.files[0]; // Store selected photo
      const reader = new FileReader();
      reader.onload = () => {
        this.photoPreviewUrl = reader.result as string; // Generate image preview
      };
      reader.readAsDataURL(this.selectedPhoto);
      console.log('Selected photo:', this.selectedPhoto);
    }
  }
}
