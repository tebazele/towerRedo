import { observer } from 'mobx-react-lite';
import React from 'react';
import { AppState } from "../../AppState";
import { Account } from "../../models/Account";
import { BindEditable } from "../../utils/FormHandler";
import { logger } from "../../utils/Logger";
import Pop from "../../utils/Pop";
import { accountService } from "../../services/AccountService";

function EditAccount() {
  const editable = {...AppState.account}
  const bindEditable = BindEditable(editable)

  async function editAccount() {
    try {
      window.event?.preventDefault()
      console.log(editable);
      await accountService.editAccount(editable)
    } catch (error) {
      logger.error('[ERROR]',error)
      Pop.error(('[ERROR]'), error.message)
    }
  }

  return (

    <div className="editForm">
      <form onSubmit={editAccount} className="form-control" key={editable.id}>
        <label className="form-label" htmlFor="name">Name:</label>
        <input required type="text" className="form-control" defaultValue={editable.name} id="name" name="name" placeholder="Name..." onChange={bindEditable} />
        <label className="form-label" htmlFor="picture">Picture:</label>
        <input required type="text" className="form-control" name="picture" defaultValue={editable.picture} id="picture" placeholder="Picture..." onChange={bindEditable} />
        <button type="submit" className="btn btn-outline">Submit</button>
      </form>
    </div>
  )

}
export default observer(EditAccount)