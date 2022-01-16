import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse
} from '@angular/common/http';

import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { LoginService } from '../Services/Login/login.service';
@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private loginService: LoginService
  ) { }
  intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
    if (!this.loginService.getUserIsLogedIn() && !req.url.includes("login") && !req.url.includes("sign-up")) {
      this.router.navigate(["login-form"]);
    }
    return next.handle(req).pipe(tap(() => { },
      (err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status !== 401) {
            return;
          }
          this.loginService.dropUserIsLogedIn();
          this.router.navigate(['login-form']);
        }
      }));
  }
}

