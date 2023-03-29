import React from 'react';
import PropTypes from 'prop-types';
import { AppState } from '../AppState.js';
import "./styles/EventCardStyle.scss"
import { Link } from 'react-router-dom';



export default function EventCard({ event }) {

    return (

        <div className="EventCard">
            <Link to={`event/${event.id}`} >
                <div style={{ backgroundImage: `url(${event?.coverImg})` }} className="rounded bg-event-image">
                </div>
            </Link>
            <h6>{event.name}</h6>

        </div>
    )

}

EventCard.propTypes = {
    event: PropTypes.instanceOf(Object)
}