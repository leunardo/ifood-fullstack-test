import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { OrderSearch } from 'src/app/shared/interfaces/order-search';
import { OrderService } from 'src/app/shared/services/order.service';

@Component({
  selector: 'app-orders-search',
  templateUrl: './orders-search.component.html',
  styleUrls: ['./orders-search.component.scss']
})
export class OrdersSearchComponent implements OnInit {

  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private orderService: OrderService
  ) {

  }
  ngOnInit(): void {
    this.setupForm();
  }

  search(): void {
    const params: OrderSearch = this.form.value;
    this.orderService.search(params);
  }

  private setupForm(): void {
    this.form = this.fb.group({
      name: [],
      phone: [],
      email: [],
      startDate: [],
      endDate: []
    });
  }

}
