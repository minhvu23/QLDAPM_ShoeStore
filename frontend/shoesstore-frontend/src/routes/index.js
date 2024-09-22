// Layouts
import { DefaultLayout, HeaderOnly } from '~/components/Layout';

import Home from '~/pages/Home';
import Following from '~/pages/Following';
import Profile from '~/pages/Profile';
import Upload from '~/pages/Upload';
import LoginForm from '~/pages/Login';
import Dashboard from '~/pages/Dashboard';

// Public routes
const publicRoutes = [
    { path: '/', component: Home, layout: DefaultLayout },
    { path: '/following', component: Following, layout: DefaultLayout },
    { path: '/profile', component: Profile, layout: DefaultLayout },
    { path: '/upload', component: Upload, layout: HeaderOnly },
    { path: '/login', component: LoginForm },
    { path: '/dashboard', component: Dashboard },
];

const privateRoutes = [];

export { publicRoutes, privateRoutes };
