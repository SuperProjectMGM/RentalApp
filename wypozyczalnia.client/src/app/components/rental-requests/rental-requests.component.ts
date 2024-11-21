import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RentalService } from '../../services/rental.service';

@Component({
  selector: 'app-rental-requests',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './rental-requests.component.html',
  styleUrls: ['./rental-requests.component.scss'],
})
export class RentalRequestsComponent {
  rentalRequests: any[] = []; // Store rental requests

  constructor(private rentalService: RentalService) {}

  // Fetch rental requests
  refreshRequests() {
    this.rentalService.getRentalRequests().subscribe({
      next: (data) => {
        console.log('Fetched rental requests:', data);
        this.rentalRequests = data;
      },
      error: (error) => {
        console.error('Error fetching rental requests:', error);
      },
    });
  }

  // Approve a rental request
  approveRequest(id: number) {
    this.rentalService.approveRentalRequest(id).subscribe({
      next: () => {
        console.log(`Rental request with ID ${id} approved.`);
        this.refreshRequests();
      },
      error: (error) => {
        console.error(`Error approving request with ID ${id}:`, error);
      },
    });
  }

  // Reject a rental request
  rejectRequest(id: number) {
    this.rentalService.rejectRentalRequest(id).subscribe({
      next: () => {
        console.log(`Rental request with ID ${id} rejected.`);
        this.refreshRequests();
      },
      error: (error) => {
        console.error(`Error rejecting request with ID ${id}:`, error);
      },
    });
  }
}
