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

class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false, error: null };
  }
  
  static getDerivedStateFromError(error) {
    return { hasError: true, error };
  }
  
  componentDidCatch(error, errorInfo) {
    console.error('App Error:', error, errorInfo);
  }
  
  render() {
    if (this.state.hasError) {
      return (
        <div style={{ padding: '20px', textAlign: 'center' }}>
          <h2>Что-то пошло не так...</h2>
          <p>Мы уже работаем над исправлением проблемы</p>
          <button 
            onClick={() => this.setState({ hasError: false })}
            style={{ padding: '10px 20px', marginTop: '10px' }}
          >
            Попробовать снова
          </button>
        </div>
      );
    }
    
    return this.props.children;
  }
}

function App() {
  const [orders, setOrders] = useState([]);
  const [items, setItems] = useState([]);
  const [currentItems, setCurrentItems] = useState([]);
  const [showFullItem, setShowFullItem] = useState(false);
  const [fullItem, setFullItem] = useState({});
  const [searchQuery, setSearchQuery] = useState('');
  const [activeTab, setActiveTab] = useState('home');
  const [showAdvancedFilter, setShowAdvancedFilter] = useState(false);
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  
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

  const fallbackItems = [
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
  ];

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        
        const response = await fetch('/api/items');
        
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        
        const contentType = response.headers.get('content-type');
        if (!contentType || !contentType.includes('application/json')) {
          throw new Error('Server returned HTML instead of JSON');
        }
        
        const itemsData = await response.json();
        
        setItems(itemsData);
        setCurrentItems(itemsData);

      } catch (err) {
        console.log('Using fallback data:', err.message);
        setError(`Бекенд недоступен: ${err.message}`);
        setItems(fallbackItems);
        setCurrentItems(fallbackItems);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

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

  if (loading) {
    return (
      <div className="loading">
        <div className="loading-spinner"></div>
        <p>Загрузка товаров...</p>
      </div>
    );
  }

  const renderContent = () => {
    switch (activeTab) {
      case 'home':
        return (
          <>
            {error && (
              <div className="error">
                <p>{error}</p>
                <p>Используются демо-данные</p>
              </div>
            )}
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
            {error && (
              <div className="error">
                <p>{error}</p>
                <p>Используются демо-данные</p>
              </div>
            )}
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
    <ErrorBoundary>
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
    </ErrorBoundary>
  );
}

export default App