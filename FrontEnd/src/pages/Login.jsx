import React, { useState } from 'react';
import CenteredPanel from '../components/CenteredPanel';
import InputTextField from '../components/InputTextField';
import MainButton from '../components/MainButton';
import { usePage } from '../components/PageProvider';
import '../style/pages/Login.css';
import { login } from '../services/authService';

function Login() {
    const { setCurrentPage } = usePage();
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    const handleLogin = async (event) => {
        event.preventDefault();
        setErrorMessage('');
        try {
            const response = await login(username, password);
            if (response.token) {
                setCurrentPage(2);
            } else {
                const errorData = response;
                setErrorMessage(errorData.error || 'Please complete all fields');
            }
        } catch (error) {
            console.log(error);
            setErrorMessage('Network error');
        }
    };

    return (
        <CenteredPanel>
            <h2 className="formTitle">Login</h2>
            <form noValidate onSubmit={handleLogin}>
                <InputTextField label="Username" value={username} onChange={(e) => setUsername(e.target.value)} />
                <InputTextField label="Password" type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                <MainButton type="submit" text="Sign in" />
                {errorMessage && <div className="error-message">{errorMessage}</div>}
                <span onClick={() => setCurrentPage(1)} className="formLink">Don't have an account? Sign Up</span>
            </form>
        </CenteredPanel>
    );
}

export default Login;
