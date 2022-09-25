import React, {useEffect, useState} from 'react'

export default function SummaryPage({cakesId={}}) {
  const [summaries, sertSummaries] = useState({});

  function summarize(){
    const summary = {};
    let oldQuantity = 0;

    Object.keys(cakesId).filter(cake => cakesId[cake] != null).map(cake => {
     
      cakesId[cake].ingredients.map(x => {

        oldQuantity = summary[x.productId] ? summary[x.productId].quantity : 0;

        summary[x.productId] = {
          productName: x.productName,
          quantity: (cakesId[cake].quantity * gramToPound(x.unit, x.quantity)) + oldQuantity,
          unit: 'l',
        };
      });
    });
    return summary;
  }

  function gramToPound(unit, quantity){
    if(unit == 'l') {
      return quantity;
    }

    return quantity * 0.00220462;
  }
  
  useEffect(function(){
    
    const result = summarize();
    if(Object.keys(result)){        
      sertSummaries(summarize());
    }
  }, [cakesId]);

  return (
    <div>
      <table className="table">
        <thead>
          <tr>
            <th scope="col">Producto</th>
            <th scope="col">Cantidad</th>
            <th scope="col">Unidad</th>
          </tr>
        </thead>
        <tbody>
          {
            Object.keys(summaries).map(x => (
              <tr key={x}>
                <th scope="row">{summaries[x].productName}</th>
                <td>{summaries[x].quantity}</td>
                <td>{summaries[x].unit}</td>
              </tr>
            ))
          }          
        </tbody>
      </table>
    </div>
  )
}
