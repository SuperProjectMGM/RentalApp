import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Rental } from '../shared/rental-requests.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ReturnService {
  private readonly apiBaseUrl = environment.apiBaseUrl + "/Rental"; // Replace with your API URL
  

  constructor(private http: HttpClient) {}

  getReturnRequests(): Observable<any[]> {
    return this.http.get<Rental[]>(`${this.apiBaseUrl}/pending-rentals-to-return`);
  }

  approveReturnRequest(id: string): Observable<void> {
    return this.http.put<void>(
      `${this.apiBaseUrl}/accept-pending-rental-to-return/${id}`,
      null
    );
  }

  rejectReturnRequest(id: number): Observable<void> {
    //TODO: zrobienie odmowy
    return this.http.put<void>(`${this.apiBaseUrl}/Rental/reject-return/${id}`, {});
  }
}
