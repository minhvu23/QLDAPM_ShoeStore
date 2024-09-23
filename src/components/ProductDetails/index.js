import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import classNames from 'classnames/bind';
import styles from './ProductDetails.module.scss';
import * as productDetail from '~/apiServices/productDetail';

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
                <button className={cx('btn-cart')}>Add to cart</button>
            </div>
        </div>
    );
}

export default ProductDetails;
