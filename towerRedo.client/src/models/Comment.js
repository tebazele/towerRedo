export class Comment {
    constructor(data = {}) {
        this.body = data.body
        this.id = data.id
        this.eventId = data.eventId
        this.isAttending = data.isAttending
        this.creatorId = data.creatorId
        this.creator = data.creator
    }
}