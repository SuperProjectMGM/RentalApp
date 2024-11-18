import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../shared/services/auth.service'; // Zmiana na poprawny import
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = ''; // Zmienna na komunikaty błędów
  isLoading: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.isLoading = true;
    this.authService.login(this.email, this.password).subscribe({
      next: (response) => {
        localStorage.setItem('loggedIn', 'true'); // Ustaw flagę zalogowania
        this.router.navigate(['/protected']); // Przekierowanie do chronionej strony
        this.isLoading = false;
      },
      error: (err) => {
        console.error('Login failed', err);
        this.errorMessage = 'Login failed. Please try again.';
      },
    });
  }
}
