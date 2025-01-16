import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from "../../environments/environment";
import {Rental} from "../shared/rental-requests.model";

@Injectable({
  providedIn: 'root',
})
export class RentalService {
  private readonly apiBaseUrl = environment.apiBaseUrl + "/Rental";

  constructor(private http: HttpClient) {}

  getRentalRequests(): Observable<any[]> {
    return this.http.get<Rental[]>(`${this.apiBaseUrl}/pending-rentals`);
  }

  approveRentalRequest(id: string): Observable<void> {
      return this.http.put<void>(
      `${this.apiBaseUrl}/accept-rental/${id}`,
      null
    );
  }

  rejectRentalRequest(id: number): Observable<void> {
    return this.http.put<void>(
      `${this.apiBaseUrl}/rental-requests/${id}/reject`,
      {}
    );
  }
}
