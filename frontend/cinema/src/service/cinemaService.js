import api from './api';

const CinemaService = {
    getAllCinemas: async () => {
        try {
            const response = await api.get('/Cinemas');
            return response.data;
        } catch (error) {
            console.error('Error fetching cinemas:', error);
            return [];
        }
    },

    getCinemaById: async (id) => {
        try {
            const response = await api.get(`/Cinemas/${id}`);
            return response.data;
        } catch (error) {
            console.error(`Error fetching cinema with id ${id}:`, error);
            return null;
        }
    },

    getScreeningsByCinema: async (cinemaId) => {
        const response = await api.get(`/Screenings/Cinema/${cinemaId}`);
        return response.data;
    }
};

export default CinemaService;
