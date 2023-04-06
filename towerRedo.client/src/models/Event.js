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
    this.startDate = data.StartDate,
    this.type = data.type,
    this.creator = new Account(data.creator),
    this.creatorId = data.creatorId
  }
}