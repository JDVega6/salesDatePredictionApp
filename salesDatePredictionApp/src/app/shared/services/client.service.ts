import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ClientOrder } from '../../core/models/ClientOrder.Interfaces';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  urlBase = `${environment.api}`;
  constructor(private http: HttpClient) { }


  getAllOrderByClientId(
    clientId: number,
    sortField: string = 'OrderId',
    sortDirection: string = 'ASC'
  ): Observable<ClientOrder[]>{
    let endpoint = `client/`;
    let query = this.urlBase + endpoint + clientId;

    const params = new HttpParams()
      .set('orderBy', sortField)
      .set('orderDirection', sortDirection);

    return this.http.get<ClientOrder[]>(query, { params });
  }


}
