import { SecureStorage } from './secureStorage';

const API_BASE = 'https://your-backend-api.com'; 

export const secureFetch = async (endpoint, options = {}) => {
  const csrfToken = SecureStorage.get('csrf_token') || generateCSRFToken();
  SecureStorage.set('csrf_token', csrfToken, 24);
  
  const config = {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      'X-CSRF-Token': csrfToken,
      'X-Requested-With': 'XMLHttpRequest',
      ...options.headers,
    },
    credentials: 'same-origin'
  };
  
  try {
    const response = await fetch(`${API_BASE}${endpoint}`, config);
    
    if (!response.ok) {
      if (response.status === 401) {
        SecureStorage.remove('auth_token');
        window.location.href = '/login';
        throw new Error('Authentication required');
      }
      
      if (response.status >= 500) {
        throw new Error('Server error');
      }
      
      throw new Error(`Request failed: ${response.status}`);
    }
    
    const data = await response.json();
    
    if (data && typeof data === 'object') {
      return data;
    } else {
      throw new Error('Invalid response format');
    }
    
  } catch (error) {
    console.error('API request failed:', error);
    
    if (error.message.includes('NetworkError')) {
      throw new Error('Connection failed. Check your internet.');
    } else {
      throw new Error('Operation failed. Please try again.');
    }
  }
};

export const shopAPI = {
  getItems: () => secureFetch('/api/items'),
  
  addToCart: (itemId, quantity = 1) => 
    secureFetch('/api/cart', {
      method: 'POST',
      body: JSON.stringify({ itemId, quantity })
    }),
  
  checkout: (orderData) => 
    secureFetch('/api/checkout', {
      method: 'POST',
      body: JSON.stringify(orderData)
    })
};