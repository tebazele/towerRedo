import React from 'react';
import PropTypes from 'prop-types';
import "../pages/styles/EventPageStyle.scss";
import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { commentsService } from "../services/CommentsService";

export default function CommentsCard({ comment }) {

    async function deleteComment() {
        try {
        await commentsService.delete(comment.id)
        } catch (error) {
          logger.error('[ERROR]',error)
          Pop.error(('[ERROR]'), error.message)
        }
    }

    return (

        <div className="CommentsCard">
            <div className='row justify-content-center'>
                <div className="col-1">
                    <img src={comment.creator?.picture} className="rounded-circle prof-image coolBorder" />
                </div>
                <div className="col-5 bg-info text-light rounded p-2 ps-4 coolBoxShadow">
                    <strong>{comment.creator?.name}</strong> { AppState.account?.id == comment.creator?.id ? (<span onClick={deleteComment} className="mdi mdi-delete-forever-outline"></span>) : null }
                    <p>{comment?.body}</p>
                </div>
            </div>
        </div>
    )

}

CommentsCard.propTypes = {
    comment: PropTypes.instanceOf(Object)
}