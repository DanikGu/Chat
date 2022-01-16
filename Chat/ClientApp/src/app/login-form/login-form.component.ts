import { Component, Inject } from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { User } from '../models/User';
import { LoginResponse } from '../models/LoginResponse';
import { LoginService } from '../Services/Login/login.service';
@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private loginService: LoginService  ) {
  }
  model = new User(-1, '', '');
  public Submit() {
    this.loginService.
      login(this.model.username, this.model.password).
      subscribe((response: LoginResponse) => this.LoginResult(response));
  }
  LoginResult(response: LoginResponse) {
    if (response && response.succsess) {
      this.loginService.setUserIsLogedIn();
      this.router.navigateByUrl('/');
    }
  }
}

