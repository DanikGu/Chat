import { Component } from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginResponse } from '../../models/LoginResponse';
import { SignUpUser } from '../../Models/SignUpUser';
import { LoginService } from '../../Services/Login/login.service';
@Component({
  selector: 'app-sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['../login-form/login-form.component.css']
})
export class SignUpFormComponent {
  constructor(private loginService: LoginService,
              private router: Router) {

  }
  model = new SignUpUser(-1, '', '', '');
  public Submit() {
    if (this.model.password !== this.model.secondPassword)
    {
      return;// TODO validation
    }
    this.loginService.signUp(this.model.username, this.model.password).
      subscribe((response: LoginResponse) => this.SignUpResult(response));
  }
  SignUpResult(response: LoginResponse) {
    if (response && response.succsess) {
      this.loginService.setUserIsLogedIn();
      this.router.navigateByUrl('/');
    }
  }
}
