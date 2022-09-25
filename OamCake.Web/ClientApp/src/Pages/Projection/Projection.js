import React, { useState } from 'react'
import ProjectionDetailForm from './ProjectionDetailForm';
import CakePage from './CakePage';
import ProjectionPage from './ProjectionPage';
import SummaryPage from './SummaryPage';
import Categorybar from '../../components/Categorybar';

export default function Projection() {
  const [selectedCakes, setSelectedCakes] = useState({});


  function addCake(e, cake) {
    if(e.target.checked) {
      setSelectedCakes({...selectedCakes, [`${cake.id}`]: {...cake, quantity: cake.quantity || 1}})
    } else {
      setSelectedCakes({...selectedCakes, [`${cake.id}`]: null})
    }    
  }

  function handleUpdateQuantity(id, quantity) {
    setSelectedCakes({...selectedCakes, [`${id}`]: {...selectedCakes[id], quantity: quantity}});
  }

  return (
    <div>
        <div className="row">
            <ProjectionDetailForm catalog={window.bcatalog} cakesId={selectedCakes}/>
        </div>
        <Categorybar catalogId={window.bcatalog?.id} categoryId={window.bcategoryId} categories={window.bcategories} search={window.bsearch}/>
        <div className="card">
          <nav>
            <div className="nav nav-tabs" id="nav-tab" role="tablist">
              <button className="nav-link active" id="nav-cakes-tab" data-bs-toggle="tab" data-bs-target="#nav-cakes" type="button" role="tab" aria-controls="nav-cakes" aria-selected="true">Pasteles</button>
              <button className="nav-link" id="nav-projection-tab" data-bs-toggle="tab" data-bs-target="#nav-projection" type="button" role="tab" aria-controls="nav-projection" aria-selected="false">Proyección</button>
              <button className="nav-link" id="nav-summary-tab" data-bs-toggle="tab" data-bs-target="#nav-summary" type="button" role="tab" aria-controls="nav-summary" aria-selected="false">Resumen</button>
            </div>
          </nav>      
          <div className="tab-content" id="nav-tabContent" style={{'padding': '15px'}}>
            <div className="tab-pane fade show active" id="nav-cakes" role="tabpanel" aria-labelledby="nav-cakes-tab">
              <CakePage cakes={window.bcakes} addCake={addCake}/>
            </div>
            <div className="tab-pane fade" id="nav-projection" role="tabpanel" aria-labelledby="nav-projection-tab">
              <ProjectionPage cakes={selectedCakes} onUpdateQuantity={handleUpdateQuantity}/>
            </div>
            <div className="tab-pane fade" id="nav-summary" role="tabpanel" aria-labelledby="nav-summary-tab">
              <SummaryPage cakesId={selectedCakes}/>
            </div>
          </div>
      </div>
    </div>
  )
}
