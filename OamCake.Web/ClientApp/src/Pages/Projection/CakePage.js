import React from 'react'

export default function CakePage({cakes=[], addCake}) {
  return (
    <div className="list-group">
        {
            cakes.map(x => (
                <label className="list-group-item" key={`cake-${x.id}`}>
                    <input className="form-check-input me-1" type="checkbox" defaultChecked={x.isSet} onClick={(e) => addCake(e, x)} />
                    {x.name + " | " + x.categoryName + "  $" + x.price}
                </label>
            ))
        }
    </div>
  )
}
