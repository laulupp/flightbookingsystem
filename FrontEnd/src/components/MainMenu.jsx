import { Drawer, List, ListItemButton, ListItemText, Typography } from '@mui/material';
import React from 'react';
import '../style/components/MainMenu.css';
import { LOCAL_STORAGE_KEYS } from '../utils/LocalStorageKeys';
import { usePage } from './PageProvider';

const roles = {
    ADMIN: 2,
    COMPANY_REPRESENTATIVE: 1,
    USER: 0
};

function MainMenu(props) {
    const { setCurrentPage } = usePage();
    const role = localStorage.getItem(LOCAL_STORAGE_KEYS.ROLE);
    const companyStatus = localStorage.getItem(LOCAL_STORAGE_KEYS.COMPANY_STATUS);

    return (
        <Drawer variant="permanent" className="mainMenu">
            <div className="welcomeMessage">
                <Typography variant="h6">Welcome,</Typography>
                <Typography
                    variant="h6"
                    style={{ fontWeight: 'bold', color: 'var(--color-purple-background)' }}
                >
                    {localStorage.getItem(LOCAL_STORAGE_KEYS.FIRST_NAME)}{' '}
                    {localStorage.getItem(LOCAL_STORAGE_KEYS.LAST_NAME)}
                </Typography>
            </div>
            <List>
                <ListItemButton component="a" onClick={() => setCurrentPage(2)}>
                    <ListItemText primary="Home" />
                </ListItemButton>
                {role == roles.USER && (
                    <ListItemButton component="a" onClick={() => setCurrentPage(3)}>
                        <ListItemText primary="Flight Search" />
                    </ListItemButton>
                )}
                {role == roles.USER && (
                    <ListItemButton component="a" onClick={() => setCurrentPage(7)}>
                        <ListItemText primary="My Bookings" />
                    </ListItemButton>
                )}
                {role == roles.COMPANY_REPRESENTATIVE && companyStatus == 2 && (
                    <ListItemButton component="a" onClick={() => setCurrentPage(4)}>
                        <ListItemText primary="Flight Management" />
                    </ListItemButton>
                )}
                {role == roles.ADMIN && (
                    <ListItemButton component="a" onClick={() => setCurrentPage(9)}>
                        <ListItemText primary="Airport Management" />
                    </ListItemButton>
                )}
                {role == roles.ADMIN && (
                    <ListItemButton component="a" onClick={() => setCurrentPage(5)}>
                        <ListItemText primary="User Management" />
                    </ListItemButton>
                )}
                {role == roles.COMPANY_REPRESENTATIVE && companyStatus == 2 && (
                    <ListItemButton component="a" onClick={() => setCurrentPage(8)}>
                        <ListItemText primary="Aircraft Management" />
                    </ListItemButton>
                )}
                {role == roles.ADMIN && (
                    <ListItemButton component="a" onClick={() => setCurrentPage(10)}>
                        <ListItemText primary="Company Management" />
                    </ListItemButton>
                )}
                {role == roles.COMPANY_REPRESENTATIVE && companyStatus == 0 && (
                    <ListItemButton component="a" onClick={() => setCurrentPage(11)}>
                        <ListItemText primary="Register a Company" />
                    </ListItemButton>
                )}
                {role == roles.COMPANY_REPRESENTATIVE && companyStatus == 1 && (
                    <ListItemButton component="a" onClick={() => setCurrentPage(12)}>
                        <ListItemText primary="Register a Company" />
                    </ListItemButton>
                )}
                <ListItemButton component="a" onClick={() => setCurrentPage(6)}>
                    <ListItemText primary="User Settings" />
                </ListItemButton>
            </List>
        </Drawer>
    );
}

export default MainMenu;
