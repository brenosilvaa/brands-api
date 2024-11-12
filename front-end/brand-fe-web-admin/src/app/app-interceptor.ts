import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable, tap } from "rxjs";
import { LoginService } from "./shared/services/login.service";
import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { Router } from "@angular/router";

@Injectable()
export class AppInterceptor implements HttpInterceptor {

    constructor(private _loginService: LoginService, private _snackbar: MatSnackBar) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this._loginService.isLoggedIn()) {
            req = req.clone({
                headers: req.headers.set('Authorization', `Bearer ${this._loginService.getUser()?.token}`)
            });
        }

        return next.handle(req)
            .pipe(
                tap({
                    next: (event: HttpEvent<any>) => { },
                    error: (err: HttpErrorResponse) => {

                        let message = err.message;

                        if (err.error) {
                            message = err.error.message ?? err.error.Message;
                        }

                        if (err instanceof HttpErrorResponse) {
                            if (!navigator.onLine) {
                                this._snackbar.open('Sem conexão com a internet.');
                            } else {
                                switch (err.status) {
                                    case 400:
                                        this._snackbar.open(message || 'Má requisição.');
                                        break;
                                    case 401:
                                        this._snackbar.open(message || 'Não autenticado.');

                                        this._loginService.logout();
                                        break;
                                    case 403:
                                        this._snackbar.open(message || 'Não autorizado.');
                                        break;
                                    case 404:
                                        this._snackbar.open(message || 'Não encontrado.');
                                        break;
                                    case 500:
                                        this._snackbar.open(message || 'Erro interno do servidor.');
                                        break;
                                    default:
                                        this._snackbar.open('Não foi possível se conectar com o servidor.');
                                }
                            }
                        } else {
                            this._snackbar.open('Erro interno do Angular.');
                        }
                    }, complete: () => { }
                })
            );
    }

}