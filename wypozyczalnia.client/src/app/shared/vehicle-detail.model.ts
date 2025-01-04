export class VehicleDetail {
  brand: string = '';
  model: string = '';
  serialNo: string = '';
  vin: string = '';
  registryNo: string = '';
  yearOfProduction: number = new Date().getFullYear();
  price: number = 0.0;
  driveType: string = '';
  transmission: string = '';
  description: string = '';
  type: string = '';
  rate: number = 0.0;
  localization: string = '';
  photoUrl: string = '';
}
