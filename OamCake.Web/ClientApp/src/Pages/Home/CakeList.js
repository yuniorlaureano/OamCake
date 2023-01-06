import React, {useState, useEffect} from "react"

export default function CakeList({bcakes, add, substract, cart}) {
    
    return (
        <div className="row">
            {
                bcakes.map((cake, i) => (
                    <div className="col-xl-3 col-lg-4 col-sm-6" key={cake.cakeId}>
                        <div className="product text-center border mb-2">
                            <div className="position-relative mb-3">
                                <div className="badge text-white bg-"></div><a className="d-block" href="/Details?id=@product.Id"><img className="img-fluid w-100" src={`/photos/${cake.realPhoto}`} alt={cake.cakeName}/></a>
                                
                            </div>
                            <h6> <a className="reset-anchor" href="/Details?id=@product.Id">{cake.cakeName}</a></h6>
                            <p className="small text-muted">${cake.price}</p>
                            {/* <button className="btn btn-primary" onClick={() => buy(cake)}><i className="bi bi-cart"></i></button> */}
                            <div className="btn-group btn-group-sm" role="group" aria-label="Basic example">
                                <button type="button" className="btn btn-default" onClick={() => substract(cake)}>
                                    <i className="bi bi-caret-down"></i>
                                </button>
                                <span >{cart[cake.id]?.qty || 0}</span>
                                <button type="button" className="btn btn-default" onClick={() => add(cake)}>
                                    <i className="bi bi-caret-up"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                ))
            }
        </div>
    )
}