import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Product, ProductList } from '../../models/product';
import { catchError, map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  _headers = {
		headers: new HttpHeaders({ 'Content-Type': 'application/json' })
	};

	_headers_ = new HttpHeaders({ 'Content-Type': 'application/json' });
  private endPoint: string = environment.API_URL;
  private api:string = this.endPoint +'api/';

  constructor(public httpClient: HttpClient) { }

  getProducts():Observable<ProductList>{
    return this.httpClient
			.get<ProductList>(`${this.api}Products`)
			;
  }

  getProductsById(id:number):Observable<Product>{
    return this.httpClient
    .get<Product>(`${this.api}Products`,{
      ...this._headers,
      params: { id: id }
    }
  	)	;
  }

  postProductCreate(product: Product): Observable<Product> {
		return this.httpClient.post<any>(`${this.api}Products/`, product, this._headers).pipe(
			map((data) => data)

		);
	}

  putUpdateProduct(id:number, product: Product): Observable<Product> {
    return this.httpClient
    .put<Product>(`${this.api}Products/${id}`, product, {
      ...this._headers
    })
	}

  deldeleteProduct(id: number): Observable<void> {
		return this.httpClient
    .delete<void>(`${this.api}Products/${id}`, {
      ...this._headers
    })
	}
}
