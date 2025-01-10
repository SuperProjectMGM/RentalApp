import { Component, Inject, OnInit } from '@angular/core';
import { VehicleDetailService } from '../../services/vehicle-detail.service';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { Rental } from '../../shared/rental-requests.model';

@Component({
  selector: 'app-vehicle-history-modal',
  standalone: true,
  imports: [CommonModule, MatDialogModule],
  templateUrl: './vehicle-history-modal.component.html',
  styleUrl: './vehicle-history-modal.component.css',
})
export class VehicleHistoryModalComponent implements OnInit {
  rentalHistory: Rental[] = []; // Lista historii wynajmu

  constructor(
    @Inject(MAT_DIALOG_DATA) public vin: string,
    private dialogRef: MatDialogRef<VehicleHistoryModalComponent>,
    private service: VehicleDetailService
  ) {}

  ngOnInit(): void {
    this.service.getRentalHistory(this.vin).subscribe({
      next: (data) => {
        this.rentalHistory = data;
        console.log(this.rentalHistory);
      },
      error: (err) => {
        console.error('Failed to load rental history:', err);
      },
    });
  }

  close(): void {
    this.dialogRef.close();
  }
}
