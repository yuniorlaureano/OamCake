import React, {useState} from 'react'

const SELECTION_ALL = 1;
const SELECTION_SELECTED = 2;
const SELECTION_NOT_SELECTED = 3;

export default function CakeDetail({cakes, addProduct}) {
    const [selection, setSelection] = useState(0);

    function setSelectionHandler(e, option) {
        setSelection(option);
    }
  
  return (
    <div className="row">
        <div className='mb-3'>
            <div className="form-check form-check-inline">
                <input className="form-check-input" type="radio" name="panel-seleccion" id="selecionados-check" onChange={(e) => setSelectionHandler(e, SELECTION_SELECTED)} checked={selection == SELECTION_SELECTED}/>
                <label className="form-check-label" htmlFor="selecionados-check">Seleccionados</label>
            </div>
            <div className="form-check form-check-inline">
                <input className="form-check-input" type="radio" name="panel-seleccion" id="productos-check" onChange={(e) => setSelectionHandler(e, SELECTION_NOT_SELECTED)} checked={selection == SELECTION_NOT_SELECTED}/>
                <label className="form-check-label" htmlFor="productos-check">No selecionados</label>
            </div>
            <div className="form-check form-check-inline">
                <input className="form-check-input" type="radio" name="panel-seleccion" id="all-check" onChange={(e) => setSelectionHandler(e, SELECTION_ALL)} checked={selection == SELECTION_ALL}/>
                <label className="form-check-label" htmlFor="all-check">Todos</label>
            </div>
        </div>
        <hr/>
        <main className="catalog-cards">
            {
                cakes.map(x => (
                    <article key={x.id} className="catalog-card" style={{'position': 'relative'}}>
                        <img src={`/photos/${x.photo}`} alt="@cake.Name" />
                        <div className="text">
                            <h3>{x.name}</h3>
                            <h5>Example heading <span className="badge bg-secondary">{x.category?.name}</span></h5>
                            <i className="bi bi-trash3"></i>                            
                        </div>
                        <div className="form-check" style={{'position': 'absolute', 'bottom': '0'}}>
                            <input className="form-check-input" type="checkbox" name={`check-add-catalog-input-${x.id}`} id={`check-add-catalog-input-${x.id}`} onClick={(e) => addProduct(e, x.id)}/>
                            <label className="form-check-label" htmlFor={`check-add-catalog-input-${x.id}`}>
                                Agregar
                            </label>
                        </div>
                    </article>
                ))
            }
        </main>
    </div>
  )
}
