import React from 'react'

export default function ProjectionPage({cakes={}}) {
  return (
    <div className="accordion accordion-flush" id="projection-accordion">
        {
            Object.keys(cakes).filter(x => cakes[x] != null).map(x => (
                <div className="accordion-item" key={`cake-${cakes[x].id}`}>
                    <h2 className="accordion-header" id="flush-headingOne">
                        <button className="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target={"#flush-collapse-" + cakes[x].id} aria-expanded="false" aria-controls={"flush-collapse-" + cakes[x].id}>
                            {cakes[x].name + " | " + cakes[x].categoryName + "  $" + cakes[x].price}
                        </button>
                        <input type="number" className="form-control" style={{width: '100px'}}/>
                    </h2>
                    <div id={"flush-collapse-" + cakes[x].id} className="accordion-collapse collapse" aria-labelledby="flush-headingOne" data-bs-parent="#projection-accordion">
                        <div className="accordion-body">Placeholder content for this accordion, which is intended to demonstrate the <code>.accordion-flush</code> className. This is the first item's accordion body.</div>
                    </div>
                </div>
            ))
        }
    </div>
  )
}
