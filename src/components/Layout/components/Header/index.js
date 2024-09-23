import classNames from 'classnames/bind';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faCircleQuestion,
    faCoins,
    faEarthAsia,
    faGear,
    faKeyboard,
    faSignOut,
    faUser,
    faHeart,
    faCartShopping,
    faEllipsisVertical,
} from '@fortawesome/free-solid-svg-icons';
import Tippy from '@tippyjs/react';
import 'tippy.js/dist/tippy.css';
import styles from './Header.module.scss';
import images from '~/assets/images';
import { InboxIcon, MessageIcon, UploadIcon } from '~/components/Icons';
import Search from '../Search';
import Menu from '~/components/Popper/Menu';
import { Link } from 'react-router-dom';

const cx = classNames.bind(styles);

const MENU_ITEMS = [
    {
        icon: <FontAwesomeIcon icon={faEarthAsia} />,
        title: 'English',
        children: {
            title: 'Language',
            data: [
                {
                    type: 'language',
                    code: 'en',
                    title: 'English',
                },
                {
                    type: 'language',
                    code: 'vi',
                    title: 'Tiếng Việt',
                },
            ],
        },
    },
    {
        icon: <FontAwesomeIcon icon={faGear} />,
        title: 'Settings',
        to: '/settings',
    },
];

function Header() {
    const currentUser = true;

    // Handle logic
    const handleMenuChange = (menuItem) => {
        switch (menuItem.type) {
            case 'language':
                // Handle change language
                break;
            default:
        }
    };

    const userMenu = [
        {
            icon: <FontAwesomeIcon icon={faUser} />,
            title: 'View profile',
            to: '/@hoaa',
        },
        ...MENU_ITEMS,
        {
            icon: <FontAwesomeIcon icon={faSignOut} />,
            title: 'Log out',
            to: '/logout',
            separate: true,
        },
    ];

    return (
        <header className={cx('wrapper')}>
            <div className={cx('inner')}>
                <Link to={'/'}>
                    <img className={cx('logo')} src={images.logo} alt="Tiktok" />
                </Link>

                <Search />

                <div className={cx('actions')}>
                    {currentUser ? (
                        <></>
                    ) : (
                        <>
                            <Tippy elay={[0, 50]} content="Login" placement="bottom">
                                <button className={cx('more-btn')}>
                                    <FontAwesomeIcon icon={faUser} />
                                </button>
                            </Tippy>
                        </>
                    )}
                    <Tippy elay={[0, 50]} content="Favourite" placement="bottom">
                        <button className={cx('more-btn')}>
                            <FontAwesomeIcon icon={faHeart} />
                        </button>
                    </Tippy>

                    <Tippy elay={[0, 50]} content="Cart" placement="bottom">
                        <button className={cx('more-btn')}>
                            <FontAwesomeIcon icon={faCartShopping} />
                        </button>
                    </Tippy>

                    <Menu items={currentUser ? userMenu : MENU_ITEMS} onChange={handleMenuChange}>
                        {currentUser ? (
                            <img
                                className={cx('user-avatar')}
                                src="https://i.pinimg.com/564x/16/b2/e2/16b2e2579118bf6fba3b56523583117f.jpg"
                                alt="Nguyen Van A"
                            />
                        ) : (
                            <button className={cx('more-btn')}>
                                <FontAwesomeIcon icon={faEllipsisVertical} />
                            </button>
                        )}
                    </Menu>
                </div>
            </div>
        </header>
    );
}

export default Header;
