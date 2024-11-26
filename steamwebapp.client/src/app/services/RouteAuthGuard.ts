import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    constructor(private http: HttpClient, private router: Router) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
        return this.http.get("https://localhost:7100/api/SteamAuth/TestAuth", { observe: "response", withCredentials: true}).pipe(map((data) => {
            if(data.status == 200) {
                return true;
            } else {
                return false;
            }
        }),
    catchError(err => {
        this.router.navigateByUrl("/");
        return of(false);
    }));
    }
}