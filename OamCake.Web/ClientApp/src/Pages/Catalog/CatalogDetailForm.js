import React from 'react'

export default function CatalogDetailForm({catalog}) {
  
    const {id, description, isPublished } = catalog;

  return (
    <div className="card p-5">
        <div className="col-12">
            <form>
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
                            <input className="form-check-input" type="checkbox" name="Catalog.IsPublished" id="Catalog.IsPublished" defaultValue={isPublished}/>
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
