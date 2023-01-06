import React, { useState, useEffect } from "react";
import CatalogDetailForm from "./CatalogDetailForm";
import SerachForm from "./SerachForm";
import Categorybar from "./Categorybar";
import CakeDetail from "./CakeDetail";

export default function Ingredient() {
  const [cakesId, setCakesId] = useState({});
  
  function addProduct(e, id){    
    if(e.target.checked) {
      setCakesId({...cakesId, [`${id}`]: { id: id, value: 0 }})
    } else {
      setCakesId({...cakesId, [`${id}`]: null})
    }    
  }

  function addPrice(e, id){        
    let value = e.target.value;
    setCakesId({...cakesId, [`${id}`]: { id: id, value: value }})
  }
  
  useEffect(() => {
    if(window.bcatalog?.cakesId) {
      var cakes = {};
      window.bcatalog.cakesId.forEach(item => {
        let values = item.split('|');        
        cakes[values[0]] = {
          id: values[0],
          value: values[1]
        };
      });
      setCakesId(cakes)
    }
    
  }, []);

  return (
    <div>
        <div className="row">
            <CatalogDetailForm catalog={window.bcatalog} cakesId={cakesId}/>
            <hr/>
            <SerachForm search={window.bsearch}/>
        </div>
        <Categorybar catalogId={window.bcatalog?.id} categoryId={window.bcategoryId} categories={window.bcategories} search={window.bsearch}/>
        <CakeDetail cakes={window.bcakes} chosenCakes={cakesId} addProduct={addProduct} addPrice={addPrice}/>
    </div>
  )
}
