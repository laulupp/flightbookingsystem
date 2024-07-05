import axiosInstance from '../api/axiosInstance';
import { AIRPORT_API } from '../api/endpoints';

export const getAllAirports = async () => {
    const response = await axiosInstance.get(AIRPORT_API.GET_ALL_AIRPORTS);
    return response.data;
};

export const getAirportById = async (airportId) => {
    const response = await axiosInstance.get(AIRPORT_API.GET_AIRPORT_BY_ID(airportId));
    return response.data;
};

export const addAirport = async (airportData) => {
    const response = await axiosInstance.post(AIRPORT_API.ADD_AIRPORT, airportData);
    return response;
};

export const updateAirport = async (airportData) => {
    const response = await axiosInstance.put(AIRPORT_API.UPDATE_AIRPORT, airportData);
    return response;
};

export const deleteAirport = async (airportId) => {
    const response = await axiosInstance.delete(AIRPORT_API.DELETE_AIRPORT(airportId));
    return response.data;
};
