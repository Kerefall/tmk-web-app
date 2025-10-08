import React, { useState, useMemo } from 'react';

function AdvancedFilter({ items, onFilterChange }) {
  const [filters, setFilters] = useState({
    warehouses: [],
    productTypes: [],
    diameters: [],
    walls: [],
    gosts: [],
    steelGrades: [],
    priceRange: { min: 0, max: 1000 }
  });

  const filterOptions = useMemo(() => {
    const warehouses = [...new Set(items.map(item => item.warehouse))];
    const productTypes = [...new Set(items.map(item => item.productType))];
    const diameters = [...new Set(items.map(item => item.diameter))];
    const walls = [...new Set(items.map(item => item.wall))];
    const gosts = [...new Set(items.map(item => item.gost))];
    const steelGrades = [...new Set(items.map(item => item.steelGrade))];
    const prices = items.map(item => parseInt(item.price));
    const minPrice = Math.min(...prices);
    const maxPrice = Math.max(...prices);

    return { 
      warehouses, 
      productTypes, 
      diameters, 
      walls, 
      gosts, 
      steelGrades, 
      minPrice, 
      maxPrice 
    };
  }, [items]);

  const handleFilterChange = (filterType, value) => {
    const newValues = filters[filterType].includes(value)
      ? filters[filterType].filter(item => item !== value)
      : [...filters[filterType], value];
    
    const newFilters = { ...filters, [filterType]: newValues };
    setFilters(newFilters);
    onFilterChange(newFilters);
  };

  const handlePriceChange = (min, max) => {
    const newFilters = { 
      ...filters, 
      priceRange: { min: parseInt(min), max: parseInt(max) } 
    };
    setFilters(newFilters);
    onFilterChange(newFilters);
  };

  const resetFilters = () => {
    const newFilters = {
      warehouses: [],
      productTypes: [],
      diameters: [],
      walls: [],
      gosts: [],
      steelGrades: [],
      priceRange: { min: 0, max: 1000 }
    };
    setFilters(newFilters);
    onFilterChange(newFilters);
  };

  return (
    <div className="advanced-filter">
      <div className="filter-content">
        <h3 className="filter-title">Расширенный фильтр</h3>
        
        <div className="filter-grid">
          <div className="filter-group">
            <label className="filter-label">Склад:</label>
            <div className="filter-options">
              {filterOptions.warehouses.map(warehouse => (
                <label key={warehouse} className="filter-option">
                  <input
                    type="checkbox"
                    checked={filters.warehouses.includes(warehouse)}
                    onChange={() => handleFilterChange('warehouses', warehouse)}
                  />
                  <span className="checkmark"></span>
                  {warehouse}
                </label>
              ))}
            </div>
          </div>

          <div className="filter-group">
            <label className="filter-label">Вид продукции:</label>
            <div className="filter-options">
              {filterOptions.productTypes.map(type => (
                <label key={type} className="filter-option">
                  <input
                    type="checkbox"
                    checked={filters.productTypes.includes(type)}
                    onChange={() => handleFilterChange('productTypes', type)}
                  />
                  <span className="checkmark"></span>
                  {type}
                </label>
              ))}
            </div>
          </div>

          <div className="filter-group">
            <label className="filter-label">Диаметр:</label>
            <div className="filter-options">
              {filterOptions.diameters.map(diameter => (
                <label key={diameter} className="filter-option">
                  <input
                    type="checkbox"
                    checked={filters.diameters.includes(diameter)}
                    onChange={() => handleFilterChange('diameters', diameter)}
                  />
                  <span className="checkmark"></span>
                  {diameter}
                </label>
              ))}
            </div>
          </div>

          <div className="filter-group">
            <label className="filter-label">Стенка:</label>
            <div className="filter-options">
              {filterOptions.walls.map(wall => (
                <label key={wall} className="filter-option">
                  <input
                    type="checkbox"
                    checked={filters.walls.includes(wall)}
                    onChange={() => handleFilterChange('walls', wall)}
                  />
                  <span className="checkmark"></span>
                  {wall}
                </label>
              ))}
            </div>
          </div>

          <div className="filter-group">
            <label className="filter-label">ГОСТ:</label>
            <div className="filter-options">
              {filterOptions.gosts.map(gost => (
                <label key={gost} className="filter-option">
                  <input
                    type="checkbox"
                    checked={filters.gosts.includes(gost)}
                    onChange={() => handleFilterChange('gosts', gost)}
                  />
                  <span className="checkmark"></span>
                  {gost}
                </label>
              ))}
            </div>
          </div>

          <div className="filter-group">
            <label className="filter-label">Марка стали:</label>
            <div className="filter-options">
              {filterOptions.steelGrades.map(grade => (
                <label key={grade} className="filter-option">
                  <input
                    type="checkbox"
                    checked={filters.steelGrades.includes(grade)}
                    onChange={() => handleFilterChange('steelGrades', grade)}
                  />
                  <span className="checkmark"></span>
                  {grade}
                </label>
              ))}
            </div>
          </div>

        </div>

        <div className="filter-group price-filter">
          <label className="filter-label">Цена: {filters.priceRange.min} - {filters.priceRange.max} руб.</label>
          <div className="price-range">
            <div className="price-inputs">
              <input
                type="number"
                value={filters.priceRange.min}
                onChange={(e) => handlePriceChange(e.target.value, filters.priceRange.max)}
                className="price-input"
                placeholder="Мин"
              />
              <span>-</span>
              <input
                type="number"
                value={filters.priceRange.max}
                onChange={(e) => handlePriceChange(filters.priceRange.min, e.target.value)}
                className="price-input"
                placeholder="Макс"
              />
            </div>
            <input
              type="range"
              min={filterOptions.minPrice}
              max={filterOptions.maxPrice}
              value={filters.priceRange.min}
              onChange={(e) => handlePriceChange(e.target.value, filters.priceRange.max)}
              className="price-slider"
            />
            <input
              type="range"
              min={filterOptions.minPrice}
              max={filterOptions.maxPrice}
              value={filters.priceRange.max}
              onChange={(e) => handlePriceChange(filters.priceRange.min, e.target.value)}
              className="price-slider"
            />
          </div>
        </div>

        <button onClick={resetFilters} className="reset-filters-btn">
          Сбросить фильтры
        </button>
      </div>
    </div>
  );
}

export default AdvancedFilter