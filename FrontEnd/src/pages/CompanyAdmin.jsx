import React, { useState, useEffect } from 'react';
import { getPendingRequests, approveRequest, rejectRequest } from '../services/companyService';
import CenteredPanel from '../components/CenteredPanel';
import MainButton from '../components/MainButton';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';
//import '../style/pages/CompanyAdmin.css';

const CompanyAdmin = () => {
    const [requests, setRequests] = useState([]);

    useEffect(() => {
        const fetchRequests = async () => {
            const data = await getPendingRequests();
            setRequests(data);
        };
        fetchRequests();
    }, []);

    const handleApprove = async (companyId) => {
        await approveRequest(companyId);
        setRequests(requests.filter(request => request.id !== companyId));
    };

    const handleReject = async (companyId) => {
        await rejectRequest(companyId);
        setRequests(requests.filter(request => request.id !== companyId));
    };

    return (
        <>
            <MainMenu />
            <CenteredPanel>
                <h2 className="formTitle">Pending Company Requests</h2>
                <div className="companyList">
                    {requests.map((request) => (
                        <div key={request.id} className="companyItem">
                            <p><b>Company Name:</b> {request.company.name}</p>
                            <p><b>Registration Details:</b> {request.company.registrationDetails}</p>
                            <p><b>Company Representative:</b> {`${request.user.firstName} ${request.user.lastName} (${request.user.email})`}</p>
                            <p><b>Request Date:</b> {`${(new Date(request.requestDate)).toLocaleDateString('en-US', {
            hour: 'numeric',
            minute: 'numeric'
          })}`}</p>
                            <MainButton type="button" text="Approve" onClick={() => handleApprove(request.id)} />
                            <MainButton type="button" text="Reject" onClick={() => handleReject(request.id)} />
                        </div>
                    ))}
                </div>
            </CenteredPanel>
            <LogoutButton />
        </>
    );
};

export default CompanyAdmin;
