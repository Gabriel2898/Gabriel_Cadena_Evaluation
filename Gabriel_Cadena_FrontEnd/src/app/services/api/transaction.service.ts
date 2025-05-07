import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from '../../../environments/environment.prod';
import { ProductList, Product } from '../../models/product';
import { Transaction, TransactionList } from '../../models/transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  _headers = {
		headers: new HttpHeaders({ 'Content-Type': 'application/json' })
	};

	_headers_ = new HttpHeaders({ 'Content-Type': 'application/json' });
  private endPoint: string = environment.API_URL;
  private api:string = this.endPoint +'api/';

  constructor(public httpClient: HttpClient) { }

  getTransaction():Observable<TransactionList>{
    return this.httpClient
			.get<TransactionList>(`${this.api}Transactions`)
			;
  }

  postTransactionCreate(product: Transaction): Observable<Transaction> {
		return this.httpClient.post<any>(`${this.api}Transactions/`, product, this._headers).pipe(
			map((data) => data)

		);
	}

}
