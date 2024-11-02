export class VehicleDetail {
  carId: number = 0;
  brand: string = '';
  model: string = '';
  serialNo: string = '';
  vinId: string = '';
  registryNo: string = '';
  yearOfProduction: string = ''; // Można również użyć `number` jeśli potrzebujesz tylko roku
  rentalFrom: string = ''; // Zakładam, że data jest przechowywana w formacie string
  rentalTo: string = ''; // Zakładam, że data jest przechowywana w formacie string
  description: string = '';
  type: string = '';
}
