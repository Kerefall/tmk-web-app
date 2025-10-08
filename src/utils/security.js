
export const generateCSRFToken = () => {
  return Math.random().toString(36).substring(2, 15) + 
         Math.random().toString(36).substring(2, 15);
};


export const sanitizeFormData = (data) => {
  const sanitized = {};
  
  Object.keys(data).forEach(key => {
    let value = data[key];
    
    if (typeof value === 'string') {
      value = value
        .replace(/[<>]/g, '') 
        .substring(0, 1000); 
    }
    
    sanitized[key] = value;
  });
  
  return sanitized;
};