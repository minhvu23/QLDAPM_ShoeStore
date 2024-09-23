import * as request from '~/utils/request';

export const getProducts = async () => {
    try {
        const res = await request.get(`Product/all`);
        return res.data;
    } catch (error) {
        console.error('Failed to fetch products: ', error);
    }
};
