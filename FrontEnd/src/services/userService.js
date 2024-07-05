import axiosInstance from '../api/axiosInstance';
import { USER_API } from '../api/endpoints';

export const getAllUsers = async () => {
    const response = await axiosInstance.get(USER_API.GET_ALL_USERS);
    return response.data;
};

export const getUserProfile = async () => {
    const response = await axiosInstance.get(USER_API.GET_PROFILE);
    return response.data;
};

export const updateUser = async (userData) => {
    const response = await axiosInstance.put(USER_API.UPDATE_PROFILE, userData);
    return response;
};

export const changePassword = async (passwordData) => {
    const response = await axiosInstance.put(USER_API.CHANGE_PASSWORD, passwordData);
    return response.data;
};

export const deleteUser = async (username) => {
    const response = await axiosInstance.delete(USER_API.DELETE_USER, { params: { username } });
    return response.data;
};
