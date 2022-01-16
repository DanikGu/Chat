import { Component, Inject } from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { User } from '../models/User';
import { HttpClient } from '@angular/common/http';
@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }
  private baseUrl: string;
  model = new User(-1, '', '');
  public Submit() {
    this.http.post<LoginResponse>(this.baseUrl + 'Login/SignIn',
      {
          UserName: this.model.username,
          Password: this.model.password,
          redirectUrl: '/'
      }).subscribe((response: LoginResponse) => this.LoginResult(response));
  }
  LoginResult(response: LoginResponse) {
    alert(response.succsess);
  }
}
interface LoginResponse {
  succsess: boolean;
  message: string;
}
