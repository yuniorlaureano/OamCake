import React, { useState, useEffect } from "react";

export default function Ingredient() {
    const [productsState, setProductsState] = useState(window.products || []);
    const [ingredientsState, setIngredientsState] =  useState(window.ingredients || []);
    const [ingredient, setIngredient] =  useState({
        product: {}, unit: '', quantity: ''
    });

    function selectChange(e, input) {
        setIngredient({ ...ingredient, [input]: e.target.value});
    }

    function selectProductChange(e, input) {
        var product = productsState.find(x => x.id == e.target.value);
        setIngredient({ ...ingredient, [input]: product});
    }

    function addIngredient() {
        if(ingredient.unit && ingredient.product && ingredient.quantity) {
            var product = ingredientsState.find(x => x.productId == ingredient.product.id);
            if(!product) {
                setIngredientsState([ ...ingredientsState, {
                    productId: ingredient.product.id,
                    productName: ingredient.product.name + " " + ingredient.product.description,
                    unit: ingredient.unit,
                    quantity: ingredient.quantity
                }]);
            }
        }
    }

    function removeIngredient(index) {
        setIngredientsState(ingredientsState.filter((item, i) => i != index));
    }
  return (
    <div>
        <fieldset style={{border: 'solid 1px lightgray', 'paddingBottom': '4px'}}>
            <legend>Ingredientes</legend>
            <div className="row gx-3 gy-2 align-items-center mb-2">
                <div className="col-sm-6">
                    <select className="form-select" onChange={(e) => selectProductChange(e, "product")} value={ingredient.product.id}>
                        {
                            productsState.map(item => 
                                (<option key={item.id} value={item.id}>{ `${item.name} ${item.description}`}</option>)
                            )   
                        }
                    </select>
                </div>
                <div className="col-sm-5">
                    <div className="input-group">
                        <select className="form-select form-control-sm" onChange={(e) => selectChange(e, "unit")} value={ingredient.unit}>
                            <option value="">Unidad</option>
                            <option value="l">Libra</option>
                            <option value="g">Gramo</option>
                        </select>
                        <input type="number" className="form-control form-control-sm" placeholder="Unidad" onChange={(e) => selectChange(e, "quantity")} value={ingredient.quantity}/>
                    </div>
                </div>
                <div className="col-auto">
                    <button className="btn btn-info btn-sm" onClick={addIngredient} type="button"><i className="bi bi-plus-square"></i></button>
                </div>
            </div>
            <hr />
            <div>
                <div className="list-group">
                    {
                        ingredientsState.map((item, index) => (
                            <div key={`${index}-ingredient`} href="#" className="list-group-item list-group-item-action">
                                <input type="hidden" name={`CakeCreationDto.Ingredients[${index}].ProductId`} value={item.productId} />
                                <input type="hidden" name={`CakeCreationDto.Ingredients[${index}].Unit`} value={item.unit} />
                                <input type="hidden" name={`CakeCreationDto.Ingredients[${index}].Quantity`} value={item.quantity} />
                                <div className="d-flex w-100 justify-content-between">
                                    <h6 className="mb-1">{item.productName}</h6>
                                    <small>{`${item.quantity} ${item.unit}`} <button onClick={() => removeIngredient(index)} className="btn btn-danger btn-sm" type="button"><i className="bi bi-trash"></i></button></small>
                                </div>
                            </div>
                        ))
                    }
                </div>
            </div>
        </fieldset>

    </div>
  )
}
