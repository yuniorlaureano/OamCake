import React, {useState, useEffect} from "react";
//overflow: hidden; padding-right: 17px;
//modal-open
export default function Modal({show, hide, children, buttons, heading}){
    let outerDivStyle = {
        'display': show ? 'block':'none'
    };

    function hideModal(e) {
        if(e.target == e.currentTarget) {
            hide()
        }
    }

    useEffect(function(){
        const body = document.querySelector("body");
        
        if(show) {
            body.classList.add('modal-open', 'body-modal');
        } else {
            body.classList.remove('modal-open', 'body-modal')
        }
    }, [show]);
    
    return (
        <div onClick={hideModal} className={`modal fade ${ show ? 'show' : '' }`} tabIndex="-1" style={outerDivStyle} aria-modal="true" role={`${ show ? 'dialog' : 'true' }`}>
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                <div className="modal-header">
                    <h5 className="modal-title">
                        {heading}
                    </h5>
                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close" onClick={hideModal}></button>
                </div>
                <div className="modal-body">
                    {children}
                </div>
                <div className="modal-footer">
                    {buttons}                    
                </div>
                </div>
            </div>
        </div>
    )
}