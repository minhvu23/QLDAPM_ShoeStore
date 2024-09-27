// Layouts
import { HeaderOnly } from '~/components/Layout';

// Pages
import Home from '~/pages/Home';
import Profile from '~/pages/Profile';
import Upload from '~/pages/Upload';
import Search from '~/pages/Search';
import ProductDetail from '~/pages/ProductDetail';
import Cart from '~/pages/Cart';
// Public routes
const publicRoutes = [
    { path: '/', component: Home },
    { path: '/Product/category/:categoryId', component: Home },
    { path: '/profile', component: Profile },
    { path: '/Product/:productId', component: ProductDetail, layout: HeaderOnly },
    { path: '/search', component: Search, layout: null },
    { path: '/Cart', component: Cart},
    
];

const privateRoutes = [];

export { publicRoutes, privateRoutes };
