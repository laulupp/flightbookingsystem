import axiosInstance from '../api/axiosInstance';
import { COMPANY_API } from '../api/endpoints';

export const registerCompany = async (companyData) => {
    const response = await axiosInstance.post(COMPANY_API.REGISTER_COMPANY, companyData);
    return response;
};

export const getPendingRequests = async () => {
    const response = await axiosInstance.get(COMPANY_API.GET_PENDING_REGISTRATIONS);
    return response.data;
};

export const getAllCompanies = async () => {
    const response = await axiosInstance.get(COMPANY_API.GET_ALL);
    return response.data;
};

export const approveRequest = async (companyId) => {
    const response = await axiosInstance.post(COMPANY_API.APPROVE_REGISTRATION(companyId));
    return response.data;
};

export const rejectRequest = async (companyId) => {
    const response = await axiosInstance.post(COMPANY_API.REJECT_REGISTRATION(companyId));
    return response.data;
};
