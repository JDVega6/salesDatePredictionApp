import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PredictedOrder } from '../../core/models/PredicttedOrder.interfaces';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class OrderService {
  urlBase = `${environment.api}`;
  constructor(private http: HttpClient) { }

  getAllPredictionNextOrders(
    searchQuery: string = '',
    sortField: string = 'CustomerName',
    sortDirection: string = 'ASC'
  ): Observable<PredictedOrder[]>{
    let endpoint = `orders`;
    let query = this.urlBase + endpoint;

    const params = new HttpParams()
      .set('CustomerName', searchQuery)
      .set('orderBy', sortField)
      .set('orderDirection', sortDirection);

    return this.http.get<PredictedOrder[]>(query, { params });
  }

  addNewOrder(orderObj : any){
    return this.http.post<any>(`${this.urlBase}orders/AddOrder`, orderObj)
  }

}
