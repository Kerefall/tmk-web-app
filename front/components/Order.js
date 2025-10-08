import React from 'react'
import { FaTrash } from "react-icons/fa";

function Order(props) {
    const displayQuantity = () => {
        if (props.item.quantity && props.item.unitType) {
            const unitText = props.item.unitType === 'tons' ? 'т' : 'м';
            return `${props.item.quantity} ${unitText}`;
        }
        return '1 шт';
    };

    return (
        <div className='order-item'>
            <div 
                className="order-details"
                onClick={() => props.onShowItem(props.item)}
                style={{cursor: 'pointer', flexGrow: 1}}
            >
                <h3>{props.item.title}</h3>
                <p className="order-quantity">{displayQuantity()}</p>
                <p className="order-price">{props.item.totalPrice || props.item.price} руб.</p>
                <div className="order-specs">
                    <span>Диаметр: {props.item.diameter}</span>
                    <span>Склад: {props.item.warehouse}</span>
                </div>
            </div>
            <FaTrash 
                className='delete-icon' 
                onClick={() => props.onDelete(props.item.id)}
                title="Удалить из корзины"
            />
        </div>
    );
}

export default Order