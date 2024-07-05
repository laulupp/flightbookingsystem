import React, { createContext, useContext, useEffect, useState } from 'react';
import { login, register, verifyToken } from '../services/authService';
import { LOCAL_STORAGE_KEYS, clearLocalStorage } from '../utils/LocalStorageKeys';

const PageContext = createContext();

export const usePage = () => useContext(PageContext);

export const PageProvider = ({ children }) => {
    const [currentPage, setCurrentPage] = useState(0);

    useEffect(() => {
        console.log("11111111111111");
        const checkTokenValidity = async () => {
            console.log("22222222222");
            const token = localStorage.getItem(LOCAL_STORAGE_KEYS.TOKEN);
            const username = localStorage.getItem(LOCAL_STORAGE_KEYS.USERNAME);

            if (!token || !username) {
                console.log("33333333333");
                setCurrentPage(0);
                return;
            }

            try {
                const isValid = await verifyToken();
                if (isValid) {
                    setCurrentPage(2);
                    console.log("Previous token OK");
                } else {
                    clearLocalStorage();
                    setCurrentPage(0);
                    console.log("Previous token NOT OK");
                }
            } catch (error) {
                console.error(error);
                setCurrentPage(0);
            }
        };

        checkTokenValidity();
    }, []);

    const handleLogin = async (username, password) => {
        const userData = await login(username, password);
        if (userData.token) {
            setCurrentPage(2);
        }
    };

    const handleRegister = async (userData) => {
        const registeredUser = await register(userData);
        if (registeredUser.token) {
            setCurrentPage(2);
        }
    };

    return (
        <PageContext.Provider value={{ currentPage, setCurrentPage, handleLogin, handleRegister }}>
            {children}
        </PageContext.Provider>
    );
};
