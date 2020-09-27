import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { OrdersDetailsComponent } from './components/orders-details/orders-details.component';
import { Order } from './shared/models/order';
import { OrderService } from './shared/services/order.service';
import { mergeMap } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  orders$ = this.orderService.orders$;
  form: FormGroup;

  private fullOrderSubscription: Subscription;

  constructor(
    private orderService: OrderService,
    private fb: FormBuilder,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.setupForm();
    this.orderService.search();

    this.orderService.fullOrder$
      .pipe(mergeMap(id => this.orderService.findById(id)))
      .subscribe(order => this.openOrderDetails(order));
  }

  ngOnDestroy(): void {
    if (this.fullOrderSubscription !== null) {
      this.fullOrderSubscription.unsubscribe();
      this.fullOrderSubscription = null;
    }
  }

  populate(): void {
    this.orderService.populate().subscribe(() => {
      this.orderService.search();
    });
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

  private openOrderDetails(order: Order): void {
    this.dialog.open(OrdersDetailsComponent, {
      width: '750px',
      data: order
    });
  }
}
