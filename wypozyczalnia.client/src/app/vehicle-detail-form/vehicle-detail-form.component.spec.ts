import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VehicleDetailFormComponent } from './vehicle-detail-form.component';

describe('VehicleDetailFormComponent', () => {
  let component: VehicleDetailFormComponent;
  let fixture: ComponentFixture<VehicleDetailFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [VehicleDetailFormComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(VehicleDetailFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
