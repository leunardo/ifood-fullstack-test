import { Client } from './client';
import { Item } from './item';

export class Order {
  id: string;
  date?: Date;
  client: Client;
  totalValue: number;
  items?: Item[];
}
