import { Link } from 'react-router-dom';
import classNames from 'classnames/bind';
import styles from './ProductItem.module.scss';


const cx = classNames.bind(styles);

function ProductItem({ data, children }) {
    return (
        <Link to={`/@${data.id}`} className={cx('wrapper')}>
            {children}
            <div className={cx('info')}>
                <h4 className={cx('name')}>
                    <span>{data.name}</span>
                </h4>
            </div>
        </Link>
    );
}

export default ProductItem;

