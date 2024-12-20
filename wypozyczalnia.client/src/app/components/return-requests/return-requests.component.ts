import { Component, OnInit } from '@angular/core';
import { Rental } from '../../shared/rental-requests.model';
import { ReturnService } from '../../services/return.service';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-return-requests',
  standalone: true,
  imports: [CommonModule, HttpClientModule, FormsModule],
  templateUrl: './return-requests.component.html',
  styleUrls: ['./return-requests.component.scss'],
})
export class ReturnRequestsComponent implements OnInit {
  returnRequests: Rental[] = [];
  isApproveModalVisible = false;
  selectedRequest: Rental | null = null;

  approveData = {
    description: '',
    image: null as File | null,
  };

  constructor(private returnService: ReturnService) {}

  ngOnInit(): void {
    this.refreshRequests();
  }

  refreshRequests() {
    this.returnService.getReturnRequests().subscribe({
      next: (data) => {
        console.log('Fetched return requests:', data);
        this.returnRequests = data;
      },
      error: (error) => {
        console.error('Error fetching return requests:', error);
      },
    });
  }

  openApproveModal(request: Rental): void {
    this.selectedRequest = request;
    this.isApproveModalVisible = true;
  }

  closeApproveModal(): void {
    this.isApproveModalVisible = false;
    this.selectedRequest = null;
    this.approveData = { description: '', image: null };
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.approveData.image = input.files[0];
    }
  }

  confirmApprove(): void {
    if (this.selectedRequest) {
      const formData = new FormData();

      // formData.append('rentalId', this.selectedRequest.rentalId.toString());
      // formData.append('description', this.approveData.description);
      // if (this.approveData.image) {
      //   formData.append('image', this.approveData.image);
      // }
      this.returnService
        .approveReturnRequest(this.selectedRequest.rentalId)
        .subscribe({
          next: () => {
            console.log(
              `Approved request for rentalId ${this.selectedRequest?.rentalId}`
            );
            this.refreshRequests();
            this.closeApproveModal();
          },
          error: (error) => {
            console.error('Error approving request:', error);
          },
        });
    }
    this.refreshRequests();
    this.closeApproveModal();
  }

  rejectRequest(id: number) {
    this.returnService.rejectReturnRequest(id).subscribe({
      next: () => {
        console.log(`Return request with ID ${id} rejected.`);
        this.refreshRequests();
      },
      error: (error) => {
        console.error(`Error rejecting return request with ID ${id}:`, error);
      },
    });
  }
}
