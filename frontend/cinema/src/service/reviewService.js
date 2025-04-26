import api from './api';
import AuthService from './auth.service';

const ReviewService = {
    getReviewsByMovie: async (movieId) => {
        try {
            const storedReviews = localStorage.getItem('movie_reviews');
            const allReviews = storedReviews ? JSON.parse(storedReviews) : [];
            return allReviews.filter(review => review.movieId === movieId);
        } catch (error) {
            console.error(`Error fetching reviews for movie ${movieId}:`, error);
            return [];
        }
    },

    createReview: async (movieId, content, rating) => {
        try {
            const user = AuthService.getCurrentUser();
            if (!user) return null;

            const fullName = `${user.firstName || ''} ${user.lastName || ''}`.trim();
            let avatarUrl = user.avatarUrl || user.profileImage || user.photoURL || user.imageUrl || 'https://via.placeholder.com/150';

            const newReview = {
                id: Date.now(),
                movieId,
                userId: user.id,
                content,
                rating,
                createdAt: new Date().toISOString(),
                user: {
                    id: user.id,
                    name: fullName || user.email.split('@')[0],
                    avatarUrl
                }
            };

            const storedReviews = localStorage.getItem('movie_reviews');
            const allReviews = storedReviews ? JSON.parse(storedReviews) : [];
            allReviews.push(newReview);
            localStorage.setItem('movie_reviews', JSON.stringify(allReviews));

            return newReview;
        } catch (error) {
            console.error('Error creating review:', error);
            return null;
        }
    },

    updateReview: async (reviewId, content, rating) => {
        try {
            const storedReviews = localStorage.getItem('movie_reviews');
            if (!storedReviews) return null;

            const allReviews = JSON.parse(storedReviews);
            const reviewIndex = allReviews.findIndex(r => r.id === reviewId);
            if (reviewIndex === -1) return null;

            const user = AuthService.getCurrentUser();
            if (!user || allReviews[reviewIndex].userId !== user.id) return null;

            const fullName = `${user.firstName || ''} ${user.lastName || ''}`.trim();
            let avatarUrl = user.avatarUrl || user.profileImage || user.photoURL || user.imageUrl || 'https://via.placeholder.com/150';

            allReviews[reviewIndex] = {
                ...allReviews[reviewIndex],
                content,
                rating,
                updatedAt: new Date().toISOString(),
                user: {
                    ...allReviews[reviewIndex].user,
                    name: fullName || user.email.split('@')[0],
                    avatarUrl
                }
            };

            localStorage.setItem('movie_reviews', JSON.stringify(allReviews));
            return allReviews[reviewIndex];
        } catch (error) {
            console.error(`Error updating review ${reviewId}:`, error);
            return null;
        }
    },

    deleteReview: async (reviewId) => {
        try {
            const storedReviews = localStorage.getItem('movie_reviews');
            if (!storedReviews) return false;

            const allReviews = JSON.parse(storedReviews);
            const reviewToDelete = allReviews.find(r => r.id === reviewId);
            if (!reviewToDelete) return false;

            const user = AuthService.getCurrentUser();
            if (!user || reviewToDelete.userId !== user.id) return false;

            const updatedReviews = allReviews.filter(r => r.id !== reviewId);
            localStorage.setItem('movie_reviews', JSON.stringify(updatedReviews));

            return true;
        } catch (error) {
            console.error(`Error deleting review ${reviewId}:`, error);
            return false;
        }
    },

    getUserReviewForMovie: async (movieId) => {
        try {
            const user = AuthService.getCurrentUser();
            if (!user) return null;

            const storedReviews = localStorage.getItem('movie_reviews');
            if (!storedReviews) return null;

            const allReviews = JSON.parse(storedReviews);
            return allReviews.find(r => r.movieId === movieId && r.userId === user.id) || null;
        } catch (error) {
            console.error('Error checking user review:', error);
            return null;
        }
    }
};

export default ReviewService;
