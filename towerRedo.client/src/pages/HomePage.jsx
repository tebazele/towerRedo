import { observer } from 'mobx-react-lite';
import React, { useEffect, useState } from 'react';
import { AppState } from '../AppState.js';
import EventCard from '../components/EventCard.jsx';
import { eventsService } from '../services/EventsService.js';
import Pop from "../utils/Pop.js"
import "./styles/HomePageStyle.scss"

function HomePage() {
  const [filterBy, setFilterBy] = useState('')
  async function getEvents() {
    try {
      await eventsService.getEvents()
    }
    catch (error) {
      Pop.error(error);
    }
  }

  function filterEvents(f) {
    let events = AppState.events.filter(e => f ? e.type == f : true).map(e => {
      console.log(AppState.events);
      return (
        <div key={e.id} className="col-4 my-3">
          <EventCard event={e} />
        </div>
      )
    })
    return events;
  }

  useEffect(() => {
    getEvents()
  }, [])

  return (

    <div className="home-page container-fluid">
      {/* <section className="row">
        <h1>{filterBy}</h1>
      </section> */}
      <div className='row'>
        <div style={{ backgroundImage: `url("https://images.unsplash.com/photo-1521055170349-25f955971658?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1746&q=80")` }} className="bg-cover-image"></div>
        <div className='col-12 d-flex justify-content-around mt-2'>
          <button onClick={() => setFilterBy('')} className='btn btn-dark'>All</button>
          <button onClick={() => setFilterBy('Concert')} className='btn btn-dark'>Concert</button>
          <button onClick={() => setFilterBy('Convention')} className='btn btn-dark'>Convention</button>
          <button onClick={() => setFilterBy('Sport')} className='btn btn-dark'>Sport</button>
          <button onClick={() => setFilterBy('Digital')} className='btn btn-dark'>Digital</button>
          <button onClick={() => setFilterBy('Other')} className='btn btn-dark'>Other</button>

        </div>
        {filterEvents(filterBy)}

      </div>
    </div>
  )

}
export default observer(HomePage)