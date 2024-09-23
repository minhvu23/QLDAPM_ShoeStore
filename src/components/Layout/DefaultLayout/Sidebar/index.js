import { useEffect, useState } from 'react';
import classNames from 'classnames/bind';
import styles from './Sidebar.module.scss';
import { getCategories } from '~/apiServices/categoryList';
import { Link } from 'react-router-dom';

const cx = classNames.bind(styles);

function Sidebar() {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        const fetchCategories = async () => {
            const data = await getCategories();
            setCategories(data);
        };
        fetchCategories();
    }, []);

    return (
        <aside className={cx('wrapper')}>
            <h3 className={cx('category')}>Shoes</h3>
            {categories.map((category) => (
                <Link to={`/Product/category/${category.categoryId}`} key={category.categoryId}>
                    <p className={cx('category-item')} key={category.categoryId}>
                        {category.name}
                    </p>
                </Link>
            ))}
        </aside>
    );
}

export default Sidebar;
