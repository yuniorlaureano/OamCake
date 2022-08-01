import React, { useState, useEffect } from "react";
import CatalogDetailForm from "./CatalogDetailForm";
import SerachForm from "./SerachForm";
import CatalogBar from "./CatalogBar";
import Categorybar from "./Categorybar";
import CakeDetail from "./CakeDetail";

export default function Ingredient() {
    
  return (
    <div>
        <div className="row">
            <CatalogDetailForm catalog={window.bcatalog}/>
            <hr/>
            <SerachForm search={window.bsearch}/>
        </div>
        <Categorybar catalogId={window.bcatalog?.id} categoryId={window.bcategoryId} categories={window.bcategories} search={window.bsearch}/>
        <CatalogBar catalogId={window.bcatalog?.id} categoryId={window.bcategoryId} search={window.bsearch}/>
        <CakeDetail cakes={window.bcakes}/>
    </div>
  )
}
