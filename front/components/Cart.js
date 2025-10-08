import React from 'react'
import Order from './Order'
import { FaCreditCard } from "react-icons/fa";

function Cart(props) {
    const totalPrice = props.orders.reduce((sum, el) => {
        return sum + parseFloat(el.totalPrice || el.price);
    }, 0);

    const canProceedToPayment = () => {
        return props.userData && 
               props.userData.lastName && 
               props.userData.firstName && 
               props.userData.inn && 
               props.userData.phone && 
               props.userData.email;
    };

    const handlePayment = () => {
        if (!canProceedToPayment()) {
            alert("Вы должны заполнить данные аккаунта, чтобы перейти к оплате");
            return;
        }
        
        alert(`Перенаправление к оплате на сумму: ${totalPrice.toFixed(2)} руб.`);
    };

    return (
        <div className="cart-screen">
            <h2>Корзина</h2>
            
            {props.orders.length === 0 ? (
                <div className="empty-cart">
                    <p>Корзина пуста</p>
                </div>
            ) : (
                <>
                    <div className="orders-list">
                        {props.orders.map(el => (
                            <Order 
                                key={el.id} 
                                item={el} 
                                onDelete={props.onDelete}
                                onShowItem={props.onShowItem}
                            />
                        ))}
                    </div>
                    
                    <div className="cart-summary">
                        <div className="total-price">
                            Итого: {totalPrice.toFixed(2)} руб.
                        </div>
                        
                        <button 
                            className="payment-btn"
                            onClick={handlePayment}
                        >
                            <FaCreditCard className="payment-icon" />
                            Перейти к оплате
                        </button>
                    </div>
                </>
            )}
        </div>
    );
}

export default Cart