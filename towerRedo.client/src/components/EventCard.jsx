import React from 'react';
import PropTypes from 'prop-types';
import "./styles/EventCardStyle.scss"
import { Link } from 'react-router-dom';
import { Event } from "../models/Event.js";



export default function EventCard({ event }) {

    return (

        <div className="EventCard">
            <Link title={`${event.name} Link`} to={`event/${event?.id}`} >
                <div style={{ backgroundImage: `url(${event?.coverImg})` }} className="rounded bg-event-image">
                </div>
            </Link>
            <p className="text-light fs-5">{event?.name}</p>

        </div>
    )

}

EventCard.propTypes = {
    event: PropTypes.instanceOf(Event)
}