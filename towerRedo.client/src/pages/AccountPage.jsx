import { observer } from "mobx-react";
import React, { useEffect } from "react";
import { AppState } from "../AppState.js";
import EditAccountForm from "../components/Account/EditAccountForm.jsx";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { accountService } from "../services/AccountService.js";

function AccountPage() {

  async function getTickets() {
    try {
      await accountService.getTickets()
    } catch (error) {
      logger.error('[ERROR]',error)
      Pop.error(('[ERROR]'), error.message)
    }
  }

  useEffect(() => {
    getTickets()
  }, [])
  return (
    <div className="account-page">
      <div className="card">
        <div className="card-body bg-dark p-5 text-center">
          <img src={AppState.account?.picture}
            alt={AppState.account?.name}
            className="rounded-circle" height="200" />
          <p className="display-6 my-2">{AppState.account?.name}</p> <span className="mdi selectable fs-1 rounded mdi-pencil-outline">Edit Account</span>
        </div>
      </div>
      <EditAccountForm />
    </div>
  )
}

export default observer(AccountPage)