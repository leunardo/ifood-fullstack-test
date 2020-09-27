import { Component, Input, OnInit } from '@angular/core';
import { Order } from 'src/app/shared/models/order';
import { OrderService } from 'src/app/shared/services/order.service';

@Component({
  selector: 'app-orders-table',
  templateUrl: './orders-table.component.html',
  styleUrls: ['./orders-table.component.scss']
})
export class OrdersTableComponent implements OnInit {

  @Input() orders: Order[] = [];

  columns = ['date', 'name', 'phone', 'email', 'total-value', 'actions'];

  constructor(private orderService: OrderService) { }

  ngOnInit(): void {
  }

  showFullOrderClick(order: Order): void {
    this.orderService.openFullOrder(order.id);
  }

}
