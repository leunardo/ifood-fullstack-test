export interface OrderSearch {
  name: string;
  phone: string;
  email: string;
  startDate: Date;
  endDate: Date;
  [value: string]: any;
}
