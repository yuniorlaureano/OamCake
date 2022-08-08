import React, {useState, useEffect} from 'react'

export default function CatalogDetailForm({catalog, cakesId={}}) {
  const [isPublishedState, setIsPublishedState] = useState(false);

  const {id, description, isPublished } = catalog;

  function isPublishedHandler(e) {
    setIsPublishedState(e.target.checked);
  }

  function selectedCakeCaptions() {
    const selectedCakes = Object.keys(cakesId).length;
    if(selectedCakes > 1) {
        return `Tiene ${selectedCakes} seleccionados`;
    } else {
        return `Tiene ${selectedCakes} seleccionado`;
    }
  }

  useEffect(()=> {
    console.log(isPublished);
    setIsPublishedState(isPublished);
  }, []);
  
  return (
    <div className="card p-5">
        <div className="col-12">
            <h6 style={{'fontWeight': 'bold'}}>{selectedCakeCaptions()}</h6>
            <form method='POST'>
                {
                    Object.keys(cakesId).filter(x => cakesId[x] != null).map((x , i) => (
                        <input key={cakesId[x]} type="hidden" defaultValue={cakesId[x]} name={`Catalog.CakesId[${i}]`} />
                        // agregar el repcio aqui ver si es viable tener un solo input aqui.
                    ))
                }
                <input type="hidden" name="__RequestVerificationToken" defaultValue={window.antiForgeryToken}/>
                <input type="hidden" name="Catalog.Id" id="Catalog.Id" defaultValue={id} />
                <div className="row mb-3">
                    <label htmlFor="Catalog.Description" className="col-sm-2 col-form-label">Descripción</label>
                    <div className="col-sm-10">
                        <input type="text" name="Catalog.Description" className="form-control" defaultValue={description}/>
                    </div>
                </div>
                <div className="row mb-3">
                    <div className="col-sm-10 offset-sm-2">
                        <div className="form-check">
                            <input className="form-check-input" type="checkbox" name="Catalog.IsPublished" id="Catalog.IsPublished" onChange={isPublishedHandler} value={isPublishedState} checked={isPublishedState}/>
                            <label className="form-check-label" htmlFor="Catalog.IsPublished">
                                Está publicado
                            </label>
                        </div>
                    </div>
                </div>
                <button type="submit" className="btn btn-primary">Guardar</button>
            </form>
        </div>
    </div>
  )
}
