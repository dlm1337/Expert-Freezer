// auth.service.ts
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private tokenKey = 'authToken';

    constructor(private router: Router) { }

    setToken(token: string): void {
        localStorage.setItem(this.tokenKey, token);
    }

    getToken(): string | null {
        return localStorage.getItem(this.tokenKey);
    }

    removeToken(): void {
        localStorage.removeItem(this.tokenKey);
    }

    isLoggedIn(): boolean {
        return !!this.getToken();
    }

    logout(): void {
        this.removeToken();
        this.router.navigate(['/login']);
    }
}