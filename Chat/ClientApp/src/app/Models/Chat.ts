import { Message } from "./Message";

export interface Chat
{
  id: number,
  name: string,
  messages: Message[],
  draftMessage: string
}
