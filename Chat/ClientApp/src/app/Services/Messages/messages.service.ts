import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GetMessagesResponse } from '../../Models/GetMessagesResponse';
@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router: Router) {
  }
  public GetMessages(): Observable<GetMessagesResponse> {
    return this.http.post<GetMessagesResponse>(this.baseUrl + 'Message/ReciveLastMessages',
      {});
  }
  public SendMessage(text: string, recipientId: number): Observable<GetMessagesResponse> {
    return this.http.post<GetMessagesResponse>(this.baseUrl + 'Message/SendMessage',
      {
        message: text,
        recipientId: recipientId
      });
  }
}
