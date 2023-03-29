import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"

class EventsService {
    async createComment(reqBody) {
        const res = await api.post('api/comments', reqBody)
        logger.log(res.data)
        AppState.comments.push(res.data);
    }

    async getOneEvent(id) {
        const res = await api.get(`api/events/${id}`)
        logger.log(res.data)
        AppState.activeEvent = res.data;
    }
    async getEvents() {
        const res = await api.get('api/events')
        logger.log(res.data)
        AppState.events = res.data;
    }

    async getComments(id) {
        const res = await api.get(`api/events/${id}/comments`);
        logger.log(res.data)
        AppState.comments = res.data
    }

}

export const eventsService = new EventsService();