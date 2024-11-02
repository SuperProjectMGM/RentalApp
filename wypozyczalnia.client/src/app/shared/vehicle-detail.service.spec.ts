import { TestBed } from '@angular/core/testing';

import { VehicleDetailService } from './vehicle-detail.service';

describe('VehicleDetailService', () => {
  let service: VehicleDetailService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(VehicleDetailService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
