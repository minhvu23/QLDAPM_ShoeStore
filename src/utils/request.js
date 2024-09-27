import axios from 'axios';

const request = axios.create({
    baseURL: 'https://localhost:44365/api/',
});

// Hàm GET
export const get = async (path, options = {}) => {
    const response = await request.get(path, options);
    return response.data;
};

// Hàm POST
export const post = async (path, data, options = {}) => {
    const response = await request.post(path, data, options);
    return response.data;
};

// Hàm PUT
export const put = async (path, data, options = {}) => {
    const response = await request.put(path, data, options);
    return response.data;
};

// Hàm DELETE
export const deleteRequest = async (path, options = {}) => {
    const response = await request.delete(path, options);
    return response.data;
};

// Xuất các hàm
