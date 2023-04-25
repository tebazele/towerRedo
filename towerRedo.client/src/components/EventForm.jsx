import { observer } from 'mobx-react-lite';
import React from 'react';
import { BindEditable } from "../utils/FormHandler.js";
import { Event } from "../models/Event";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { eventsService } from "../services/EventsService.js";
import { useNavigate } from "react-router-dom";

function EventForm() {
  const editable = ({})
  let bindEditable = BindEditable(editable)
  const navigate = useNavigate()

  async function handleSubmit() {
    try {
      window.event?.preventDefault()
      if(editable.id) {
        await eventsService.editEvent(editable)
        Pop.success(`${editable.name} was edited.`)
      } else {
        let event = await eventsService.createEvent(editable)
        logger.log(event)
        navigate(`/event/${event.id}`)
        Pop.success(`${editable.name} was created! =)`)
      }
    } catch (error) {
      logger.error('[ERROR]',error)
      Pop.error(('[ERROR]'), error.message)
    }
  }

  return (

    <div className="eventForm">

    <form onSubmit={handleSubmit} className="row d-flex justify-content-center" key={editable.id}>

      <div className="col-6 mb-3 ">
          <label className="form-label" htmlFor="">
            Name
          </label>
          <input
            defaultValue={editable.name}
            className="form-control"
            name="name"
            id="name"
            type="text"
            onChange={bindEditable}
          />
        </div>

      <div className="col-6 mb-3 ">
          <label className="form-label" htmlFor="">
            Cover Image
          </label>
          <input
            defaultValue={editable.coverImg}
            className="form-control"
            name="coverImg"
            id="coverImg"
            type="text"
            onChange={bindEditable}
          />
        </div>

      <div className="col-6 mb-3 ">
          <label className="form-label" htmlFor="">
            Description
          </label>
          <input
            defaultValue={editable.description}
            className="form-control"
            name="description"
            id="description"
            type="text"
            onChange={bindEditable}
          />
        </div>
    
      <div className="col-6 mb-3 ">
          <label className="form-label" htmlFor="">
            Location
          </label>
          <input
            defaultValue={editable.location}
            className="form-control"
            name="location"
            id="location"
            type="text"
            onChange={bindEditable}
          />
        </div>
    
      <div className="col-6 mb-3 ">
          <label className="form-label" htmlFor="">
            Capacity
          </label>
          <input
            defaultValue={editable.capacity}
            className="form-control"
            name="capacity"
            id="capacity"
            type="number"
            onChange={bindEditable}
          />
        </div>
    
      <div className="col-6 mb-3 ">
          <label className="form-label" htmlFor="">
            Start Date
          </label>
          <input
            defaultValue={editable.startDate}
            className="form-control"
            name="startDate"
            id="startDate"
            type="date"
            onChange={bindEditable}
          />
        </div>
    
      <div className="col-6 mb-3 ">
          <label className="form-label" htmlFor="">
            Type
          </label>
          <select name="type" defaultValue={editable.type} onChange={bindEditable} className="form-select" aria-label="Default select example">
            <option defaultValue={editable.type} value="Concert">Concert</option>
            <option defaultValue={editable.type} value="Convention">Convention</option>
            <option defaultValue={editable.type} value="Sport">Sport</option>
            <option defaultValue={editable.type} value="Digital">Digital</option>
            <option defaultValue={editable.type} value="Other">Other</option>
          </select>
      </div>

      <button
          data-bs-dismiss="modal"
          type="submit"
          className="col-11 mb-2 btn btn-outline selectable"
        >
          Submit
      </button>
    
    </form>

    </div>
  )

}
export default observer(EventForm)