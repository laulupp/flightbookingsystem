import React from 'react';
import CenteredPanel from '../components/CenteredPanel';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';
//import '../style/pages/Home.css';

const Home = () => {
    return (
        <>
            <MainMenu />
            <CenteredPanel>
                <h2 className="formTitle">Welcome to the Flight Booking System</h2>
            </CenteredPanel>
            <LogoutButton />
        </>
    );
};

export default Home;
