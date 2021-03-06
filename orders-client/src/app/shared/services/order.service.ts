import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Order } from '../models/order';
import { OrderSearch } from '../interfaces/order-search';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private ordersSubject = new Subject<Order[]>();
  private fullOrderSubject = new Subject<string>();

  public orders$ = this.ordersSubject.asObservable();
  public fullOrder$ = this.fullOrderSubject.asObservable();

  constructor(private httpClient: HttpClient) { }

  search(query?: OrderSearch): void {
    const params = this.getParams(query);

    this.httpClient.get<Order[]>(`${environment.apiUrl}/orders`, { params })
      .subscribe(orders => {
        this.ordersSubject.next(orders);
      });
  }

  populate(): Observable<void> {
    return this.httpClient.get<void>(`${environment.apiUrl}/orders/populate`);
  }

  openFullOrder(id: string): void {
    this.fullOrderSubject.next(id);
  }

  findById(id: string): Observable<Order> {
    return this.httpClient.get<Order>(`${environment.apiUrl}/orders/${id}`);
  }

  private getParams(query: OrderSearch): HttpParams {
    let params = new HttpParams();
    if (!query) {
      return undefined;
    }

    Object.entries(query).forEach(([key, value]) => {
      if (value instanceof Date) {
        params = params.append(key, value.toISOString());
      } else if (Boolean(value)) {
        params = params.append(key, value);
      }
    });

    return params;
  }
}
