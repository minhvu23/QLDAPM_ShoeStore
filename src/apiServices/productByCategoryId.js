import * as request from '~/utils/request';

export const getProductsByCategoryId = async (categoryId) => {
    try {
        const res = await request.get(`Product/category/${categoryId}`);
        return res.data;
    } catch (error) {
        console.error('Failed to fetch products: ', error);
    }
};
