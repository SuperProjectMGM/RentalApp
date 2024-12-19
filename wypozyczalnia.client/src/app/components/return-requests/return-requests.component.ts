import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { Rental } from '../../shared/rental-requests.model';
import { ReturnService } from '../../services/return.service';

@Component({
  selector: 'app-return-requests',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './return-requests.component.html',
  styleUrls: ['./return-requests.component.scss'],
})
export class ReturnRequestsComponent implements OnInit {
  returnRequests: Rental[] = []; // Store return requests

  constructor(private returnService: ReturnService) {}

  ngOnInit(): void {
    this.refreshRequests();
  }

  // Fetch return requests
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

  // Approve a return request
  approveRequest(id: string) {
    this.returnService.approveReturnRequest(id).subscribe({
      next: () => {
        console.log(`Return request with ID ${id} approved.`);
        this.refreshRequests();
      },
      error: (error) => {
        console.error(`Error approving return request with ID ${id}:`, error);
      },
    });
  }

  // Reject a return request
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
