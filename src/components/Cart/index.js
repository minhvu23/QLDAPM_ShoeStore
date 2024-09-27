import React, { useEffect, useState } from 'react';
import { getCartItemsByCartId, getCartTotal, deleteCartItem, updateCart } from '~/apiServices/cartService';
import classNames from 'classnames/bind';
import styles from './Cart.module.scss'; 

const cx = classNames.bind(styles);

const Cart = () => {
    const cartId = 1; // Đặt cartId ở đây
    const [cartItems, setCartItems] = useState([]);
    const [cartTotal, setCartTotal] = useState(0);
    const [loading, setLoading] = useState(true); 

    useEffect(() => {
        const fetchCartItems = async () => {
            setLoading(true); 
            try {
                const items = await getCartItemsByCartId(1); // Sử dụng cartId
                setCartItems(items || []); 
            } catch (error) {
                console.error('Failed to fetch cart items:', error);
            } finally {
                setLoading(false); 
            }
        };

        const fetchCartTotal = async () => {
            try {
                const total = await getCartTotal(1); // Sử dụng cartId
                setCartTotal(total);
            } catch (error) {
                console.error('Failed to fetch cart total:', error);
            }
        };

        fetchCartItems();
        fetchCartTotal();
    }, [cartId]); // Theo dõi cartId nếu nó thay đổi

    const handleUpdateQuantity = async (productId, newQuantity) => {
        try {
            await updateCart(productId, newQuantity);
            const updatedItems = await getCartItemsByCartId(cartId); // Sử dụng cartId
            setCartItems(updatedItems);
        } catch (error) {
            console.error('Failed to update cart:', error);
        }
    };

    const handleDeleteCartItem = async (cartItemId) => {
        try {
            await deleteCartItem(cartItemId);
            const updatedItems = await getCartItemsByCartId(1); // Sử dụng cartId
            setCartItems(updatedItems);
        } catch (error) {
            console.error('Failed to delete cart item:', error);
        }
    };

    if (loading) {
        return <p>Loading...</p>; 
    }

    return (
        <div className={cx('wrapper')}>
            <h2>Your Cart</h2>
            {cartItems.length === 0 ? (
                <p>Your cart is empty.</p>
            ) : (
                <ul className={cx('cart-list')}>
                    {cartItems.map((item) => (
                        <li key={item.id} className={cx('cart-item')}>
                            <div className={cx('image')}>
                                <img src={item.productImageUrl} alt={item.productName} />
                            </div>
                            <div className={cx('details')}>
                                <h3 className={cx('product-name')}>{item.productName}</h3>
                                <p className={cx('price')}>Price: {item.price}</p>
                                <p className={cx('quantity')}>
                                    Quantity:
                                    <input
                                        type="number"
                                        value={item.quantity}
                                        onChange={(e) => handleUpdateQuantity(item.productId, e.target.value)}
                                    />
                                </p>
                                <button className={cx('remove-btn')} onClick={() => handleDeleteCartItem(item.id)}>
                                    Remove
                                </button>
                            </div>
                        </li>
                    ))}
                </ul>
            )}
            <h3>Total: {cartTotal}</h3>
        </div>
    );
};

export default Cart;
