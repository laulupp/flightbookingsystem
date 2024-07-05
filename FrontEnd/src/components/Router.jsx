import React from 'react';
import Home from '../pages/Home';
import Login from '../pages/Login';
import Booking from '../pages/Booking';
import Register from '../pages/Register';
import FlightSearch from '../pages/FlightSearch';
import FlightAdmin from '../pages/FlightAdmin';
import UserManagement from '../pages/UserManagement';
import UserSettings from '../pages/UserSettings';
import Aircraft from '../pages/Aircraft';
import Airport from '../pages/Airport';
import CompanyAdmin from '../pages/CompanyAdmin';
import { usePage } from './PageProvider';
import CompanyRegistration from '../pages/CompanyRegistration';
import CompanyRegistrationPending from '../pages/CompanyRegistrationPending';

function Router(props) {
    const { currentPage } = usePage();
    const renderPage = () => {
        switch (currentPage) {
            case 0:
                return <Login />;
            case 1:
                return <Register />;
            case 2:
                return <Home />;
            case 3:
                return <FlightSearch />;
            case 4:
                return <FlightAdmin />;
            case 5:
                return <UserManagement />;
            case 6:
                return <UserSettings />;
            case 7:
                return <Booking />;
            case 8:
                return <Aircraft />;
            case 9:
                return <Airport />;
            case 10:
                return <CompanyAdmin />;
            case 11:
                return <CompanyRegistration />;
            case 12:
                return <CompanyRegistrationPending />;
            default:
                return <Home />;
        }
    }
    return (
        <>
            {renderPage()}
        </>
    );
}

export default Router;
