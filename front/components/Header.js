import React from 'react'
import { FaSearch, FaFilter } from "react-icons/fa";

export default function Header(props) {
  const handleSearchChange = (e) => {
    props.onSearch(e.target.value);
  };

  return (
    <header>
        <div className="header-content">

            <div className="search-container">
              <FaSearch className="search-icon" />
              <input
                type="text"
                placeholder="Поиск товаров..."
                value={props.searchQuery}
                onChange={handleSearchChange}
                className="search-input"
              />
            </div>
            
            <FaFilter 
              onClick={props.onToggleAdvancedFilter} 
              className={`filter-icon ${props.showAdvancedFilter ? 'active' : ''}`}
            />
        </div>
    </header>
  );
}