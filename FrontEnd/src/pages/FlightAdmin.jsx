import React, { useState, useEffect } from 'react';
import { getAllCompanyFlights, addFlight, updateFlight, deleteFlight } from '../services/flightService';
import { getAllAirports } from '../services/airportService';
import { getCompanyAircrafts } from '../services/aircraftService';
import { Box, Button, FormControl, Grid, InputLabel, MenuItem, Paper, Select, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, TextField, Typography } from '@mui/material';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';
import { LOCAL_STORAGE_KEYS } from '../utils/LocalStorageKeys';

const FlightAdmin = () => {
    const [flights, setFlights] = useState([]);
    const [newFlight, setNewFlight] = useState({
        departureTime: '',
        arrivalTime: '',
        aircraftId: '',
        originAirportId: '',
        destinationAirportId: ''
    });
    const [aircrafts, setAircrafts] = useState([]);
    const [airports, setAirports] = useState([]);
    const [error, setError] = useState('');

    useEffect(() => {
        const companyId = localStorage.getItem(LOCAL_STORAGE_KEYS.COMPANY_ID);
        const fetchInitialData = async () => {
            const [aircraftData, airportData, flightData] = await Promise.all([
                getCompanyAircrafts(companyId),
                getAllAirports(),
                getAllCompanyFlights(companyId)
            ]);
            setAircrafts(aircraftData);
            setAirports(airportData);
            setFlights(flightData);
        };
        fetchInitialData();
    }, []);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewFlight(prev => ({ ...prev, [name]: value }));
    };

    const validateInput = () => {
        console.log(newFlight)
        if (!newFlight.departureTime || !newFlight.arrivalTime || newFlight.originAirportId === newFlight.destinationAirportId || new Date(newFlight.departureTime) >= new Date(newFlight.arrivalTime)) {
            setError('Please ensure all fields are correctly filled and logical.');
            return false;
        }
        setError('')
        return true;
    };

    const convertToUTC = (localDate) => {
        const date = new Date(localDate);
        return new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toISOString();
    };

    const handleCreateFlight = async () => {
        if (!validateInput()) return;

        const companyId = localStorage.getItem(LOCAL_STORAGE_KEYS.COMPANY_ID);

        const utcFlight = {
            ...newFlight,
            companyId: companyId,
            departureTime: convertToUTC(newFlight.departureTime),
            arrivalTime: convertToUTC(newFlight.arrivalTime)
        };

        const response = await addFlight(utcFlight);
        if (response) {
            utcFlight.id = response.data.id;
            setFlights([...flights, utcFlight]);
            setNewFlight({ departureTime: '', arrivalTime: '', aircraftId: '', originAirportId: '', destinationAirportId: '' });
        }
    };

    return (
        <>
            <MainMenu />
            <Box sx={{ m: 4, pl: 32 }} >
                <Typography variant="h4" gutterBottom>Flight Management</Typography>
                {error && <Typography color="error">{error}</Typography>}
                <Grid container spacing={2}>
                    <Grid item xs={12} md={3}>
                        <TextField
                            fullWidth
                            type="datetime-local"
                            label="Departure Time"
                            name="departureTime"
                            value={newFlight.departureTime}
                            onChange={handleInputChange}
                            InputLabelProps={{ shrink: true }}
                        />
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <TextField
                            fullWidth
                            type="datetime-local"
                            label="Arrival Time"
                            name="arrivalTime"
                            value={newFlight.arrivalTime}
                            onChange={handleInputChange}
                            InputLabelProps={{ shrink: true }}
                        />
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <FormControl fullWidth>
                            <InputLabel>Aircraft</InputLabel>
                            <Select
                                value={newFlight.aircraftId}
                                onChange={handleInputChange}
                                name="aircraftId"
                            >
                                {aircrafts.map(aircraft => (
                                    <MenuItem key={aircraft.id} value={aircraft.id}>{aircraft.model}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} md={3}>
                        <FormControl fullWidth>
                            <InputLabel>Origin Airport</InputLabel>
                            <Select
                                value={newFlight.originAirportId}
                                onChange={handleInputChange}
                                name="originAirportId"
                            >
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
                                value={newFlight.destinationAirportId}
                                onChange={handleInputChange}
                                name="destinationAirportId"
                            >
                                {airports.map(airport => (
                                    <MenuItem key={airport.id} value={airport.id}>{airport.name}</MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12}>
                        <Button variant="contained" color="primary" onClick={handleCreateFlight}>Create Flight</Button>
                    </Grid>
                </Grid>
            </Box>
            <Box sx={{ m: 4, pl: 32 }}>
                <Box mt={4}>
                    <Typography variant="h5" gutterBottom>Flight List</Typography>
                    <TableContainer component={Paper}>
                        <Table>
                            <TableHead>
                                <TableRow>
                                    <TableCell>Departure Time</TableCell>
                                    <TableCell>Arrival Time</TableCell>
                                    <TableCell>Aircraft</TableCell>
                                    <TableCell>Origin Airport</TableCell>
                                    <TableCell>Destination Airport</TableCell>
                                    <TableCell>Remaining Tickets</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>{console.log(flights)}
                                {flights.map((flight) => (
                                    <TableRow key={flight.id}>
                                        <TableCell>{new Date(flight.departureTime).toLocaleString()}</TableCell>
                                        <TableCell>{new Date(flight.arrivalTime).toLocaleString()}</TableCell>
                                        <TableCell>{aircrafts.find(a => a.id === flight.aircraftId)?.model || 'N/A'}</TableCell>
                                        <TableCell>{airports.find(a => a.id === flight.originAirportId)?.name || 'N/A'}</TableCell>
                                        <TableCell>{airports.find(a => a.id === flight.destinationAirportId)?.name || 'N/A'}</TableCell>
                                        <TableCell>{flight.remainingTickets}</TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Box>
            </Box>
            <LogoutButton />
        </>
    );
};

export default FlightAdmin;
