import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import classNames from 'classnames/bind';
import styles from './ProductDetails.module.scss';
import * as productDetail from '~/apiServices/productDetail';
import * as cartService from '~/apiServices/cartService';

const cx = classNames.bind(styles);

function ProductDetails() {
    const { productId } = useParams();
    const [product, setProduct] = useState(null); // Khởi tạo là null để kiểm tra dễ hơn

    useEffect(() => {
        const fetchProductDetail = async () => {
            try {
                const result = await productDetail.getProductDetail(productId);
                setProduct(result); // Giả sử result là một đối tượng sản phẩm
            } catch (error) {
                console.error('Failed to fetch product details: ', error);
            }
        };
        fetchProductDetail();
    }, [productId]);

    // Hàm xử lý thêm sản phẩm vào giỏ hàng
    const handleAddToCart = async () => {
        try {
            console.log('Button clicked!'); // Xác nhận sự kiện onClick
            console.log('Product:', product); // Kiểm tra dữ liệu sản phẩm
            if (!product) {
                throw new Error('Product not found');
            }

            // Giả sử cartId là 1, bạn có thể thay đổi theo logic thực tế
            const cartId = 1;
            const quantity = 1;
            const price = product.price;

            // Gọi API addToCart với đầy đủ thông tin
            const result = await cartService.addToCart(cartId, product.id, quantity, price);

            console.log('Add to cart result:', result); // Kiểm tra phản hồi
            if (result) {
                alert(`${product.name} has been added to the cart!`);
            }
        } catch (error) {
            console.error('Error adding to cart: ', error);
        }
    };

    if (!product) {
        return <p>Loading...</p>; // Hiển thị loading nếu sản phẩm chưa được tải
    }

    return (
        <div className={cx('wrapper')}>
            <div className={cx('image')}>
                <img src={product.imageUrl} className="rounded" alt={product.name} />
            </div>
            <div className={cx('container')}>
                <h2 className={cx('name')}>{product.name}</h2>
                <p className={cx('description')}>{product.description}</p>

                <p className={cx('price')}>Price: {product.price}</p>
                <p className={cx('quantity')}>Quantity: {product.quantity}</p>

                <button className={cx('btn-buy')}>Buy</button>
                {/* Nút thêm vào giỏ hàng */}
                <button className={cx('btn-cart')} onClick={handleAddToCart}>
                    Add to cart
                </button>
            </div>
        </div>
    );
}

export default ProductDetails;
