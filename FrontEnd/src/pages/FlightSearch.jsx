import React, { useState, useEffect } from 'react';
import {
    Box, Button, FormControl, Grid, InputLabel, MenuItem, Paper, Select,
    Table, TableBody, TableCell, TableContainer, TableHead, TableRow,
    TextField, Typography, Dialog, DialogActions, DialogContent,
    DialogContentText, DialogTitle
} from '@mui/material';
import { addBooking } from '../services/bookingService';
import { getAllFlights } from '../services/flightService';
import { getAllAirports } from '../services/airportService';
import { getAllCompanies } from '../services/companyService';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';
import { LOCAL_STORAGE_KEYS } from '../utils/LocalStorageKeys';

const FlightSearch = () => {
    const [flights, setFlights] = useState([]);
    const [airports, setAirports] = useState([]);
    const [companies, setCompanies] = useState([]);
    const [filter, setFilter] = useState({
        originAirportId: '',
        destinationAirportId: '',
        departureTime: '',
        arrivalTime: '',
        companyId: ''
    });
    const [openDialog, setOpenDialog] = useState(false);
    const [selectedFlight, setSelectedFlight] = useState(null);

    useEffect(() => {
        const fetchInitialData = async () => {
            const [airportData, companyData] = await Promise.all([
                getAllAirports(),
                getAllCompanies()
            ]);
            setAirports(airportData);
            setCompanies(companyData);
            fetchFlights(filter);
        };
        fetchInitialData();
    }, []);

    const fetchFlights = async (params) => {
        const data = await getAllFlights(params);
        setFlights(data.filter(flight => flight.remainingTickets > 0));
    };

    const convertToUTC = (localDate) => {
        const date = new Date(localDate);
        return new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toISOString();
    };

    const handleFilterChange = async (e) => {
        const { name, value } = e.target;
        let newFilters = { ...filter, [name]: value };

        console.log(newFilters)
        setFilter(newFilters);
        await fetchFlights({ ...newFilters, departureTime: newFilters.departureTime == '' ? '' : convertToUTC(newFilters.departureTime), arrivalTime: newFilters.arrivalTime == '' ? '' : convertToUTC(newFilters.arrivalTime) });
    };

    const handleBookFlight = async () => {
        if (selectedFlight) {
            var data = { flightId: selectedFlight.id, userId: await localStorage.getItem(LOCAL_STORAGE_KEYS.USER_ID) }
            await addBooking(data);
            setOpenDialog(false);
            fetchFlights(filter);
        }
    };

    const openBookingDialog = (flight) => {
        setSelectedFlight(flight);
        setOpenDialog(true);
    };

    return (
        <>
            <MainMenu />
            <Box sx={{ m: 4, pl: 32 }}>
                <Typography variant="h4" gutterBottom>Flight Booking</Typography>
                <Grid container spacing={2}>
                    {/* Filters */}
                    <Grid item xs={12} md={3}>
                        <FormControl fullWidth>
                            <InputLabel>Origin Airport</InputLabel>
                            <Select
                                value={filter.originAirportId}
                                onChange={handleFilterChange}
                                name="originAirportId"
                            >
                                <MenuItem value="">None</MenuItem>
                                {airports.map(airport => (
                                    <MenuItem key={airport.id} value={airport.id}>{airport.name}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <FormControl fullWidth>
                            <InputLabel>Destination Airport</InputLabel>
                            <Select
                                value={filter.destinationAirportId}
                                onChange={handleFilterChange}
                                name="destinationAirportId"
                            >
                                <MenuItem value="">None</MenuItem>
                                {airports.map(airport => (
                                    <MenuItem key={airport.id} value={airport.id}>{airport.name}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <TextField
                            fullWidth
                            type="datetime-local"
                            label="Departure Time"
                            name="departureTime"
                            value={filter.departureTime}
                            onChange={handleFilterChange}
                            InputLabelProps={{ shrink: true }}
                        />
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <TextField
                            fullWidth
                            type="datetime-local"
                            label="Arrival Time"
                            name="arrivalTime"
                            value={filter.arrivalTime}
                            onChange={handleFilterChange}
                            InputLabelProps={{ shrink: true }}
                        />
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <FormControl fullWidth>
                            <InputLabel>Company</InputLabel>
                            <Select
                                value={filter.companyId}
                                onChange={handleFilterChange}
                                name="companyId"
                            >
                                <MenuItem value="">None</MenuItem>
                                {companies.map(company => (
                                    <MenuItem key={company.id} value={company.id}>{company.name}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>
                </Grid>
                {/* Flight List */}
                <TableContainer component={Paper} sx={{ mt: 4 }}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Company</TableCell>
                                <TableCell>Departure Time</TableCell>
                                <TableCell>Arrival Time</TableCell>
                                <TableCell>Origin Airport</TableCell>
                                <TableCell>Destination Airport</TableCell>
                                <TableCell>Remaining Tickets</TableCell>
                                <TableCell>Book</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {console.log(flights)}
                            {flights.map((flight) => (
                                <TableRow key={flight.id}>
                                    <TableCell>{companies.find(c => c.id === flight.companyId)?.name}</TableCell>
                                    <TableCell>{new Date(flight.departureTime).toUTCString()}</TableCell>
                                    <TableCell>{new Date(flight.arrivalTime).toUTCString()}</TableCell>
                                    <TableCell>{airports.find(a => a.id === flight.originAirportId)?.name || 'N/A'}</TableCell>
                                    <TableCell>{airports.find(a => a.id === flight.destinationAirportId)?.name || 'N/A'}</TableCell>
                                    <TableCell>{flight.remainingTickets}</TableCell>
                                    <TableCell>
                                        <Button variant="contained" onClick={() => openBookingDialog(flight)}>Book</Button>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            </Box>
            <Dialog open={openDialog} onClose={() => setOpenDialog(false)}>
                <DialogTitle>Confirm Booking</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Are you sure you want to book this flight?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setOpenDialog(false)}>Cancel</Button>
                    <Button onClick={handleBookFlight} color="primary">Book Flight</Button>
                </DialogActions>
            </Dialog>
            <LogoutButton />
        </>
    );
};

export default FlightSearch;
