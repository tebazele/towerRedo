import React from 'react';
import PropTypes from 'prop-types';
import "../pages/styles/EventPageStyle.scss";

export default function CommentsCard({ comment }) {

    return (

        <div className="CommentsCard">
            <div className='row justify-content-center'>
                <div className="col-1">
                    <img src={comment.creator?.picture} className="rounded-circle prof-image coolBorder" />
                </div>
                <div className="col-5 bg-info text-light rounded p-2 ps-4 coolBoxShadow">
                    <strong>{comment.creator?.name}</strong>
                    <p>{comment?.body}</p>
                </div>
            </div>
        </div>
    )

}

CommentsCard.propTypes = {
    comment: PropTypes.instanceOf(Object)
}