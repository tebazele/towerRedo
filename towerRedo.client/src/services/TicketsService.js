import { runInAction } from "mobx"
import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class TicketsService{
  async createTicket(body) {
    let ticketData = {}
    ticketData.eventId = body
    const res = await api.post('api/tickets', ticketData)
    logger.log('[CREATING:TICKET]', res.data)
    runInAction(() => {
      AppState.tickets.push(res.data)
    }) 
  }
}

export const ticketsService = new TicketsService()