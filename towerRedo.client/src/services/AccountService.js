import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService';
import { Event } from '../models/Event.js';

class AccountService {
  async getMyEvents() {
    const res = await api.get('account/events')
    logger.log('[getting events i have created]', res.data)
    AppState.myEvents = res.data.map(e => new Event(e))
  }
  async getTickets() {
    const res = await api.get('account/tickets')
    logger.log('[GETTING:MyTICKETS]', res.data)
    AppState.tickets = res.data
  }
  async getAccount() {
    try {
      if (AppState.account) {
        return AppState.account
      }
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
      return AppState.account
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???')
      return null
    }
  }

  async editAccount(body) {
    const res = await api.put('account', body)
    logger.log(res.data, "EDITED ACCOUNT")
    AppState.account = res.data
  }
}

export const accountService = new AccountService()