import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, tap, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {
  constructor(private router: Router) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        //console.log('Request: ', request);
        return next.handle(request).pipe(
            tap((event: HttpEvent<any>) => {
                //console.log('Success response: ', event);
            }),
            catchError((error: HttpErrorResponse) => {
                if(error.status == 401) {
                    this.router.navigateByUrl("/");
                }
                //console.error('Error response:', error);
                return throwError(() => new Error(error.message));
            })
        );
    }
}