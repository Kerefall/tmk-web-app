import React, { useState } from 'react';
import DOMPurify from 'dompurify';

const sanitizeText = (text) => {
  if (!text) return '';
  return DOMPurify.sanitize(text, {
    ALLOWED_TAGS: [],
    ALLOWED_ATTR: []
  });
};

function Item(props) {
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
        setQuantity(1);
    };

    const safeTitle = sanitizeText(props.item?.title);
    const safeDesc = sanitizeText(props.item?.desc);
    const safePrice = sanitizeText(props.item?.price?.toString());
    const safeDiameter = sanitizeText(props.item?.diameter);
    const safeWall = sanitizeText(props.item?.wall);
    const safeWarehouse = sanitizeText(props.item?.warehouse);

    return (
        <div className='item'>
            <h2 onClick={() => props.onShowItem(props.item)}>{safeTitle}</h2>
            <p>{safeDesc}</p>
            
            <div className="item-specs">
                <span className="spec">Диаметр: {safeDiameter}</span>
                <span className="spec">Стенка: {safeWall}</span>
                <span className="spec">Склад: {safeWarehouse}</span>
            </div>
            
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
            
            <b>{calculatePrice()} руб.</b>
            <div className='add-to-card' onClick={handleAddToCart}>+</div>
        </div>
    );
}

export default Item


// import React, { useState } from 'react';
// import DOMPurify from 'dompurify';

// const sanitizeText = (text) => {
//   if (!text) return '';
//   return DOMPurify.sanitize(text, {
//     ALLOWED_TAGS: [],
//     ALLOWED_ATTR: []
//   });
// };

// const validateImageUrl = (url) => {
//   if (!url) return './img/default.jpg';
  
//   if (url.startsWith('./img/') || url.startsWith('/img/')) {
//     return url;
//   }
  
//   if (url.startsWith('http://') || url.startsWith('https://')) {
//     return './img/default.jpg';
//   }
  
//   return './img/default.jpg';
// };

// function Item(props) {
//     const [unitType, setUnitType] = useState('tons');
//     const [quantity, setQuantity] = useState(1);
//     const [imageError, setImageError] = useState(false);

//     const calculatePrice = () => {
//         const basePrice = parseFloat(props.item.price);
//         if (unitType === 'tons') {
//             return (basePrice * quantity).toFixed(2);
//         } else {
//             return (basePrice * quantity / 1000).toFixed(2);
//         }
//     };

//     const handleAddToCart = () => {
//         const itemWithQuantity = {
//             ...props.item,
//             quantity: quantity,
//             unitType: unitType,
//             totalPrice: calculatePrice()
//         };
//         props.onAdd(itemWithQuantity);
//         setQuantity(1);
//     };

//     const handleImageError = () => {
//         setImageError(true);
//     };

//     const safeTitle = sanitizeText(props.item?.title);
//     const safeDesc = sanitizeText(props.item?.desc);
//     const safePrice = sanitizeText(props.item?.price?.toString());
//     const safeDiameter = sanitizeText(props.item?.diameter);
//     const safeWall = sanitizeText(props.item?.wall);
//     const safeWarehouse = sanitizeText(props.item?.warehouse);
//     const safeImage = imageError ? './img/default.jpg' : validateImageUrl(props.item?.img);

//     return (
//         <div className='item'>
//             <img 
//                 src={safeImage} 
//                 alt={safeTitle || 'Товар'}
//                 onError={handleImageError}
//                 loading="lazy"
//             />
            
//             <h2 onClick={() => props.onShowItem(props.item)}>{safeTitle}</h2>
//             <p>{safeDesc}</p>
            
//             <div className="item-specs">
//                 <span className="spec">Диаметр: {safeDiameter}</span>
//                 <span className="spec">Стенка: {safeWall}</span>
//                 <span className="spec">Склад: {safeWarehouse}</span>
//             </div>
            
//             <div className="quantity-selector">
//                 <label>Единица измерения:</label>
//                 <select value={unitType} onChange={(e) => setUnitType(e.target.value)}>
//                     <option value="tons">Тонны</option>
//                     <option value="meters">Метры</option>
//                 </select>
                
//                 <label>Количество:</label>
//                 <input 
//                     type="number" 
//                     min="0.1" 
//                     step="0.1"
//                     value={quantity}
//                     onChange={(e) => setQuantity(parseFloat(e.target.value) || 0.1)}
//                 />
//             </div>
            
//             <b>{calculatePrice()} руб.</b>
//             <div className='add-to-card' onClick={handleAddToCart}>+</div>
//         </div>
//     );
// }

// export default Item