//TODO: zrobić model requesta i podmienic w komponencie z any
export enum RentalStatus {
  Pending = 1,    // Rental request is pending
  Confirmed = 2,  // Rental has been confirmed
  Completed = 3,  // Rental has been completed
}

export interface Rental {
  rentalId: string;
  name: string;
  surname: string;
  birthDate: Date;
  dateOfReceiptOfDrivingLicense: Date;
  personalNumber: number;
  licenceNumber: number;
  address: string;
  phoneNumber: string;
  vinId: string;
  start: Date;
  end: Date;
  status: RentalStatus;
  description: string;
}
