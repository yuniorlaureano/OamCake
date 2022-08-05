import React, {useState, useEffect} from 'react'

export default function CatalogDetailForm({catalog, productsId={}}) {
  const [isPublishedState, setIsPublishedState] = useState(false);

  const {id, description, isPublished } = catalog;

  function isPublishedHandler(e) {
    setIsPublishedState(e.target.checked);
  }

  useEffect(()=> {
    setIsPublishedState(isPublished);
  }, []);
  
  return (
    <div className="card p-5">
        <div className="col-12">
            <form method='POST'>
                {
                    Object.keys(productsId).filter(x => productsId[x] != null).map((x , i) => (
                        <input key={productsId[x]} type="hidden" defaultValue={productsId[x]} name={`Catalog.ProductsId[${i}]`} />
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
                            <input className="form-check-input" type="checkbox" name="Catalog.IsPublished" id="Catalog.IsPublished" onChange={isPublishedHandler} value={isPublishedState}/>
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
