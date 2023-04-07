import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { useParams } from 'react-router';
import { Link } from 'react-router-dom';
import { AppState } from '../AppState.js';
import CommentsCard from '../components/CommentsCard.jsx';
import { eventsService } from '../services/EventsService.js';
import Pop from '../utils/Pop.js';
import "./styles/EventPageStyle.scss";
import "../components/styles/CommentCardStyle.scss";
import CommentForm from '../components/CommentForm.jsx';

function EventPage() {

    const { id } = useParams()
    async function getOneEvent() {
        try {
            await eventsService.getOneEvent(id)
        }
        catch (error) {
            Pop.error(error);
        }
    }

    async function getComments() {
        try {

            await eventsService.getComments(id)
        }
        catch (error) {
            Pop.error(error);
        }
    }

    let comments = AppState.comments.map(c => {
        return (
            <div key={c.id} className="my-3">
                <CommentsCard comment={c} />
            </div>
        )
    })

    useEffect(() => {
        getOneEvent()
        getComments()
    }, [])

    return (

        <div className="EventPage">
            <div className='container-fluid'>
                <section className="row mx-2 my-3 eventDetailsFont rounded">
                    <div style={{ backgroundImage: `url(${AppState.activeEvent?.coverImg})` }} className="rounded bg-active-image col-12">
                        <section className="frosted row">
                            <div className="col-12 col-md-4 d-flex align-items-center">
                                <img src={AppState.activeEvent?.coverImg} className="rounded img-fluid active-image-size border border-1" />
                            </div>
                            <div className="col-md-8">
                                <section className="row justify-content-between">
                                    <div className="col-md-12"><i className="mdi mdi-dots-horizontal"></i></div>
                                    <div className="col-5 eventDetailsFont">
                                        <h1>{AppState.activeEvent?.name}</h1>
                                        <h4>{AppState.activeEvent?.location}</h4>
                                    </div>
                                    <div className='col-md-5'>
                                        <h1>{AppState.activeEvent?.startDate}</h1>
                                    </div>
                                </section>
                                <section className="row desc-height">
                                    <div className="col-md-12 p-2">
                                        <p>{AppState.activeEvent?.description}</p>
                                    </div>
                                </section>
                                <section className="row justify-content-between">
                                    <div className="col-md-5">
                                        <h6><span className="ticketsLeftFont">{AppState.activeEvent?.capacity}</span> spots left</h6>
                                    </div>
                                    <div className="col-md-4 text-end">
                                        <button className='btn btn-warning'>Attend</button>
                                    </div>
                                </section>
                            </div>
                        </section>
                    </div>
                </section>
                <section className="row">
                    <div className="col-md-12">
                        <Link to={`/event/${id}/attendees`}>
                            <h3>See who's attending</h3>
                        </Link>
                    </div>
                </section>
                <section className="row justify-content-center ">
                    <div className="col-md-10 bg-secondary rounded p-3 my-4">
                        <CommentForm />
                        {comments}
                    </div>
                </section>

            </div>
        </div>
    )

}
export default observer(EventPage)