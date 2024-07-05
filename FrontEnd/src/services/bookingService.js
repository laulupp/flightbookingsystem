import axiosInstance from '../api/axiosInstance';
import { BOOKING_API } from '../api/endpoints';

export const getUserBookings = async (username) => {
    const response = await axiosInstance.get(BOOKING_API.GET_USER_BOOKINGS);
    return response.data;
};

export const addBooking = async (bookingData) => {
    console.log(bookingData)
    const response = await axiosInstance.post(BOOKING_API.BOOK_FLIGHT, bookingData);
    return response.data;
};

export const updateBooking = async (bookingData) => {
    const response = await axiosInstance.put(BOOKING_API.UPDATE_BOOKING, bookingData);
    return response.data;
};

export const cancelBooking = async (bookingId) => {
    const response = await axiosInstance.delete(BOOKING_API.CANCEL_BOOKING(bookingId));
    return response.data;
};
