import React from 'react'

export default function SerachForm({search}) {
  return (
    <form className="d-flex justify-content-end" method="get" asp-page-handler="OnGet">
        <div className="input-group mb-3">
            <input type="text" className="form-control" name="Search" placeholder="Nombre del Pastel" defaultValue={search}/>
            <button className="btn btn-outline-secondary" type="submit"><i className="bi bi-search"></i></button>
        </div>
    </form>
  )
}
