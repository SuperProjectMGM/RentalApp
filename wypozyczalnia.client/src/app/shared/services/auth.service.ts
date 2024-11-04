import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
<<<<<<< HEAD
import { environment } from '../../../environments/environment';
=======
>>>>>>> c92627f151216cb797147692099547e94ef6b892

@Injectable({
  providedIn: 'root',
})
export class AuthService {
<<<<<<< HEAD
  private apiUrl = environment.apiBaseUrl;
=======
  private apiUrl = 'http://localhost:5076/api/Auth';
>>>>>>> c92627f151216cb797147692099547e94ef6b892

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    return this.http
      .post<any>(
<<<<<<< HEAD
        `${this.apiUrl}/Auth/login`,
=======
        `${this.apiUrl}/login`,
>>>>>>> c92627f151216cb797147692099547e94ef6b892
        { email, password },
        { withCredentials: true }
      )
      .pipe(
        tap((response) => {
          if (response && response.token) {
            localStorage.setItem('loggedIn', 'true'); // Ustawienie flagi logowania
            console.log('ustawiono falge');
          }
        })
      );
  }

  logout(): Observable<any> {
    return this.http
      .post(`${this.apiUrl}/logout`, {}, { withCredentials: true })
      .pipe(
        tap(() => {
          localStorage.removeItem('loggedIn'); // Usunięcie flagi logowania
        })
      );
  }

  isAuthenticated(): boolean {
    // Tu możesz w przyszłości dodać logikę sprawdzającą sesję
    if (localStorage.getItem('loggedIn') == 'true') {
      return true;
    }
    return false;
  }
}
