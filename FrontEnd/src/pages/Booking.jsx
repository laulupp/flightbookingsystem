import React, { useState, useEffect } from 'react';
import {
    Box, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow,
    Typography, Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle
} from '@mui/material';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';
import { getUserBookings, cancelBooking } from '../services/bookingService';
import { getAllFlights } from '../services/flightService';
import { getAllAirports } from '../services/airportService';
import { getAllCompanies } from '../services/companyService';

const UserBookings = () => {
    const [bookings, setBookings] = useState([]);
    const [flights, setFlights] = useState([]);
    const [airports, setAirports] = useState([]);
    const [companies, setCompanies] = useState([]);
    const [openDialog, setOpenDialog] = useState(false);
    const [selectedBooking, setSelectedBooking] = useState(null);

    useEffect(() => {
        fetchInitialData();
    }, []);

    const fetchInitialData = async () => {
        const [bookingsData, flightsData, airportsData, companiesData] = await Promise.all([
            getUserBookings(),
            getAllFlights(),
            getAllAirports(),
            getAllCompanies()
        ]);
        setBookings(bookingsData);
        setFlights(flightsData);
        setAirports(airportsData);
        setCompanies(companiesData);
    };

    const handleDelete = async () => {
        await cancelBooking(selectedBooking.id);
        setOpenDialog(false);
        fetchInitialData();  // Refresh data after deletion
    };

    const openDeleteDialog = (booking) => {
        setSelectedBooking(booking);
        setOpenDialog(true);
    };

    const findDetailsById = (list, id, property) => {
        const item = list.find(item => item.id === id);
        return item ? item[property] : 'N/A';
    };

    return (
        <>
            <MainMenu />
            <Box sx={{ m: 4, pl: 32 }}>
                <Typography variant="h4" gutterBottom>User Bookings</Typography>
                <TableContainer component={Paper} sx={{ mt: 4 }}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Flight Number</TableCell>
                                <TableCell>Departure Time</TableCell>
                                <TableCell>Arrival Time</TableCell>
                                <TableCell>Origin Airport</TableCell>
                                <TableCell>Destination Airport</TableCell>
                                <TableCell>Company</TableCell>
                                <TableCell>Action</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {bookings.map((booking) => {
                                const flight = flights.find(f => f.id === booking.flightId) || {};
                                return (
                                    <TableRow key={booking.id}>
                                        <TableCell>{new Date(booking.bookingDate).toLocaleString()}</TableCell>
                                        <TableCell>{flight.departureTime ? new Date(flight.departureTime).toLocaleString() : 'N/A'}</TableCell>
                                        <TableCell>{flight.arrivalTime ? new Date(flight.arrivalTime).toLocaleString() : 'N/A'}</TableCell>
                                        <TableCell>{findDetailsById(airports, flight.originAirportId, 'name')}</TableCell>
                                        <TableCell>{findDetailsById(airports, flight.destinationAirportId, 'name')}</TableCell>
                                        <TableCell>{findDetailsById(companies, flight.companyId, 'name')}</TableCell>
                                        <TableCell>
                                            <Button color="secondary" onClick={() => openDeleteDialog(booking)}>Delete</Button>
                                        </TableCell>
                                    </TableRow>
                                );
                            })}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Box>
            <Dialog open={openDialog} onClose={() => setOpenDialog(false)}>
                <DialogTitle>Confirm Deletion</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this booking? This action cannot be undone.
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpenDialog(false)}>Cancel</Button>
                    <Button onClick={handleDelete} color="primary">Delete</Button>
                </DialogActions>
            </Dialog>
            <LogoutButton />
        </>
    );
};

export default UserBookings;
