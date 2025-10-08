import React, { useState } from 'react'

function ShowFullItem(props) {
    const [unitType, setUnitType] = useState('tons');
    const [quantity, setQuantity] = useState(1);

    const calculatePrice = () => {
        const basePrice = parseFloat(props.item.price);
        if (unitType === 'tons') {
            return (basePrice * quantity).toFixed(2);
        } else {
            return (basePrice * quantity / 1000).toFixed(2);
        }
    };

    const handleAddToCart = () => {
        const itemWithQuantity = {
            ...props.item,
            quantity: quantity,
            unitType: unitType,
            totalPrice: calculatePrice()
        };
        props.onAdd(itemWithQuantity);
    };

    return (
        <div className='full-item' onClick={() => props.onShowItem(props.item)}>
            <div onClick={(e) => e.stopPropagation()}>
                <div className="full-item-header">
                    <h2>{props.item.title}</h2>
                    <button 
                        className="close-btn"
                        onClick={() => props.onShowItem(props.item)}
                    >
                        ×
                    </button>
                </div>
                
                <div className="full-item-content">
                    <div className="full-item-info">
                        <p><strong>Описание:</strong> {props.item.desc}</p>
                        <p><strong>Категория:</strong> {props.item.category}</p>
                        <p><strong>Материал:</strong> {props.item.material === 'steel' ? 'Сталь' : 'Нержавеющая сталь'}</p>
                        <p><strong>Диаметр:</strong> {props.item.diameter}</p>
                        <p><strong>Толщина стенки:</strong> {props.item.wall}</p>
                        <p><strong>ГОСТ:</strong> {props.item.gost}</p>
                        <p><strong>Марка стали:</strong> {props.item.steelGrade}</p>
                        <p><strong>Склад:</strong> {props.item.warehouse}</p>
                        <p><strong>Вид продукции:</strong> {props.item.productType}</p>
                    </div>

                    {/* <div className="full-item-purchase">
                        <div className="quantity-selector">
                            <label>Единица измерения:</label>
                            <select value={unitType} onChange={(e) => setUnitType(e.target.value)}>
                                <option value="tons">Тонны</option>
                                <option value="meters">Метры</option>
                            </select>
                            
                            <label>Количество:</label>
                            <input 
                                type="number" 
                                min="0.1" 
                                step="0.1"
                                value={quantity}
                                onChange={(e) => setQuantity(parseFloat(e.target.value) || 0.1)}
                            />
                        </div>
                        
                        <div className="full-item-price">
                            <b>Цена: {calculatePrice()} руб.</b>
                            <div className='add-to-card' onClick={handleAddToCart}>+</div>
                        </div>
                    </div> */}
                </div>
            </div>
        </div>
    );
}

export default ShowFullItem