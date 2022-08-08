import React, { useState, useEffect } from "react";
import CatalogDetailForm from "./CatalogDetailForm";
import SerachForm from "./SerachForm";
import Categorybar from "./Categorybar";
import CakeDetail from "./CakeDetail";

export default function Ingredient() {
  const [cakesId, setCakesId] = useState({});
  
  function addProduct(e, id){    
    if(e.target.checked) {
      setCakesId({...cakesId, [`${id}`]: id})
    } else {
      setCakesId({...cakesId, [`${id}`]: null})
    }    
  }

  function addPrice(e, id){   
    if(cakesId[id]) {

    }
    if(e.target.checked) {
      setCakesId({...cakesId, [`${id}`]: id})
    } else {
      setCakesId({...cakesId, [`${id}`]: null})
    }    
  }

  useEffect(() => {
    if(window.bcatalog?.cakesId) {
      var cakes = {};
      window.bcatalog.cakesId.forEach(id => {
        cakes[`${id}`] = id;
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
        <CakeDetail cakes={window.bcakes} addProduct={addProduct}/>
    </div>
  )
}
