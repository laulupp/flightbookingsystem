import axiosInstance from '../api/axiosInstance';
import { FLIGHT_API } from '../api/endpoints';

export const getAllFlights = async (params) => {
    const queryString = new URLSearchParams(params).toString();
    const response = await axiosInstance.get(`${FLIGHT_API.SEARCH_FLIGHTS}?${queryString}`);
    return response.data;
};

export const getAllCompanyFlights = async (companyId) => {
    const response = await axiosInstance.get(FLIGHT_API.GET_COMPANY_FLIGHTS(companyId));
    return response.data;
};

export const searchFlights = async (searchParams) => {
    const response = await axiosInstance.get(FLIGHT_API.SEARCH_FLIGHTS, { params: searchParams });
    return response.data;
};

export const addFlight = async (flightData) => {
    const response = await axiosInstance.post(FLIGHT_API.ADD_FLIGHT, flightData);
    return response;
};

export const updateFlight = async (flightId, flightData) => {
    const response = await axiosInstance.put(FLIGHT_API.UPDATE_FLIGHT(flightId), flightData);
    return response.data;
};

export const deleteFlight = async (flightId) => {
    const response = await axiosInstance.delete(FLIGHT_API.DELETE_FLIGHT(flightId));
    return response.data;
};
