import React from 'react';

export default function Categorybar({categoryId, categories, search, catalogId}) {
  return (
    <nav aria-label="breadcrumb">
        <h4>Categoría</h4>
        <ol className="breadcrumb">
            {
                (categoryId == null || categoryId == 0)? 
                    (<li className="breadcrumb-item active" aria-current="page">Todos</li>) :
                    (<li className="breadcrumb-item"><a href={`/admin/catalog/add?Search=${search}`}>Todas</a></li>)
            }

            {
                categories.map(x => {
                    if(x.id == categoryId){
                        return <li key={x.id} className="breadcrumb-item active" aria-current="page">{x.name}</li>
                    } else {
                        return <li key={x.id} className="breadcrumb-item"><a href={`/admin/catalog/add?categoryId=${x.id}&Search=${search}&catalogId=${catalogId}`}>{x.name}</a></li>
                    }
                })
            }
        </ol>
    </nav>
  )
}
