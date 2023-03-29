import React from 'react';
import { Comment } from '../models/Comment.js';
import { eventsService } from '../services/EventsService.js';
import Pop from '../utils/Pop.js';
import "./styles/CommentCardStyle.scss";
import { BindEditable } from "../utils/FormHandler.js";
import { useParams } from 'react-router';

export default function CommentForm() {
    const { id } = useParams()
    let editable = new Comment({})
    const bindEditable = BindEditable(editable);

    async function createComment() {
        try {
            window.event?.preventDefault()
            const reqBody = {
                ...editable,
                eventId: id
            }
            await eventsService.createComment(reqBody)
        }
        catch (error) {
            Pop.error(error);
        }
    }

    return (

        <div className="CommentForm">
            <form onSubmit={createComment} className="text-center">
                <textarea rows={8} name="body" id="body" onChange={bindEditable} className="form-control my-3"></textarea>
                <div className='text-end me-5'>
                    <button className='btn btn-outline-dark'>Submit</button>

                </div>
            </form>
        </div>
    )

}