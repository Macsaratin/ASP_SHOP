import api from './api';

const ProductService = {
    getAllMovies: async () => {
        try {
            const response = await api.get('/Movies');
            return response.data;
        } catch (error) {
            console.error('Error fetching movies:', error);
            return [];
        }
    },

    getMovieById: async (id) => {
        try {
            const response = await api.get(`/Movies/${id}`);
            return response.data;
        } catch (error) {
            console.error(`Error fetching movie with id ${id}:`, error);
            return null;
        }
    },

    getNowShowingMovies: async () => {
        try {
            const response = await api.get('/Movies/now-showing');
            return response.data;
        } catch (error) {
            console.error('Error fetching now showing movies:', error);
            return [];
        }
    },

    getComingSoonMovies: async () => {
        try {
            const response = await api.get('/Movies/coming-soon');
            return response.data;
        } catch (error) {
            console.error('Error fetching coming soon movies:', error);
            return [];
        }
    },

    getPopularMovies: async () => {
        try {
            const response = await api.get('/Movies/popular');
            return response.data;
        } catch (error) {
            console.error('Error fetching popular movies:', error);
            return [];
        }
    },

    getScreeningsByMovie: async (movieId) => {
        const response = await api.get(`/Screenings/Movie/${movieId}`);
        return response.data;
    },
    getMoviesByGenre: async (genre) => {
        try {
            const response = await api.get(`/Movies/genre/${genre}`);
            return response.data;
        } catch (error) {
            console.error(`Error fetching movies for genre ${genre}:`, error);
            return [];
        }
    },
    getAllGenre: async () => {
        try {
            const response = await api.get('/Movies/genres');
            return response.data;
        } catch (error) {
            console.error('Error fetching movies:', error);
            return [];
        }
    },
};

export default ProductService;
