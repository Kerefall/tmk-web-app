import React, { useState, useEffect } from "react";
import Header from "./components/Header";
import Footer from "./components/Footer";
import Items from "./components/Items";
import Categories from "./components/Categories";
import ShowFullItem from "./components/ShowFullItem";
import BottomNavigation from "./components/BottomNavigation";
import AdvancedFilter from "./components/AdvancedFilter";
import Cart from "./components/Cart";
import Account from "./components/Account";

function App() {
  const [orders, setOrders] = useState([]);
  
  const [items] = useState([
    {
      id: 1,
      title: 'Труба сварная 50мм',
      desc: 'Сварная труба среднего диаметра',
      category: 'Сварные средние и малые',
      price: '100',
      material: 'steel',
      diameter: '50мм',
      wall: '3мм',
      gost: 'ГОСТ 10704-91',
      steelGrade: 'Ст3',
      warehouse: 'Москва',
      productType: 'Труба сварная'
    },
    {
      id: 2,
      title: 'Труба сварная 1020мм',
      desc: 'Сварная труба большого диаметра',
      category: 'Сварные большого диаметра',
      price: '200',
      material: 'steel',
      diameter: '1020мм',
      wall: '12мм',
      gost: 'ГОСТ 20295-85',
      steelGrade: 'Ст3',
      warehouse: 'Санкт-Петербург',
      productType: 'Труба сварная'
    }

  ]);
  
  const [currentItems, setCurrentItems] = useState([]);
  
  const [showFullItem, setShowFullItem] = useState(false);
  const [fullItem, setFullItem] = useState({});
  
  const [searchQuery, setSearchQuery] = useState('');
  const [activeTab, setActiveTab] = useState('home');
  const [showAdvancedFilter, setShowAdvancedFilter] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [advancedFilters, setAdvancedFilters] = useState({
    warehouses: [],
    productTypes: [],
    diameters: [],
    walls: [],
    gosts: [],
    steelGrades: [],
    priceRange: { min: 0, max: 1000 }
  });
  
  const [userData, setUserData] = useState({
    lastName: '',
    firstName: '',
    middleName: '',
    inn: '',
    phone: '',
    email: ''
  });

  useEffect(() => {
    setCurrentItems(items);
    
    const savedUserData = localStorage.getItem('userData');
    if (savedUserData) {
      setUserData(JSON.parse(savedUserData));
    }
  }, [items]);

  useEffect(() => {
    let filtered = items;
    
    if (searchQuery) {
      filtered = filtered.filter(item => 
        item.title.toLowerCase().includes(searchQuery.toLowerCase())
      );
    }
    
    if (selectedCategory && selectedCategory !== 'all') {
      filtered = filtered.filter(item => item.category === selectedCategory);
    }

    if (advancedFilters.warehouses.length > 0) {
      filtered = filtered.filter(item => 
        advancedFilters.warehouses.includes(item.warehouse)
      );
    }

    if (advancedFilters.productTypes.length > 0) {
      filtered = filtered.filter(item => 
        advancedFilters.productTypes.includes(item.productType)
      );
    }

    if (advancedFilters.diameters.length > 0) {
      filtered = filtered.filter(item => 
        advancedFilters.diameters.includes(item.diameter)
      );
    }

    if (advancedFilters.walls.length > 0) {
      filtered = filtered.filter(item => 
        advancedFilters.walls.includes(item.wall)
      );
    }

    if (advancedFilters.gosts.length > 0) {
      filtered = filtered.filter(item => 
        advancedFilters.gosts.includes(item.gost)
      );
    }

    if (advancedFilters.steelGrades.length > 0) {
      filtered = filtered.filter(item => 
        advancedFilters.steelGrades.includes(item.steelGrade)
      );
    }

    filtered = filtered.filter(item => 
      parseInt(item.price) >= advancedFilters.priceRange.min && 
      parseInt(item.price) <= advancedFilters.priceRange.max
    );

    setCurrentItems(filtered);
  }, [searchQuery, advancedFilters, items, selectedCategory]);

  const onShowItem = (item) => {
    setFullItem(item);
    setShowFullItem(!showFullItem);
  };

  const chooseCategory = (category) => {
    if (category === 'all') {
      setSelectedCategory(null);
      setCurrentItems(items);
      return;
    }
    setSelectedCategory(category);
  };


  const deleteOrder = (id) => {
    setOrders(orders.filter(el => el.id !== id));
  };


  const addToOrder = (item) => {

    const existingItemIndex = orders.findIndex(order => order.id === item.id);
    
    if (existingItemIndex !== -1) {

      const updatedOrders = [...orders];
      updatedOrders[existingItemIndex] = {
        ...updatedOrders[existingItemIndex],
        quantity: item.quantity,
        unitType: item.unitType,
        totalPrice: item.totalPrice
      };
      setOrders(updatedOrders);
    } else {

      setOrders([...orders, item]);
    }
  };

  const handleSearch = (query) => {
    setSearchQuery(query);
  };

  const handleAdvancedFilterChange = (newFilters) => {
    setAdvancedFilters(newFilters);
  };

  const toggleAdvancedFilter = () => {
    setShowAdvancedFilter(!showAdvancedFilter);
  };

  const updateUserData = (newUserData) => {
    setUserData(newUserData);
    localStorage.setItem('userData', JSON.stringify(newUserData));
  };

  const renderContent = () => {
    switch (activeTab) {
      case 'home':
        return (
          <>
            {!selectedCategory ? (
              <>
                <Categories chooseCategory={chooseCategory}/>
                {showAdvancedFilter && (
                  <AdvancedFilter 
                    items={items}
                    onFilterChange={handleAdvancedFilterChange}
                  />
                )}
              </>
            ) : (
              <>
                <div className="selected-category">
                  <h2>{selectedCategory}</h2>
                  <button 
                    onClick={() => setSelectedCategory(null)}
                    className="back-button"
                  >
                    ← Назад к категориям
                  </button>
                </div>
                {showAdvancedFilter && (
                  <AdvancedFilter 
                    items={items}
                    onFilterChange={handleAdvancedFilterChange}
                  />
                )}
                <Items 
                  onShowItem={onShowItem} 
                  items={currentItems} 
                  onAdd={addToOrder} 
                />
              </>
            )}
          </>
        );
      case 'cart':
        return (
          <Cart 
            orders={orders} 
            onDelete={deleteOrder}
            userData={userData}
            onShowItem={onShowItem} 
          />
        );
      case 'account':
        return (
          <Account 
            userData={userData} 
            onUpdateUserData={updateUserData}
          />
        );
      default:
        return (
          <>
            <Categories chooseCategory={chooseCategory}/>
            {showAdvancedFilter && (
              <AdvancedFilter 
                items={items}
                onFilterChange={handleAdvancedFilterChange}
              />
            )}
            <Items onShowItem={onShowItem} items={currentItems} onAdd={addToOrder} />
          </>
        );
    }
  };

  return (
    <div className="wrapper">
      <Header 
        orders={orders} 
        onDelete={deleteOrder}
        onSearch={handleSearch}
        searchQuery={searchQuery}
        onToggleAdvancedFilter={toggleAdvancedFilter}
        showAdvancedFilter={showAdvancedFilter}
      />
      
      {renderContent()}
      
      {showFullItem && (
        <ShowFullItem 
          onAdd={addToOrder} 
          onShowItem={onShowItem} 
          item={fullItem}
        />
      )}
      
      <BottomNavigation 
        activeTab={activeTab}
        onTabChange={setActiveTab}
        cartItemsCount={orders.length}
      />
      
      <Footer />
    </div>
  );
}

export default App