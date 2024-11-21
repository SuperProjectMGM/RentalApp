import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from "../../environments/environment";
import {Rental} from "../shared/rental-requests.model";

@Injectable({
  providedIn: 'root', // This makes the service available application-wide
})
export class RentalService {
  private readonly apiBaseUrl = environment.apiBaseUrl + "/Rental"; // Replace with your API URL

  constructor(private http: HttpClient) {}

  // Fetch rental requests (GET request)
  getRentalRequests(): Observable<any[]> {
    return this.http.get<Rental[]>(`${this.apiBaseUrl}/pending-rentals`);
  }

  // Approve rental request (PUT request)
  approveRentalRequest(id: string): Observable<void> {
      return this.http.put<void>(
      `${this.apiBaseUrl}/accept-rental/${id}`,
      null
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
