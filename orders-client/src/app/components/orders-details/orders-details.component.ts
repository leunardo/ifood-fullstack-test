import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Order } from 'src/app/shared/models/order';

@Component({
  selector: 'app-orders-details',
  templateUrl: './orders-details.component.html',
  styleUrls: ['./orders-details.component.scss']
})
export class OrdersDetailsComponent implements OnInit {
  columns = ['description', 'quantity', 'price', 'total'];

  constructor(@Inject(MAT_DIALOG_DATA) public order: Order) { }

  ngOnInit(): void {
  }

}
