import React, { useState, useEffect } from 'react';
import { registerCompany, getPendingRequests, approveRequest, rejectRequest } from '../services/companyService';
import CenteredPanel from '../components/CenteredPanel';
import InputTextField from '../components/InputTextField';
import MainButton from '../components/MainButton';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';
import { usePage } from '../components/PageProvider';
import { LOCAL_STORAGE_KEYS } from '../utils/LocalStorageKeys';

const CompanyRegistration = () => {
    const { setCurrentPage } = usePage();
    const [companyData, setCompanyData] = useState({
        name: '',
        registrationDetails: ''
    });
    const [pendingRequests, setPendingRequests] = useState([]);
    const [error, setError] = useState('');

    const handleChange = (e) => {
        const { name, value } = e.target;
        setCompanyData(prev => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await registerCompany(companyData);
            if (response.status == 200) {
                setCompanyData({ name: '', registrationDetails: '' });
                localStorage.setItem(LOCAL_STORAGE_KEYS.COMPANY_STATUS, 1);
                setCurrentPage(12);
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
                <h2 className="formTitle">Company Registration</h2>
                {error && <div className="error-message">{error}</div>}
                <form onSubmit={handleSubmit}>
                    <InputTextField label="Company Name" name="name" value={companyData.name} onChange={handleChange} />
                    <InputTextField label="Registration Details" name="registrationDetails" value={companyData.registrationDetails} onChange={handleChange} />
                    <MainButton type="submit" text="Register Company" />
                </form>
            </CenteredPanel>
            <LogoutButton />
        </>
    );
};

export default CompanyRegistration;
