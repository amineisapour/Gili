import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    constructor(
        private router: Router,
        private accountService: AccountService
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (this.accountService.isAuthenticat()) {
            return true;
        }
        // not logged in so redirect to login page with the return url
        //this.router.navigate(['/auth/login'], { queryParams: { returnUrl: state.url } });
        window.location.href = '/auth/login?returnUrl=' + state.url;
        return false;
    }

}
