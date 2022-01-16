import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { LoginResponse } from '../../models/LoginResponse';
import { CanActivate, Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class LoginService implements CanActivate  {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
  }
  public login(login: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.baseUrl + 'Login/SignIn',
      {
        UserName: login,
        Password: password
      });
  }
  public signUp(login: string, password: string): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.baseUrl + 'Login/SignUp',
      {
        UserName: login,
        Password: password
      });
  }
  public Logout():void {
    this.http.post<LoginResponse>(this.baseUrl + 'Login/Logout',
      {
      }).subscribe();
    this.dropUserIsLogedIn();
  }
  public setUserIsLogedIn(): void {
    localStorage.setItem("userLogedIn", JSON.stringify(true));
  }
  public dropUserIsLogedIn(): void {
    localStorage.removeItem("userLogedIn");
  }
  public getUserIsLogedIn(): boolean{
    var value = localStorage.getItem("userLogedIn");
    if (value) {
      return JSON.parse(value);
    } else {
      return false;
    }
  }
  public canActivate() {
    if (!this.getUserIsLogedIn()) {
      this.router.navigate(["login-form"]);
    }
    return this.getUserIsLogedIn();
  }
}
