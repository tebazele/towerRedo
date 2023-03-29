import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { AppState } from '../AppState.js';
import EventCard from '../components/EventCard.jsx';
import { eventsService } from '../services/EventsService.js';
import Pop from "../utils/Pop.js"
import "./styles/HomePageStyle.scss"

function HomePage() {
  async function getEvents() {
    try {
      await eventsService.getEvents()
    }
    catch (error) {
      Pop.error(error);
    }
  }

  let events = (AppState.events.map(e => {
    return (
      <div key={e.id} className="col-4 my-3">
        <EventCard event={e} />
      </div>
    )
  }))

  useEffect(() => {
    getEvents()
  }, [])

  return (

    <div className="home-page container-fluid">
      <div className='row'>
        <div style={{ backgroundImage: `url("https://images.unsplash.com/photo-1521055170349-25f955971658?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1746&q=80")` }} className="bg-cover-image"></div>
        <div className='col-12 d-flex justify-content-around mt-2'>
          <button className='btn btn-outline-dark'>All</button>
          <button className='btn btn-outline-dark'>Concert</button>
          <button className='btn btn-outline-dark'>Convention</button>
          <button className='btn btn-outline-dark'>Sport</button>
          <button className='btn btn-outline-dark'>Digital</button>
          <button className='btn btn-outline-dark'>Other</button>

        </div>
        {events}

      </div>
    </div>
  )

}
export default observer(HomePage)