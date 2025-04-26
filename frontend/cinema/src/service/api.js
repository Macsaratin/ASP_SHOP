import axios from 'axios';

// Địa chỉ API backend (chỉnh nếu cần)
const API_URL = 'http://localhost:5246/api';
// Hoặc dùng HTTPS nếu backend hỗ trợ
// const API_URL = 'https://localhost:7080/api';

const api = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

// Thêm interceptor cho request để đính kèm token nếu có
api.interceptors.request.use(
    function (config) {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    function (error) {
        return Promise.reject(error);
    }
);

// Thêm interceptor cho response để xử lý token hết hạn
api.interceptors.response.use(
    function (response) {
        return response;
    },
    function (error) {
        if (error.response && error.response.status === 401) {
            localStorage.removeItem('token');
            window.location.href = '/login';
        }
        return Promise.reject(error);
    }
);

export default api;
