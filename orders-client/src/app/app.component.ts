import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { Order } from './shared/models/order';
import { OrderService } from './shared/services/order.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  orders$ = this.orderService.search();
  form: FormGroup;


  constructor(
    private orderService: OrderService,
    private fb: FormBuilder
  ) {

  }
  ngOnInit(): void {
    this.setupForm();
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
