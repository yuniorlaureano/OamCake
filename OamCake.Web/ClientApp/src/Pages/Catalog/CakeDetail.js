import React from 'react'

export default function CakeDetail({cakes}) {
  return (
    <div className="row">
        <main className="catalog-cards">
            {
                cakes.map(x => (
                    <article key={x.id} className="catalog-card">
                        <img src={`/photos/${x.photo}`} alt="@cake.Name" />
                        <div className="text">
                            <h3>{x.name}</h3>
                            <h5>Example heading <span className="badge bg-secondary">{x.category?.name}</span></h5>
                            <i className="bi bi-trash3"></i>
                        </div>
                    </article>
                ))
            }
        </main>
    </div>
  )
}
