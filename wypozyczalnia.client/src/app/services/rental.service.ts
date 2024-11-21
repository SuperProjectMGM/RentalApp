import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root', // This makes the service available application-wide
})
export class RentalService {
  private readonly apiBaseUrl = 'https://your-api-endpoint.com'; // Replace with your API URL

  constructor(private http: HttpClient) {}

  // Fetch rental requests (GET request)
  getRentalRequests(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiBaseUrl}/rental-requests`);
  }

  // Approve rental request (PUT request)
  approveRentalRequest(id: number): Observable<void> {
    return this.http.put<void>(
      `${this.apiBaseUrl}/rental-requests/${id}/approve`,
      {}
    );
  }

  // Reject rental request (PUT request)
  rejectRentalRequest(id: number): Observable<void> {
    return this.http.put<void>(
      `${this.apiBaseUrl}/rental-requests/${id}/reject`,
      {}
    );
  }
}
