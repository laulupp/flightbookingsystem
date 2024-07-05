import axiosInstance from '../api/axiosInstance';
import { AUTH_API } from '../api/endpoints';
import { HEADER_NAMES } from '../utils/HeaderNames';
import { LOCAL_STORAGE_KEYS } from '../utils/LocalStorageKeys';

export const login = async (username, password) => {
    const response = await axiosInstance.post(AUTH_API.LOGIN, { username, password });
    if (response.data.token) {
        localStorage.setItem(LOCAL_STORAGE_KEYS.USERNAME, response.data.username);
        localStorage.setItem(LOCAL_STORAGE_KEYS.TOKEN, response.data.token);
        localStorage.setItem(LOCAL_STORAGE_KEYS.FIRST_NAME, response.data.firstName);
        localStorage.setItem(LOCAL_STORAGE_KEYS.LAST_NAME, response.data.lastName);
        localStorage.setItem(LOCAL_STORAGE_KEYS.ROLE, response.data.role);
        localStorage.setItem(LOCAL_STORAGE_KEYS.COMPANY_STATUS, response.data.companyStatus);
        localStorage.setItem(LOCAL_STORAGE_KEYS.COMPANY_ID, response.data.companyId);
        localStorage.setItem(LOCAL_STORAGE_KEYS.USER_ID, response.data.userId);
    }
    return response.data;
};

export const register = async (userData) => {
    const response = await axiosInstance.post(AUTH_API.REGISTER, userData);
    if (response.data.token) {
        localStorage.setItem(LOCAL_STORAGE_KEYS.USERNAME, response.data.username);
        localStorage.setItem(LOCAL_STORAGE_KEYS.TOKEN, response.data.token);
        localStorage.setItem(LOCAL_STORAGE_KEYS.FIRST_NAME, response.data.firstName);
        localStorage.setItem(LOCAL_STORAGE_KEYS.LAST_NAME, response.data.lastName);
        localStorage.setItem(LOCAL_STORAGE_KEYS.ROLE, response.data.role);
        localStorage.setItem(LOCAL_STORAGE_KEYS.COMPANY_STATUS, 0);
        localStorage.setItem(LOCAL_STORAGE_KEYS.USER_ID, response.data.userId);
    }
    return response.data;
};

export const verifyToken = async () => {
    const response = await axiosInstance.get(AUTH_API.VERIFY_TOKEN, { "1": 1 });

    return response.status == 200;
}