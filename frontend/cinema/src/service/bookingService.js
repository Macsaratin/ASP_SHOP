import api from './api';

const BookingService = {
    getAllUserBookings: async () => {
        const response = await api.get('/Bookings');
        return response.data;
    },

    getBookingById: async (id) => {
        const response = await api.get(`/Bookings/${id}`);
        return response.data;
    },

    createBooking: async (data) => {
        // data = { screeningId: number, seatIds: array }
        const response = await api.post('/Bookings', data);
        return response.data;
    },

    processPayment: async (data) => {
        // data = { bookingId: number, paymentMethod: string, paymentReference?: string }
        const response = await api.post('/Bookings/payment', data);
        return response.data;
    },

    cancelBooking: async (id) => {
        await api.put(`/Bookings/${id}/cancel`);
    }
};

export default BookingService;
