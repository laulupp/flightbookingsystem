const API_BASE_URL = 'http://localhost:5000';

export const AUTH_API = {
    LOGIN: `${API_BASE_URL}/auth/login`,
    REGISTER: `${API_BASE_URL}/auth/register`,
    VERIFY_TOKEN: `${API_BASE_URL}/verifytoken`
};

export const USER_API = {
    GET_PROFILE: `${API_BASE_URL}/user/profile`,
    GET_ALL_USERS: `${API_BASE_URL}/user`,
    UPDATE_PROFILE: `${API_BASE_URL}/user`,
    CHANGE_PASSWORD: `${API_BASE_URL}/user/changepassword`,
    DELETE_USER: `${API_BASE_URL}/user`
};

export const COMPANY_API = {
    GET_ALL: `${API_BASE_URL}/companies`,
    REGISTER_COMPANY: `${API_BASE_URL}/companies/register`,
    GET_PENDING_REGISTRATIONS: `${API_BASE_URL}/companies/pending`,
    APPROVE_REGISTRATION: (companyId) => `${API_BASE_URL}/companies/${companyId}/approve`,
    REJECT_REGISTRATION: (companyId) => `${API_BASE_URL}/companies/${companyId}/reject`
};

export const FLIGHT_API = {
    GET_COMPANY_FLIGHTS: (companyId) => `${API_BASE_URL}/flights/company/${companyId}`,
    SEARCH_FLIGHTS: `${API_BASE_URL}/flights`,
    ADD_FLIGHT: `${API_BASE_URL}/flights`,
    UPDATE_FLIGHT: (flightId) => `${API_BASE_URL}/flights/${flightId}`,
    DELETE_FLIGHT: (flightId) => `${API_BASE_URL}/flights/${flightId}`
};

export const BOOKING_API = {
    GET_USER_BOOKINGS: `${API_BASE_URL}/bookings`,
    BOOK_FLIGHT: `${API_BASE_URL}/bookings`,
    UPDATE_BOOKING: (bookingId) => `${API_BASE_URL}/bookings/${bookingId}`,
    CANCEL_BOOKING: (bookingId) => `${API_BASE_URL}/bookings/${bookingId}`
};

export const AIRCRAFT_API = {
    GET_COMPANY_AIRCRAFTS: (companyId) => `${API_BASE_URL}/companies/${companyId}/aircrafts`,
    GET_AIRCRAFT_BY_ID: (companyId, aircraftId) => `${API_BASE_URL}/companies/${companyId}/aircrafts/${aircraftId}`,
    ADD_AIRCRAFT: (companyId) => `${API_BASE_URL}/companies/${companyId}/aircrafts`,
    UPDATE_AIRCRAFT: (companyId, aircraftId) => `${API_BASE_URL}/companies/${companyId}/aircrafts/${aircraftId}`,
    DELETE_AIRCRAFT: (companyId, aircraftId) => `${API_BASE_URL}/companies/${companyId}/aircrafts/${aircraftId}`
};

export const AIRPORT_API = {
    GET_ALL_AIRPORTS: `${API_BASE_URL}/airports`,
    GET_AIRPORT_BY_ID: (airportId) => `${API_BASE_URL}/airports/${airportId}`,
    ADD_AIRPORT: `${API_BASE_URL}/airports`,
    UPDATE_AIRPORT: `${API_BASE_URL}/airports`,
    DELETE_AIRPORT: (airportId) => `${API_BASE_URL}/airports/${airportId}`
};
