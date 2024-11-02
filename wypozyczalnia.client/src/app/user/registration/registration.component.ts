import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-registration',
  standalone: true,
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.css',
  imports: [CommonModule, FormsModule],
})
export class RegistrationComponent {}
