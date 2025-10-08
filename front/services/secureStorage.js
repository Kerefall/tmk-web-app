const APP_SECRET = 'my-shop-secret-2024';

export const SecureStorage = {
  set: (key, value, expiresInHours = 24) => {
    try {
      const item = {
        value: value,
        expiry: Date.now() + (expiresInHours * 60 * 60 * 1000),
        signature: btoa(value + APP_SECRET) 
      };
      localStorage.setItem(key, JSON.stringify(item));
      return true;
    } catch (error) {
      console.error('SecureStorage set error:', error);
      return false;
    }
  },

  get: (key) => {
    try {
      const itemStr = localStorage.getItem(key);
      if (!itemStr) return null;

      const item = JSON.parse(itemStr);
      
      if (Date.now() > item.expiry) {
        localStorage.removeItem(key);
        return null;
      }
      
      const expectedSignature = btoa(item.value + APP_SECRET);
      if (item.signature !== expectedSignature) {
        localStorage.removeItem(key);
        return null;
      }
      
      return item.value;
    } catch (error) {
      localStorage.removeItem(key);
      return null;
    }
  },

  remove: (key) => {
    try {
      localStorage.removeItem(key);
      return true;
    } catch (error) {
      return false;
    }
  }
};