import { Component, OnInit } from '@angular/core';
import { VehicleDetailService } from '../shared/vehicle-detail.service';

@Component({
  selector: 'app-vehicles-details',
  templateUrl: './vehicles-details.component.html',
  styleUrl: './vehicles-details.component.css'
})
export class VehiclesDetailsComponent implements OnInit {
  constructor(public service: VehicleDetailService) { }
    ngOnInit(): void {
      this.service.refreshList();
    }
}
