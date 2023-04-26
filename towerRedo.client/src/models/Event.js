import { Account } from "./Account";

export class Event {
  constructor(data = {}) {
    this.id = data.id,
    this.name = data.name,
    this.description = data.description,
    this.coverImg = data.coverImg,
    this.location = data.location,
    this.capacity = data.capacity,
    this.isCanceled = data.isCanceled,
    this.startDate = new Date(data.startDate).toDateString('en-us'),
    this.type = data.type,
    this.creator = data.creator ? new Account(data.creator) : null,
    this.creatorId = data.creatorId
  }
}