import React, { useState, useEffect } from 'react';
import { registerCompany, getPendingRequests, approveRequest, rejectRequest } from '../services/companyService';
import CenteredPanel from '../components/CenteredPanel';
import InputTextField from '../components/InputTextField';
import MainButton from '../components/MainButton';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';

const CompanyRegistrationPending = () => {
    const [companyData, setCompanyData] = useState({
        name: '',
        registrationDetails: ''
    });
    const [pendingRequests, setPendingRequests] = useState([]);
    const [error, setError] = useState('');
    const [success, setSuccess] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCompanyData(prev => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await registerCompany(companyData);
            if (response.status == 200) {
                setSuccess('Company registration request submitted successfully!');
                setCompanyData({ name: '', registrationDetails: '' });
            }
            else {
                setError('Complete all fields');
            }
        } catch (error) {
            setError('Failed to submit company registration.');
        }
    };

    return (
        <>
            <MainMenu />
            <CenteredPanel>
                <h2 className="formTitle">Company Registration Pending</h2>
                <p style={{ textAlign: 'center' }}>You have a pending request, you should wait for an admin to resolve your request.</p>
            </CenteredPanel>
            <LogoutButton />
        </>
    );
};

export default CompanyRegistrationPending;
