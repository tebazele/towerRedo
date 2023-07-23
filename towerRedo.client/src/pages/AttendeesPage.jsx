import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { useParams } from 'react-router-dom';
import { eventsService } from '../services/EventsService.js';
import { AppState } from '../AppState.js';

function AttendeesPage() {

// let cars = (AppState.cars.map(c => {
//     return (
//       <div className="col-md-4 my-3" key={c.id}>
//         <CarCard car={c} />
//       </div>
//     )
//   }))
    let attendees = (AppState.tickets.map(t => {
        return (
            
                <div className='col-12 m-3 d-flex justify-content-center align-items-center' key={t.id}>
                <img src={t.creator.picture} className='m-2 rounded-circle'></img>
                <h3 className='my-3 text-light'>{ t.creator.name }</h3>
                </div>
            
        )
    }))

   

    return (

        <div className="AttendeesPage">
            {attendees}
        </div>
    )

}
export default observer(AttendeesPage)