import { HttpClientModule } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { PredictedOrder } from '../../core/models/PredicttedOrder.interfaces';
import { MatPaginator } from '@angular/material/paginator';  
import { MatTableModule } from '@angular/material/table';   
import { MatPaginatorModule } from '@angular/material/paginator'; 
import { MatTableDataSource } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { MatDialog } from '@angular/material/dialog';
import { FormSearchComponent } from '../../shared/components/form-search/form-search.component';
import { ActivatedRoute } from '@angular/router';
import { ClientOrderComponent } from '../client-order/client-order.component';
import { NewOrderComponent } from '../new-order/new-order.component';
import { OrderService } from '../../shared/services/order.service';

@Component({
  selector: 'app-date-prediction',
  standalone: true,
  imports: [
              CommonModule,
              MatPaginatorModule, 
              MatTableModule,
              MatSortModule,
              HttpClientModule,
              FormSearchComponent, 
              ClientOrderComponent,
              NewOrderComponent
            ],
  templateUrl: './date-prediction.component.html',
  styleUrl: './date-prediction.component.css'
})

export class DatePredictionComponent implements OnInit, AfterViewInit {

  displayedColumns: string[] = ['customerName', 'lastOrderDate', 'nextPredictedOrder', 'actions'];
  
  dataSource = new MatTableDataSource<PredictedOrder>([]); 
  @ViewChild(MatPaginator) paginator!: MatPaginator; 
  @ViewChild(MatSort) sort!: MatSort;

  private searchQuery : string = '';
  predictedOrders: PredictedOrder[] = [];

  constructor(private ordersSvc: OrderService, private router: ActivatedRoute,public dialog: MatDialog) 
  {}

  ngOnInit(): void 
  {
    this.router.params.subscribe(params => {
      this.searchQuery = params['query'] || "";  
      this.loadOrders();  
    });
  }

  private loadOrders(sortField: string = 'CustomerName', sortDirection: string = 'ASC') {

    this.ordersSvc
    .getAllPredictionNextOrders(this.searchQuery, sortField, sortDirection)
    .subscribe((data) => {
      this.dataSource.data = data;
    });
    }
    
  sortData(sort: Sort) 
  {
    const sortField = this.sort.active;
    const sortDirection = this.sort.direction;
    this.loadOrders(sortField, sortDirection);
  }

  ngAfterViewInit(): void 
  {
    this.dataSource.paginator = this.paginator; 
    this.dataSource.sort = this.sort;
  }

  viewOrders(custid: number, customerName: string): void {

    const modalData = {
      Custid: custid,
      CustomerName: customerName
    };

    const dialogRef = this.dialog.open(ClientOrderComponent, {
      width: '1250px', 
      data:  modalData 
    });
  }

  newOrder(custid: number, customerName: string): void {
    const modalData = {
      Custid: custid,
      CustomerName: customerName
    };

    const dialogRef = this.dialog.open(NewOrderComponent, {
      width: '100', 
      data:  modalData 
    });
    
  }

}
