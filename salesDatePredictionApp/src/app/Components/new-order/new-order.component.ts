import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Employees } from '../../core/models/Employees.Interfaces';
import { Shipper } from '../../core/models/Shippers.Interfaces';
import { Product } from '../../core/models/Product.Interfaces';
import { OrderService } from '../../shared/services/order.service';
import { ToastrService } from 'ngx-toastr';
import { DataService } from '../../shared/services/Data.service';

@Component({
  selector: 'app-new-order',
  standalone: true,
  imports: [CommonModule,HttpClientModule, FormsModule, ReactiveFormsModule],
  templateUrl: './new-order.component.html',
  styleUrl: './new-order.component.css'
})
export class NewOrderComponent implements OnInit {

  formNewOrder: FormGroup;
  isSubmitted = false;

  employees: Employees[] = [];
  shippers: Shipper[] = [];
  products: Product[] = [];

  constructor(
    public dialogRef: MatDialogRef<NewOrderComponent>, 
    @Inject(MAT_DIALOG_DATA) public data: {Custid:number, CustomerName:string} ,
    private http: HttpClient, 
    public dialog: MatDialog,
    private formBuilder: FormBuilder,
    private ordersSvc: OrderService,
    private DataSvc: DataService,
    private toastr: ToastrService
  ) {



    this.formNewOrder = this.formBuilder.group({
      Custid: ['', Validators.required],
      Empid: ['', Validators.required],
      Shipperid: ['', Validators.required],
      Shipname: ['', [Validators.required, Validators.maxLength(100)]],
      Shipaddress: ['', [Validators.required, Validators.maxLength(200)]],
      Shipcity: ['', [Validators.required, Validators.maxLength(100)]],
      Orderdate: ['', Validators.required],
      Requireddate: ['', Validators.required],
      Shippeddate: [''],
      Freight: ['', [Validators.required, Validators.min(0)]],
      Shipcountry: ['', [Validators.required, Validators.maxLength(100)]],
      Productid: ['', Validators.required],
      Unitprice: ['', [Validators.required, Validators.min(0)]],
      Qty: ['', [Validators.required, Validators.min(1)]],
      Discount: ['', [Validators.required, Validators.min(0), Validators.max(100)]],
    });
  }

  ngOnInit(): void 
  {
    this.formNewOrder.patchValue({
      Custid: this.data.Custid
    });
    this.loadEmployees();
    this.loadShippers();
    this.loadProducts();

  }

      private loadEmployees(sortField: string = 'Empid', sortDirection: string = 'ASC') {
  
        this.DataSvc.getAllEmployees(sortField, sortDirection)
                    .subscribe((data) => 
                    {
                      this.employees = data;
                    });
      }



      private loadShippers(sortField: string = 'ShipperId', sortDirection: string = 'ASC') {
  
        this.DataSvc.getAllShippers(sortField, sortDirection)
                    .subscribe((data) => 
                    {
                      this.shippers = data;
                    });
      }


      private loadProducts(sortField: string = 'Productid', sortDirection: string = 'ASC') {
  
        this.DataSvc.getAllProducts(sortField, sortDirection)
                    .subscribe((data) => 
                    {
                      this.products = data;
                    });
      }


      onSubmit() {
        if (this.formNewOrder.valid) {
            this.ordersSvc.addNewOrder(this.formNewOrder.value).subscribe({
              next: (res) =>{
                this.formNewOrder.reset();
                this.toastr.success('A new order was successfully added!', 'Success');
              },
              error: (err) => {
              this.toastr.error('Failed to add order.', 'Error');
              console.log(err);
            },
            });
        }
      }


  close(): void {
    this.dialogRef.close(); 
  }
}
