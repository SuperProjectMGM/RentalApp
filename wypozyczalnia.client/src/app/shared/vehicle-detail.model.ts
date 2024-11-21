export class VehicleDetail {
  carId: string = '';
  brand: string = '';
  model: string = '';
  serialNo: string = '';
  vinId: string = '';
  registryNo: string = '';
  yearOfProduction: number = new Date().getFullYear();
  rentalFrom: Date = new Date();
  rentalTo: Date = new Date();
  prize: number = 0.0;
  driveType: string = '';
  transmission: string = '';
  description: string = '';
  type: string = '';
  rate: number = 0.0;
  localization: string = '';
}
