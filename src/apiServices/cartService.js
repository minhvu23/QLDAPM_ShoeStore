import * as request from '~/utils/request';


// Đặt cartId mặc định là 1
const cartId = 1;

// Lấy sản phẩm trong giỏ hàng theo cartId
export const getCartItemsByCartId = async () => {
    try {
        const response = await request.get(`/api/Cart/{cartId}/items`);
        return response.data; // Giả sử API trả về danh sách sản phẩm
    } catch (error) {
        console.error('Failed to fetch cart items:', error);
        throw error;
    }
};

// Lấy tổng giá trị của giỏ hàng
export const getCartTotal = async () => {
    try {
        const response = await request.get(`/api/Cart/{cartId}/total`);
        return response.data; // Giả sử API trả về tổng giá trị
    } catch (error) {
        console.error('Failed to fetch cart total:', error);
        throw error;
    }
};

// Thêm sản phẩm vào giỏ hàng
export const addToCart = async (cartId, productId, quantity, price) => {
    try {
        const response = await request.post(`/Cart/add-to-cart`, {
            cartId,
            productId,
            quantity,
            price
        });
        return response.data;
    } catch (error) {
        console.error('Failed to add to cart: ', error);
        throw error;
    }
};

// Cập nhật giỏ hàng
export const updateCart = async (productId, quantity) => {
    try {
        const res = await request.put('/Cart/update-cart', {
            cartId,
            productId,
            quantity,
        });
        return res.data;
    } catch (error) {
        console.error('Failed to update cart: ', error);
        throw error;
    }
};

// Xóa sản phẩm khỏi giỏ hàng
export const deleteCartItem = async (cartItemId) => {
    try {
        const res = await request.deleteRequest(`/Cart/delete-cart-item/${cartItemId}`);
        return res.data;
    } catch (error) {
        console.error('Failed to delete cart item: ', error);
        throw error;
    }
};
