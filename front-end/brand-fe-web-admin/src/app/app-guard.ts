import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { LoginService } from "./shared/services/login.service";

export const AppGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {

    const loginService = inject(LoginService);
    const canActivate = loginService.canActivate();

    if (!canActivate)
        loginService.logout();

    return canActivate;
}