import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, switchMap, throwError } from 'rxjs';
import { Rental } from '../shared/rental-requests.model';
import { environment } from '../../environments/environment';
import { NgForm } from '@angular/forms';
import { AzureBlobStorageService } from './azure-blob-storage.service';
import { ToastrModule } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class ReturnService {
  private readonly apiBaseUrl = environment.apiBaseUrl; 
  

  constructor(private http: HttpClient, private blobService: AzureBlobStorageService,private toastr: ToastrModule ) {}

  getReturnRequests(): Observable<any[]> {
    return this.http.get<Rental[]>(`${this.apiBaseUrl}/Rental/pending-rentals-to-return`);
  }

  approveReturnRequest(id: string, photo: File | null){
    // Step 1: Get SAS URL
    this.http.get<{ sasUrl: string }>(`${this.apiBaseUrl}/Storage/rentals`).subscribe({
        next: (response) => {
            const uploadUrl = response.sasUrl;
            console.log('SAS URL:', uploadUrl);

            // Step 2: Upload the image
            this.blobService.uploadImage(uploadUrl, 'rentalstmp', photo!, photo!.name, () => {});
                // Step 3: Approve the return after the image upload completes
                const url = `${uploadUrl.split('?')[0]}/rentalstmp/${photo!.name}`;
                console.log('Uploaded file URL:', url);

                // Step 4: Make PUT request to approve the rental return
                return this.http.put<void>(
                    `${this.apiBaseUrl}/accept-pending-rental-to-return/${id}?photoUrl=${url}`,
                    null
                ).subscribe({
                    next: () => {
                        console.log('Return request approved successfully!');
                    },
                    error: (err) => {
                        console.error('Error during PUT request:', err);
                    }
                
            });
        },
        error: (err) => {
            console.error('Error during SAS URL fetch:', err);
        }
    });;
}

  rejectReturnRequest(id: number): Observable<void> {
    //TODO: zrobienie odmowy
    return this.http.put<void>(`${this.apiBaseUrl}/Rental/reject-return/${id}`, {});
  }
}
