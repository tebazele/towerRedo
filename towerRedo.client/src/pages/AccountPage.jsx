import { observer } from "mobx-react";
import React from "react";
import { AppState } from "../AppState.js";
import EditAccountForm from "../components/Account/EditAccountForm.jsx";

function AccountPage() {

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