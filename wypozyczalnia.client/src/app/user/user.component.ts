import { Component } from '@angular/core';
import { RegistrationComponent } from './registration/registration.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user',
  standalone: true,
  templateUrl: './user.component.html',
  styleUrl: './user.component.css',
  imports: [CommonModule, FormsModule, RegistrationComponent],
})
export class UserComponent {}
