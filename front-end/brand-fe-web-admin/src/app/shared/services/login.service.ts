import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { User } from '../models/user';
import { Login } from '../models/login';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  @Output() onLoginChange: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private _http: HttpClient,
    private _router: Router
  ) { }

  login(login: Login): Observable<User> {
    return this._http.post<User>(`${environment.api}/auth/users/login`, login).pipe(
      tap((user: User) => {
        localStorage.setItem('brand_user', JSON.stringify(user));
        this.onLoginChange.emit(true);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('brand_user');

    this.onLoginChange.emit(false);

    this._router.navigate(['/login']);
  }

  getUser(): User | null {
    let user = localStorage.getItem('brand_user');

    if (!user)
      return null;

    return JSON.parse(user);
  }

  isLoggedIn(): boolean {
    const user = this.getUser();

    if (!user)
      return false;

    return new Date(user.tokenExpiration) >= new Date();
  }

  canActivate(): boolean {
    return this.isLoggedIn();
  }
}
