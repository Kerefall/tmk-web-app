import React, { useState, useEffect } from 'react';

function Account({ userData, onUpdateUserData }) {
    const [isEditing, setIsEditing] = useState(false);
    const [formData, setFormData] = useState({
        lastName: '',
        firstName: '',
        middleName: '',
        inn: '',
        phone: '',
        email: ''
    });

    useEffect(() => {
        if (userData) {
            setFormData(userData);
        }
    }, [userData]);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));
    };

    const handleSave = () => {
        onUpdateUserData(formData);
        setIsEditing(false);
    };

    const handleCancel = () => {
        if (userData) {
            setFormData(userData);
        }
        setIsEditing(false);
    };

    const isFormValid = () => {
        return formData.lastName && 
               formData.firstName && 
               formData.inn && 
               formData.phone && 
               formData.email;
    };

    return (
        <div className="account-screen">
            <h2>Личный кабинет</h2>
            
            <div className="account-form">
                <div className="form-group">
                    <label>Фамилия:</label>
                    <input
                        type="text"
                        name="lastName"
                        value={formData.lastName}
                        onChange={handleInputChange}
                        disabled={!isEditing}
                        placeholder="Введите фамилию"
                    />
                </div>

                <div className="form-group">
                    <label>Имя:</label>
                    <input
                        type="text"
                        name="firstName"
                        value={formData.firstName}
                        onChange={handleInputChange}
                        disabled={!isEditing}
                        placeholder="Введите имя"
                    />
                </div>

                <div className="form-group">
                    <label>Отчество:</label>
                    <input
                        type="text"
                        name="middleName"
                        value={formData.middleName}
                        onChange={handleInputChange}
                        disabled={!isEditing}
                        placeholder="Введите отчество"
                    />
                </div>

                <div className="form-group">
                    <label>ИНН:</label>
                    <input
                        type="text"
                        name="inn"
                        value={formData.inn}
                        onChange={handleInputChange}
                        disabled={!isEditing}
                        placeholder="Введите ИНН"
                    />
                </div>

                <div className="form-group">
                    <label>Номер телефона:</label>
                    <input
                        type="tel"
                        name="phone"
                        value={formData.phone}
                        onChange={handleInputChange}
                        disabled={!isEditing}
                        placeholder="Введите номер телефона"
                    />
                </div>

                <div className="form-group">
                    <label>Email:</label>
                    <input
                        type="email"
                        name="email"
                        value={formData.email}
                        onChange={handleInputChange}
                        disabled={!isEditing}
                        placeholder="Введите email"
                    />
                </div>

                <div className="account-actions">
                    {!isEditing ? (
                        <button 
                            className="edit-btn"
                            onClick={() => setIsEditing(true)}
                        >
                            Редактировать данные
                        </button>
                    ) : (
                        <div className="edit-actions">
                            <button 
                                className="save-btn"
                                onClick={handleSave}
                                disabled={!isFormValid()}
                            >
                                Сохранить
                            </button>
                            <button 
                                className="cancel-btn"
                                onClick={handleCancel}
                            >
                                Отмена
                            </button>
                        </div>
                    )}
                </div>

                {!isFormValid() && isEditing && (
                    <div className="warning-message">
                        Заполните все обязательные поля (Фамилия, Имя, ИНН, Телефон, Email)
                    </div>
                )}
            </div>
        </div>
    );
}

export default Account