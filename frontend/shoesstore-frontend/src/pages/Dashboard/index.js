import React from 'react';
import UserProfile from '~/components/Layout/components/UserProfile';

const handleLogout = () => {
    localStorage.removeItem('user');
    window.location.href = '/login'; // Redirect to login page
};

const Dashboard = () => {
    return (
        <div>
            <h1>Dashboard</h1>
            <UserProfile />
            <button onClick={handleLogout}>Logout</button>
        </div>
    );
};

export default Dashboard;
