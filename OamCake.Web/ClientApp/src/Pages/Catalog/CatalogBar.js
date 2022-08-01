import React from 'react'

export default function CatalogBar({categoryId,search,catalogId}) {
  return (
    <nav aria-label="breadcrumb">
        <h4>Origen</h4>
        <ol className="breadcrumb">
            <li className="breadcrumb-item"><a href={`/admin/catalog/add?categoryId=${categoryId}&Search=${search}&catalogid=${catalogId}`}>En catálogo</a></li>
            <li className="breadcrumb-item"><a href={`/admin/catalog/add?categoryId=${categoryId}&Search=${search}&catalogid=${catalogId}`}>Productos</a></li>
        </ol>
    </nav>
  )
}
