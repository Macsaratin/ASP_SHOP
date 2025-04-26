import api from './api';

const AuthService = {
    login: async (data) => {
        try {
            const response = await api.post('/Auth/login', data);
            if (response.data && response.data.token) {
                localStorage.setItem('token', response.data.token);
                // Store user data from response
                const userData = {
                    userId: response.data.userId,
                    email: response.data.email,
                    firstName: response.data.firstName,
                    lastName: response.data.lastName,
                    phoneNumber: response.data.phoneNumber || '',
                    role: response.data.role,
                    username: response.data.username || response.data.email || ''
                };

                localStorage.setItem('user', JSON.stringify(userData));
            }
            return response.data;
        } catch (error) {
            console.error('Login error:', error);
            throw new Error('Đăng nhập không thành công');
        }
    },

    register: async (data) => {
        return api.post('/Auth/register', data);
    },

    logout: () => {
        localStorage.removeItem('token');
        localStorage.removeItem('user');
    },

    fetchCurrentUser: async () => {
        try {
            const response = await api.get('/Users/me');

            if (response.data) {
                const userData = {
                    userId: response.data.id,
                    id: response.data.id,
                    email: response.data.email,
                    firstName: response.data.firstName,
                    lastName: response.data.lastName,
                    phoneNumber: response.data.phoneNumber || '',
                    role: response.data.role,
                    username: response.data.username || response.data.email || ''
                };

                localStorage.setItem('user', JSON.stringify(userData));
                return userData;
            }
            return null;
        } catch (error) {
            console.error('Error fetching user data:', error);
            return null;
        }
    },

    updateProfile: async (data) => {
        if (!data.username) {
            data.username = data.email;
        }

        const response = await api.put('/Users/profile', data);

        if (response.data) {
            const currentUser = AuthService.getCurrentUser();
            if (currentUser) {
                const updatedUser = {
                    ...currentUser,
                    firstName: data.firstName,
                    lastName: data.lastName,
                    phoneNumber: data.phoneNumber,
                    email: data.email,
                    username: data.username
                };
                localStorage.setItem('user', JSON.stringify(updatedUser));
            }
        }

        return response.data;
    },

    changePassword: async (data) => {
        return api.put('/Auth/change-password', data);
    },

    getCurrentUser: () => {
        const userStr = localStorage.getItem('user');
        if (userStr) {
            try {
                return JSON.parse(userStr);
            } catch (error) {
                console.error('Error parsing user data from localStorage:', error);
                return null;
            }
        }
        return null;
    },

    isLoggedIn: () => {
        return !!localStorage.getItem('token');
    }
};

export default AuthService;
