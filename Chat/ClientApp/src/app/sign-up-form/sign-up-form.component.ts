import { Component } from '@angular/core';
import { FormControl, NgForm } from '@angular/forms';
import { SignUpUser } from '../models/SignUpUser';
@Component({
  selector: 'app-sign-up-form',
  templateUrl: './sign-up-form.component.html',
  styleUrls: ['../login-form/login-form.component.css']
})
export class SignUpFormComponent {
  model = new SignUpUser(-1, '', '', '');
  public Submit() {
    
  }
}
