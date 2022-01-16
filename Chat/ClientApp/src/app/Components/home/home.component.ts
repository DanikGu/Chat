import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../../Services/Login/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(private loginService: LoginService, private router: Router)
  { }
  public Logout(): void {
    this.router.navigate(['login-form']);
    this.loginService.Logout();
  }
}
