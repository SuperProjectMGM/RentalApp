export enum RentalStatus {
  Pending = 1,  
  Confirmed = 2, 
  Completed = 3, 
  WaitingForReturnAcceptance = 4,
  Returned = 5
}

export interface Rental {
  rentalId: string;
  slug: string,
  name: string;
  surname: string;
  personalNumber: string;
  vin: string;
  start: string;
  end: string;
  status: RentalStatus;
  description: string;
  photoUrl:string;
  returnLatitude:string;
  returnLongtitude:string;
  returnClientDescription:string;
}
