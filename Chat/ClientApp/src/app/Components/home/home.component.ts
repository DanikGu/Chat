import { Component, NgModule } from '@angular/core';
import { Router } from '@angular/router';
import { Message } from '../../Models/Message';
import { LoginService } from '../../Services/Login/login.service';
import { MessagesService } from '../../Services/Messages/messages.service';
import { CommonModule } from '@angular/common'
import { GetMessagesResponse } from '../../Models/GetMessagesResponse';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})

export class HomeComponent {
  constructor(private messageService: MessagesService, private loginService: LoginService, private router: Router)
  { }
  public messages: Message[] = [];
  public getMessages() {
    this.messageService.GetMessages().subscribe(result => this.messages = result.messages);
  }
  public sendMessage() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    var time = today.getHours() + '/' + today.getMinutes() + '/' + today.getSeconds() + '/' + today.getMilliseconds();
    var text = "testMessage" + mm + '/' + dd + '/' + yyyy + "   " + time;
    this.messageService.SendMessage(text, 2).subscribe();
  }
  public Logout(): void {
    this.router.navigate(['login-form']);
    this.loginService.Logout();
  }
}
