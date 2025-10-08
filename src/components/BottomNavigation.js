import React from 'react';
import { FaHome, FaShoppingCart, FaUser } from "react-icons/fa";

function BottomNavigation({ activeTab, onTabChange, cartItemsCount }) {
  const tabs = [
    { id: 'home', label: '', icon: <FaHome /> },
    { id: 'cart', label: '', icon: <FaShoppingCart /> },
    { id: 'account', label: '', icon: <FaUser /> }
  ];

  return (
    <nav className="bottom-navigation">
      {tabs.map(tab => (
        <button
          key={tab.id}
          className={`nav-item ${activeTab === tab.id ? 'active' : ''}`}
          onClick={() => onTabChange(tab.id)}
        >
          <div className="nav-icon">
            {tab.icon}
            {tab.id === 'cart' && cartItemsCount > 0 && (
              <span className="nav-badge">{cartItemsCount}</span>
            )}
          </div>
          <span className="nav-label">{tab.label}</span>
        </button>
      ))}
    </nav>
  );
}

export default BottomNavigation