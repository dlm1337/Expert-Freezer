// auth.guard.ts
import { inject } from '@angular/core';
import { CanActivateFn, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './auth.service';


export const authenticationGuard: CanActivateFn = (route, state) => {

    const authService = inject(AuthService);
    const router = inject(Router);


    if (!authService.isLoggedIn()) {
        console.log("was no auth");

        router.navigate(['/login']);
        return false;
    }

    console.log(authService.getToken());
    console.log("auth found.")
    return true;

};


