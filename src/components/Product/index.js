import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import classNames from 'classnames/bind';
import styles from './Product.module.scss';
import * as productList from '~/apiServices/productList';
import * as productByCategoryId from '~/apiServices/productByCategoryId';

const cx = classNames.bind(styles);

function Product() {
    const { categoryId } = useParams();
    const [products, setProducts] = useState([]);

    useEffect(() => {
        const fetchProducts = async () => {
            try {
                const result = categoryId
                    ? await productByCategoryId.getProductsByCategoryId(categoryId)
                    : await productList.getProducts();

                setProducts(result || []);
            } catch (error) {
                console.error('Failed to fetch products: ', error);
            }
        };
        fetchProducts();
    }, [categoryId]);

    if (products.length === 0) {
        return <h1>Không có sản phẩm</h1>;
    }

    return (
        <div className={cx('product-list')}>
            {products.map((product) => (
                <Link to={`/Product/${product.productId}`} key={product.productId}>
                    <div className={cx('card')} style={{ width: '180px' }}>
                        <img className={cx('card-img-top')} src={product.imageUrl} alt={product.name} />
                        <div className={cx('card-body')}>
                            <h4 className={cx('card-title')}>{product.name}</h4>
                            <p className={cx('card-text')}>{product.description}</p>
                        </div>
                    </div>
                </Link>
            ))}
        </div>
    );
}

export default Product;
