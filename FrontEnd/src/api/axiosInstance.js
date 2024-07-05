import axios from 'axios';
import { HEADER_NAMES } from '../utils/HeaderNames';
import { LOCAL_STORAGE_KEYS } from '../utils/LocalStorageKeys';

const axiosInstance = axios.create({
    headers: {
        'Content-Type': 'application/json',
    },
    validateStatus: () => true
});

axiosInstance.interceptors.request.use(
    config => {
        // Skip adding headers for login and registration endpoints
        console.log(config.url);
        if (!config.url.includes('/auth')) {
            const username = localStorage.getItem(LOCAL_STORAGE_KEYS.USERNAME);
            const token = localStorage.getItem(LOCAL_STORAGE_KEYS.TOKEN);
            if (username && token) {
                config.headers[HEADER_NAMES.USER] = username;
                config.headers[HEADER_NAMES.TOKEN] = token;
            }
        }
        return config;
    }
);

axiosInstance.interceptors.response.use(
    response => {
        return response;
    },
);

export default axiosInstance;
