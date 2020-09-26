import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Order } from '../models/order';
import { OrderSearch } from '../interfaces/order-search';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private httpClient: HttpClient) {

  }

  search(query?: OrderSearch): Observable<Order[]> {
    const params = new HttpParams();
    if (query) {
      params.append('name', query.name);
      params.append('phone', query.phone);
      params.append('email', query.email);
      params.append('startDate', query.startDate.toISOString());
      params.append('endDate', query.endDate.toISOString());
    }

    return this.httpClient.get<Order[]>(`${environment.apiUrl}/orders`, { params });
  }
}
