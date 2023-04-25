import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"
import { Event } from "../models/Event.js";

class EventsService {
    async editEvent(editable) {
      const res = await api.put(`api/events/${editable.id}`, editable)
      logger.log('[EDITED EVENT]', new Event(res.data))
    }
   async createEvent(editable) {
      const res = await api.post('api/events', editable)
      logger.log('[CREATED EVENT]', new Event(res.data))
      AppState.events.push(new Event(res.data))
    }
 
    async createComment(reqBody) {
        const res = await api.post('api/comments', reqBody)
        logger.log(res.data)
        AppState.comments.push(res.data);
    }

    async getOneEvent(eventId) {
        const res = await api.get(`api/events/${eventId}`)
        logger.log(res.data)
        AppState.activeEvent = new Event(res.data);
    }
    async getEvents() {
        const res = await api.get('api/events')
        logger.log(res.data)
        AppState.events = res.data.map(e => new Event(e));
        logger.log(AppState.events)
    }

    async getComments(id) {
        const res = await api.get(`api/events/${id}/comments`);
        logger.log(res.data)
        AppState.comments = res.data
    }

    // SECTION TICKETS

    async getTickets(id) {
        const res = await api.get(`api/events/${id}/tickets`)
        logger.log('[GETTING:TICKETS]', res.data)
        AppState.tickets = res.data
    }

    async deleteTicket(id) {
        const res = await api.delete(`api/tickets/${id}`)
        logger.log(res.data, "DELETING TICKET")
        AppState.tickets = AppState.tickets.filter(t => t.id != id)
        if(AppState.activeEvent != null) { 
        AppState.activeEvent.capacity++
        }
    }
   

}

export const eventsService = new EventsService();