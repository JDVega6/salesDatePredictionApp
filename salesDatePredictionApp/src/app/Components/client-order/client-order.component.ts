import { AfterViewInit, Component, Inject, OnInit, ViewChild  } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { PredictedOrder } from '../../core/models/PredicttedOrder.interfaces';
import { MatSort, MatSortModule, Sort } from '@angular/material/sort';
import { CommonModule } from '@angular/common';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { ClientOrder } from '../../core/models/ClientOrder.Interfaces';
import { ClientService } from '../../shared/services/client.service';

@Component({
  selector: 'app-client-order',
  standalone: true,
  imports: [
            CommonModule,
            MatPaginatorModule, 
            MatTableModule,
            MatSortModule,
            HttpClientModule,
  ],
  templateUrl: './client-order.component.html',
  styleUrl: './client-order.component.css'
})
export class ClientOrderComponent implements OnInit, AfterViewInit{

    displayedColumns: string[] = ['orderId', 'requiredDate', 'shippedDate', 'shipName', 'shipAddress', 'shipCity'];
    predictedOrders: PredictedOrder[] = [];
    dataSource = new MatTableDataSource<ClientOrder>([]); 
    @ViewChild(MatPaginator) paginator!: MatPaginator; 
    @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private clientSvc: ClientService,
    public dialogRef: MatDialogRef<ClientOrderComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: {Custid:number, CustomerName:string} ,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void 
  {
      this.loadOrders();  
  }

    private loadOrders(sortField: string = 'OrderId', sortDirection: string = 'ASC') {
        this.clientSvc
            .getAllOrderByClientId(this.data.Custid, sortField, sortDirection)
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
  
  close(): void {
    this.dialogRef.close(); 
  }
}
