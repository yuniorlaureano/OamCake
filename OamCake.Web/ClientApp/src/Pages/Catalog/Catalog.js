import React, { useState, useEffect } from "react";
import CatalogDetailForm from "./CatalogDetailForm";
import SerachForm from "./SerachForm";
import Categorybar from "./Categorybar";
import CakeDetail from "./CakeDetail";

export default function Ingredient() {
  const [productsId, setProductsId] = useState({});
  
  function addProduct(e, id){
    console.log(e.target.checked)
    if(e.target.checked) {
      setProductsId({...productsId, [`${id}`]: id})
    } else {
      setProductsId({...productsId, [`${id}`]: null})
    }
    
  }

  return (
    <div>
        <div className="row">
            <CatalogDetailForm catalog={window.bcatalog} productsId={productsId}/>
            <hr/>
            <SerachForm search={window.bsearch}/>
        </div>
        <Categorybar catalogId={window.bcatalog?.id} categoryId={window.bcategoryId} categories={window.bcategories} search={window.bsearch}/>
        <CakeDetail cakes={window.bcakes} addProduct={addProduct}/>
    </div>
  )
}
