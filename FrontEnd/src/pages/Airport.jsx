import React, { useState, useEffect } from 'react';
import { getAllAirports, addAirport, updateAirport, deleteAirport } from '../services/airportService';
import CenteredPanel from '../components/CenteredPanel';
import InputTextField from '../components/InputTextField';
import MainButton from '../components/MainButton';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';
import '../style/pages/Airport.css';

const Airport = () => {
    const [airports, setAirports] = useState([]);
    const [newAirport, setNewAirport] = useState({ name: '', location: '' });
    const [editingAirport, setEditingAirport] = useState(null);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchAirports = async () => {
            try {
                const data = await getAllAirports();
                setAirports(data);
            } catch (error) {
                console.error("Failed to fetch airports:", error);
                setError('Failed to load airports.');
            }
        };
        fetchAirports();
    }, []);

    const handleInputChange = (e, setter) => {
        const { name, value } = e.target;
        setter(prev => ({ ...prev, [name]: value }));
    };

    const handleCreateAirport = async () => {
        try {
            const response = await addAirport(newAirport);
            if (response.status == 200) {
                setAirports([...airports, response.data]);
                setNewAirport({ name: '', location: '' });
                setError('');
            }
            else {
                setError(response.data.error ? response.data.error : "Invalid input");
            }
        } catch (error) {
            console.error("Error adding airport:", error);
            setError('Failed to add airport.');
        }
    };

    const handleUpdateAirport = async () => {
        console.log(editingAirport);
        try {
            const response = await updateAirport(editingAirport);
            console.log(response);
            if (response.status == 200) {
                console.log("intra ");
                setAirports(airports.map(ap => ap.id === editingAirport.id ? editingAirport : ap));
                setEditingAirport(null);
                setError('');
            }
            else {
                console.log(response)
                setError(response.data.error ? response.data.error : "Invalid input");
            }
        } catch (error) {
            console.error("Error updating airport:", error);
            setError('Failed to update airport.');
        }
    };

    const handleDeleteAirport = async (airportId) => {
        try {
            await deleteAirport(airportId);
            setAirports(airports.filter(ap => ap.id !== airportId));
            setError('');
        } catch (error) {
            console.error("Error deleting airport:", error);
            setError('Failed to delete airport.');
        }
    };

    return (
        <>
            <MainMenu />
            <CenteredPanel containerHeight="1200">
                <h2 className="formTitle">Airport Management</h2>
                <InputTextField label="Name" name="name" value={newAirport.name} onChange={(e) => handleInputChange(e, setNewAirport)} />
                <InputTextField label="Location" name="location" value={newAirport.location} onChange={(e) => handleInputChange(e, setNewAirport)} />
                <MainButton type="button" text="Add Airport" onClick={handleCreateAirport} />
                {error && <div className="error-message">{error}</div>}
                <div className="airportList">
                    {airports.map((airport) => (
                        <div key={airport.id} className="airportItem">
                            <p>Name: {airport.name}</p>
                            <p>Location: {airport.location}</p>
                            <MainButton text="Edit" onClick={() => setEditingAirport(airport)} />
                            <MainButton text="Delete" onClick={() => handleDeleteAirport(airport.id)} />
                        </div>
                    ))}
                </div>
                {editingAirport && (
                    <div className="editForm">
                        <h3>Edit Airport</h3>
                        <InputTextField label="Name" name="name" value={editingAirport.name} onChange={(e) => handleInputChange(e, setEditingAirport)} />
                        <InputTextField label="Location" name="location" value={editingAirport.location} onChange={(e) => handleInputChange(e, setEditingAirport)} />
                        <MainButton type="button" text="Save" onClick={handleUpdateAirport} />
                    </div>
                )}
            </CenteredPanel>
            <div style={{ marginBottom: 30 }}></div>
            <LogoutButton />
        </>
    );
};

export default Airport;
