import { observer } from 'mobx-react-lite';
import React, { useEffect } from 'react';
import { useParams } from 'react-router';
import { App } from '../App.jsx';
import { AppState } from '../AppState.js';
import { eventsService } from '../services/EventsService.js';
import Pop from '../utils/Pop.js';
import "./styles/EventPageStyle.scss"

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

    useEffect(() => {
        getOneEvent()
    }, [])

    return (

        <div className="EventPage">
            <div className='container-fluid'>
                <section className="row mx-2 my-3 pos-rel">
                    <div className='pos-abs-1 blur'></div>
                    <div style={{ backgroundImage: `url(${AppState.activeEvent?.coverImg})` }} className="bg-active-image col-12 pos-abs-1 pt-4">
                        <section className="row m-auto">
                            <div className="col-4">
                                <img src={AppState.activeEvent?.coverImg} className="img-fluid active-image-size border border-1" />
                            </div>
                            <div className="col-8">
                                <section className="row justify-content-between">
                                    <div className="col-5">
                                        <h1>{AppState.activeEvent?.name}</h1>
                                        <h4>{AppState.activeEvent?.location}</h4>
                                    </div>
                                    <div className='col-5'>
                                        <h1>{AppState.activeEvent?.startDate}</h1>
                                    </div>
                                </section>
                                <section className="row desc-height">
                                    <div className="col-12 p-2">
                                        <p>{AppState.activeEvent?.description}</p>
                                    </div>
                                </section>
                                <section className="row justify-content-between">
                                    <div className="col-5">
                                        <h6>{AppState.activeEvent?.capacity}</h6>
                                    </div>
                                    <div className="col-4 text-end">
                                        <button className='btn btn-warning'>Attend</button>
                                    </div>
                                </section>
                            </div>
                        </section>

                    </div>
                </section>

            </div>
        </div>
    )

}
export default observer(EventPage)