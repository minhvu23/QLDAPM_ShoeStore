import * as request from '~/utils/request';

export const search = async (q, type = 'less') => {
    try {
        const res = await request.get('Product/search', {
            params: {
                q,
                type,
            },
        });
        return res.data;
    } catch (error) {
        console.log(error);
    }
};
