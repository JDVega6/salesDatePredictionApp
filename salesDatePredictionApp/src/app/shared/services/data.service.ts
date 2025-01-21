import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Employees } from '../../core/models/Employees.Interfaces';
import { Observable } from 'rxjs';
import { Shipper } from '../../core/models/Shippers.Interfaces';
import { Product } from '../../core/models/Product.Interfaces';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  urlBase = `${environment.api}`;
  constructor(private http: HttpClient) { }

  getAllEmployees(
    sortField: string = 'Empid',
    sortDirection: string = 'ASC'
  ): Observable<Employees[]> {
    return this.getAllData<Employees>('employees', sortField, sortDirection);
  }
  
  getAllShippers(
    sortField: string = 'ShipperId',
    sortDirection: string = 'ASC'
  ): Observable<Shipper[]> {
    return this.getAllData<Shipper>('shipper', sortField, sortDirection);
  }

  getAllProducts(
    sortField: string = 'Productid',
    sortDirection: string = 'ASC'
  ): Observable<Product[]> {
    return this.getAllData<Product>('products', sortField, sortDirection);
  }
  
  getAllData<T>(
    endpoint: string,
    sortField: string,
    sortDirection: string
  ): Observable<T[]> {
    const query = `${this.urlBase}${endpoint}`;
  
    const params = new HttpParams()
      .set('orderBy', sortField)
      .set('orderDirection', sortDirection);
  
    return this.http.get<T[]>(query, { params });
  }

}
