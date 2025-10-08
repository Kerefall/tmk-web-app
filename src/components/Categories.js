import React, { useState } from 'react'

function Categories(props) {
    const [categories] = useState([
        {
            key: 'all',
            name: 'Все',
        },
        {
            key: 'Сварные средние и малые',
            name: 'Сварные средние и малые'
        },
        {
            key: 'Сварные большого диаметра',
            name: 'Сварные большого диаметра'
        },
        {
            key: 'Горячедеформированные',
            name: 'Горячедеформированные'
        },
        {
            key: 'Холоднодеформированные',
            name: 'Холоднодеформированные'
        },
        {
            key: 'Профильные сварные',
            name: 'Профильные сварные'
        },
        {
            key: 'Из нержавеющей стали',
            name: 'Из нержавеющей стали'
        }
    ]);

    return (
        <div className='categories-grid'>
            {categories.map(el => (
                <div 
                    key={el.key} 
                    className='category-card'
                    onClick={() => props.chooseCategory(el.key)}
                >
                    <div className='category-icon'>{el.icon}</div>
                    <h3 className='category-name'>{el.name}</h3>
                </div>
            ))}
        </div>
    )
}

export default Categories