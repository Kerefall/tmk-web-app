import validator from 'validator';

export const SecurityUtils = {
  validateEmail: (email) => {
    return validator.isEmail(email) && 
           !validator.contains(email, ['<', '>', 'script', 'javascript']);
  },

  validatePrice: (price) => {
    const priceStr = price.toString();
    return validator.isNumeric(priceStr.replace('.', '')) && 
           parseFloat(price) > 0 &&
           parseFloat(price) < 1000000; 
  },

  validateText: (text, maxLength = 500) => {
    if (!text || typeof text !== 'string') return '';
    
    let clean = text
      .replace(/<script\b[^<]*(?:(?!<\/script>)<[^<]*)*<\/script>/gi, '')
      .replace(/javascript:/gi, '')
      .replace(/on\w+=/gi, '')
      .substring(0, maxLength);
    
    return clean;
  },

  sanitizeSearch: (searchTerm) => {
    return validator.whitelist(searchTerm, 'a-zA-Zа-яА-Я0-9\\s\\-\\.');
  }
};