import React from 'react'
import { Outlet } from 'react-router-dom'
import { Navbar } from './components/Navbar.jsx'
import { AppState } from "./AppState.js"
import EventForm from "./components/EventForm.jsx"


export function App() {

  return (
    <div className="App" id="app">
      <header>
        <Navbar />
      </header>

      <main>
        <Outlet />
      </main>

     <div
        className="modal fade "
        id="postModal"
        tabIndex={-1}
        role="dialog"
        aria-labelledby="postModalLabel"
        aria-hidden="true"
      >
        <div
          className="modal-dialog modal-dialog-centered  roundedCorners"
          role="document"
        >
          <div className="modal-content modalBackground roundedCorners">
            <div className="modal-header text-center">
              <h5 className="modal-title w-100 text-dark" id="modalTitleId">
                Create Event
              </h5>
              <button
                type="button"
                className="btn-close btn-close-white"
                data-bs-dismiss="modal"
                aria-label="Close"
              ></button>
            </div>
            <div className="container-fluid ">
              <EventForm />
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
