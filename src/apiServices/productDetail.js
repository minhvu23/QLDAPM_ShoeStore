import * as request from '~/utils/request';

export const getProductDetail = async (productId) => {
    try {
        const res = await request.get(`Product/find/${productId}`);
        return res.data;
    } catch (error) {
        console.log(error);
    }
};
