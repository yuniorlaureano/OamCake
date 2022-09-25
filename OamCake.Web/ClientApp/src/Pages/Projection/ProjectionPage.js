import React from 'react'

export default function ProjectionPage({cakes={},onUpdateQuantity}) {
  return (
    <table className='table table-bordered'>
        {
            Object.keys(cakes).filter(x => cakes[x] != null).map(x => (
                <tbody key={`cake-${cakes[x].id}`}>
                    <tr>
                        <td >{cakes[x].name + " | " + cakes[x].categoryName}
                            <input type="number" className="form-control" style={{width: '100px', display: 'inline-block', margin: '5px'}} onChange={(e) => onUpdateQuantity(cakes[x].id, e.target.value || 1)} value={cakes[x].quantity || "1"}/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table>
                                {
                                    cakes[x].ingredients.map((y,i) => (
                                        <tr key={`cake-${cakes[x].id}-${i}`}>
                                            <td>{y.productName}</td>
                                            <td>{`${y.quantity * cakes[x].quantity} ${y.unit}`}</td>
                                        </tr>
                                    ))
                                }                                    
                            </table>
                        </td>
                    </tr>
                </tbody>

                // <div className="accordion-item" key={`cake-${cakes[x].id}`}>
                //     <h2 className="accordion-header" id="flush-headingOne">
                //         <button className="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target={"#flush-collapse-" + cakes[x].id} aria-expanded="false" aria-controls={"flush-collapse-" + cakes[x].id}>
                //             {cakes[x].name + " | " + cakes[x].categoryName + "  $" + cakes[x].price}
                //         </button>
                //         <input type="number" className="form-control" style={{width: '100px'}} onChange={(e) => onUpdateQuantity(cakes[x].id, e.target.value || 1)} value={cakes[x].quantity || "1"}/>
                //     </h2>
                //     <div id={"flush-collapse-" + cakes[x].id} className="accordion-collapse collapse" aria-labelledby="flush-headingOne" data-bs-parent="#projection-accordion">
                //         <ul className="list-group">
                //             {
                //                 cakes[x].ingredients.map((y,i) => (
                //                     <li key={i} className="list-group-item d-flex justify-content-between align-items-center">
                //                         {y.productName}
                //                         <span className="badge bg-primary rounded-pill">{`${y.quantity * cakes[x].quantity} ${y.unit}`}</span>
                //                     </li>
                //                 ))
                //             }
                //         </ul>
                //     </div>




                    
                // </div>
            ))
        }
   </table>
  )
}
