import React, { useState } from 'react';
import axios from 'axios'; // Or use fetch

const LoginForm = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(''); // Clear error before trying to log in

        try {
            const response = await axios.post('https://localhost:44359/api/Login/auth', {
                username,
                password,
            });

            console.log('Login Response:', response.data); // Log the response

            if (response.data.success) {
                // Store user info in localStorage
                localStorage.setItem('user', JSON.stringify(response.data.data));
                console.log('User Data Saved in LocalStorage:', response.data.data); // Log user data
                // Redirect or update state as necessary
                window.location.href = '/dashboard';
            } else {
                setError(response.data.message || 'Login failed');
            }
        } catch (err) {
            console.error('Error logging in:', err);
            setError('An error occurred while logging in');
        }
    };

    return (
        <div>
            <h2>Login</h2>
            {error && <p style={{ color: 'red' }}>{error}</p>}
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Username:</label>
                    <input type="text" value={username} onChange={(e) => setUsername(e.target.value)} required />
                </div>
                <div>
                    <label>Password:</label>
                    <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                </div>
                <button type="submit">Login</button>
            </form>
        </div>
    );
};

export default LoginForm;
