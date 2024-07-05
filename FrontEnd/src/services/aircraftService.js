import axiosInstance from '../api/axiosInstance';
import { AIRCRAFT_API } from '../api/endpoints';

export const getCompanyAircrafts = async (companyId) => {
    const response = await axiosInstance.get(AIRCRAFT_API.GET_COMPANY_AIRCRAFTS(companyId));
    return response.data;
};

export const getAircraftById = async (companyId, aircraftId) => {
    const response = await axiosInstance.get(AIRCRAFT_API.GET_AIRCRAFT_BY_ID(companyId, aircraftId));
    return response.data;
};

export const addAircraft = async (aircraftData, companyId) => {
    const response = await axiosInstance.post(AIRCRAFT_API.ADD_AIRCRAFT(companyId), aircraftData);
    return response.data;
};

export const updateAircraft = async (companyId, aircraftId, aircraftData) => {
    const response = await axiosInstance.put(AIRCRAFT_API.UPDATE_AIRCRAFT(companyId, aircraftId), aircraftData);
    return response.data;
};

export const deleteAircraft = async (companyId, aircraftId) => {
    const response = await axiosInstance.delete(AIRCRAFT_API.DELETE_AIRCRAFT(companyId, aircraftId));
    return response.data;
};
