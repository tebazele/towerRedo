import { logger } from "../utils/Logger.js";
import { api } from "./AxiosService.js"
import { AppState } from "../AppState.js"

class EventsService {
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

}

export const eventsService = new EventsService();