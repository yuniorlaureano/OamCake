import React, {useState, useEffect} from "react"

function totalKakes(cart){
    let total = 0;
    let price = 0;
    let cartKeys = Object.keys(cart)
    for(let i = 0; i < cartKeys.length; i++) {
        total += cart[cartKeys[i]].qty;
        price += cart[cartKeys[i]].price * cart[cartKeys[i]].qty;
    }
    return {
        price: price,
        total: total
    };
}

export default function Checkout() {
    const [cart, setCart] = useState({});
    const [totalProduct, setTotalProduct] = useState({
        price: 0,
        total: 0
    });

    function cleanLocalStorage() {
        console.log('joder')
        window.localStorage.removeItem('oam_cake_cart')
    }

    useEffect(function() {
        let tempCart = JSON.parse(window.localStorage.getItem('oam_cake_cart'));
        setTotalProduct(totalKakes(tempCart))
        setCart(tempCart);
        console.log(tempCart);
    }, [])
    
    return (
        <div>
           <table className="table">
                <thead>
                    <tr>
                        <th>Cake</th>
                        <th>Cantidad</th>
                        <th>$</th>
                    </tr>
                </thead>
                <tbody>
                    {
                        Object.keys(cart).map((k,i) => (
                            <tr key={i}>
                                <td>{cart[k]?.cakeName}</td>
                                <td>{cart[k]?.qty}</td> 
                                <td>{cart[k]?.qty * cart[k]?.price}</td>
                            </tr>
                        ))
                    }
                </tbody>
                <tfoot>
                    <tr>
                        <th>Total</th>
                        <td>{totalProduct.total}</td>
                        <td>{totalProduct.price}</td>
                    </tr>
                    <tr>
                        <th></th>
                        <td></td>
                        <td>
                            <form method="POST" onSubmit={cleanLocalStorage}>
                                <input type="hidden" name="__RequestVerificationToken" defaultValue={window.antiForgeryToken}/>
                                {
                                    Object.keys(cart).map((k,i) => (
                                        <div  key={i}>
                                            <input type="hidden" name={`order[${i}].cakeId`} defaultValue={`${cart[k].cakeId}`} />
                                            <input type="hidden" name={`order[${i}].price`} defaultValue={`${cart[k].price}`} />
                                            <input type="hidden" name={`order[${i}].qty`} defaultValue={`${cart[k].qty}`} />
                                        </div>
                                    ))
                                }
                                <button className="btn btn-primary">Pagar</button>
                            </form>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    )
}