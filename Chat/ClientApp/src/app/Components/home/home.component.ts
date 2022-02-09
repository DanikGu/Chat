import { Component, NgModule} from '@angular/core';
import { Router } from '@angular/router';
import { Message } from '../../Models/Message';
import { Chat } from '../../Models/Chat';
import { LoginService } from '../../Services/Login/login.service';
import { MessagesService } from '../../Services/Messages/messages.service';
import { CommonModule } from '@angular/common'
import { GetMessagesResponse } from '../../Models/GetMessagesResponse';
import { Observable } from 'rxjs/internal/Observable';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private messageService: MessagesService, private loginService: LoginService, private router: Router)
  {
    this.getMessages();
    this.userId = this.loginService.getUserId();
   }
  public messages: Message[] = [];
  public getMessages() {
    this.messageService.GetMessages().subscribe(result => this.chats = result.chats);
  }
  public chats: Chat[] = [];
  public currentChat: Chat = { id: -1, draftMessage: '', messages: [], name: '' };
  public userId: number = 0;
  public sendMessage() {
    if (!this.currentChat.id || !this.currentChat.draftMessage) { return; }
    var message = {
      senderId: this.userId,
      text: this.currentChat.draftMessage,
      chatId: this.currentChat.id,
      createdDate: '',
      id: -1
    };
    this.messageService.
      SendMessage(this.currentChat.draftMessage, this.currentChat.id).
      subscribe(res => message.id = res.messageId);
    this.currentChat.messages.unshift(message);
    this.currentChat.draftMessage = '';
  }

  public chooseChat(chat: Chat) {
    this.currentChat = chat;
  }
  public Logout(): void {
    this.router.navigate(['login-form']);
    this.loginService.Logout();
  }
}
