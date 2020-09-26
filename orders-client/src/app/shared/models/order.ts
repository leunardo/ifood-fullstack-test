import { Client } from './client';

export class Order {
  id: string;
  date: Date;
  client: Client;
  totalValue: number;
}
