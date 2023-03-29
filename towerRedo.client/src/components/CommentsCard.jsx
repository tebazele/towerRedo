import React from 'react';
import PropTypes from 'prop-types';

export default function CommentsCard({ comment }) {

    return (

        <div className="CommentsCard">
            <div className='row justify-content-center'>
                <div className="col-3">
                    <img src={comment.creator.picture} className="rounded-circle prof-image" />
                </div>
                <div className="col-8 bg-info text-light rounded p-2 ps-4">
                    <strong>{comment.creator.name}</strong>
                    <p>{comment.body}</p>
                </div>
            </div>
        </div>
    )

}

CommentsCard.propTypes = {
    comment: PropTypes.instanceOf(Object)
}