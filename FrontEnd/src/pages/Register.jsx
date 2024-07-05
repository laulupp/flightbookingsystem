import React, { useState } from 'react';
import CenteredPanel from '../components/CenteredPanel';
import InputTextField from '../components/InputTextField';
import MainButton from '../components/MainButton';
import { usePage } from '../components/PageProvider';
import '../style/pages/Register.css';
import { register } from '../services/authService';

function Register() {
    const { setCurrentPage } = usePage();
    const [formData, setFormData] = useState({
        username: '',
        email: '',
        firstName: '',
        lastName: '',
        phoneNumber: '',
        password: '',
        repeatPassword: '',
        isCompany: false
    });
    const [errorMessage, setErrorMessage] = useState('');

    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData({ ...formData, [name]: type === 'checkbox' ? checked : value });
    };

    const handleRegister = async (event) => {
        event.preventDefault();
        if (formData.password !== formData.repeatPassword) {
            setErrorMessage("Passwords do not match.");
            return;
        }

        try {
            const data = await register({
                username: formData.username,
                email: formData.email,
                firstName: formData.firstName,
                lastName: formData.lastName,
                phoneNumber: formData.phoneNumber,
                password: formData.password,
                isCompany: formData.isCompany
            });

            if (data.token) {
                setCurrentPage(2);
            } else {
                const errorData = data;
                if (errorData.errors) {
                    let errors = Object.entries(errorData.errors).map(([key, messages]) => {
                        if (key === "PhoneNumber") return "The Phone Number has an invalid format.";
                        return messages.join('\n');
                    });
                    setErrorMessage(errors.join('\n'));
                } else {
                    setErrorMessage(errorData.error || 'Please complete all fields');
                }
            }
        } catch (error) {
            console.log(error);
            setErrorMessage('Network error. Please try again later.');
        }
    };

    return (
        <CenteredPanel containerHeight="900">
            <h2 className="formTitle">Register</h2>
            <form noValidate onSubmit={handleRegister}>
                <InputTextField label="Username" name="username" value={formData.username} onChange={handleInputChange} required />
                <InputTextField label="Email" name="email" value={formData.email} onChange={handleInputChange} type="email" required />
                <InputTextField label="First Name" name="firstName" value={formData.firstName} onChange={handleInputChange} required />
                <InputTextField label="Last Name" name="lastName" value={formData.lastName} onChange={handleInputChange} required />
                <InputTextField label="Phone Number" name="phoneNumber" value={formData.phoneNumber} onChange={handleInputChange} />
                <InputTextField label="Password" name="password" type="password" value={formData.password} onChange={handleInputChange} required />
                <InputTextField label="Confirm Password" name="repeatPassword" type="password" value={formData.repeatPassword} onChange={handleInputChange} required />
                <div className="centered-container">
                    <label>
                        <input type="checkbox" name="isCompany" checked={formData.isCompany} onChange={handleInputChange} />
                        Register as a Company
                    </label>
                </div>
                <MainButton type="submit" text="Register" />
                {errorMessage && <div className="error-message">{errorMessage}</div>}
                <span onClick={() => setCurrentPage(0)} className="formLink">Already have an account? Sign in</span>
            </form>
        </CenteredPanel>
    );
}

export default Register;
