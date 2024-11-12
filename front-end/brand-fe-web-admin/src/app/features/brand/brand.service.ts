import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Brand } from './models/brand';
import { environment } from 'src/environments/environment';
import { CreateBrand } from './models/create-brand';
import { RemoveResult } from 'src/app/shared/models/remove-result';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  constructor(private _http: HttpClient) { }

  list(): Observable<Brand[]> {
    return this._http.get<Brand[]>(`${environment.api}/brands/brands`);
  }

  find(id: number): Observable<Brand> {
    return this._http.get<Brand>(`${environment.api}/brands/brands/${id}`);
  }

  create(brand: CreateBrand): Observable<Brand> {
    return this._http.post<Brand>(`${environment.api}/brands/brands`, brand);
  }

  update(id: number, brand: CreateBrand): Observable<Brand> {
    return this._http.put<Brand>(`${environment.api}/brands/brands/${id}`, brand);
  }

  remove(id: number): Observable<RemoveResult> {
    return this._http.delete<RemoveResult>(`${environment.api}/brands/brands/${id}`);
  }
}
