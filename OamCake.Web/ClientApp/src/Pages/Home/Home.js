import React, {useState, useEffect} from "react"
import CakeList from "./CakeList"
import Modal from "../../components/Modal";

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

function cartToSave(cart){
    let cartKeys = Object.keys(cart)
    let cartToSave = {}
    for(let i = 0; i < cartKeys.length; i++) {
        if(cart[cartKeys[i]].qty > 0) {
            cartToSave[cartKeys[i]] = cart[cartKeys[i]];
        }
        
    }
    return cartToSave;
}

export default function Home({bcakes, savedCart}) {
    const [cart, setCart] = useState(savedCart);
    const [totalProduct, setTotalProduct] = useState({
        price: 0,
        total: 0
    });
    const [modalShow, setModalShow] = useState(false);

    function add(cake){
        let tempQty = (cart[cake.id]?.qty || 0);
        let tempCart = {
            ...cart,
            [cake.id]: { 
                ...cake,
                qty: tempQty + 1
            },
        };
        setCart(cartToSave(tempCart))
    }

    function substract(cake){
        let tempQty = (cart[cake.id]?.qty || 1) - 1;
        tempQty = tempQty < 0 ? 0 : tempQty;
        let tempCart = {
            ...cart,
            [cake.id]: { 
                ...cake,
                qty: tempQty
            },
        };
        setCart(cartToSave(tempCart))
    }

    function showModal() {
        setModalShow(true)
    }

    function hideModal() {
        setModalShow(false)
    }

    useEffect(function() {
        setTotalProduct(totalKakes(cart))
        window.localStorage.setItem('oam_cake_cart', JSON.stringify(cart))
    }, [cart])
    
    return (
        <div>
            <Modal show={modalShow} hide={hideModal} heading={<i className="bi bi-cart"></i>} buttons={[
                <a className="btn btn-primary" key="1" href="/Checkout">Comprar</a>
                // <button key="2" type="button" className="btn btn-primary">Save changes</button>
            ]}>
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
                    </tfoot>
                </table>
            </Modal>
            <button className="btn btn-default" onClick={showModal}>
                Pedidos <i className="bi bi-cart"></i> {totalProduct.total}
            </button>
            |
            <button className="btn btn-default" onClick={showModal}>
                Mis pedidos
            </button>
            <CakeList bcakes={bcakes} add={add} substract={substract} cart={cart}/>
        </div>
    )
}