import React, { useState, useEffect } from 'react';
import { getCompanyAircrafts, addAircraft, updateAircraft, deleteAircraft } from '../services/aircraftService';
import CenteredPanel from '../components/CenteredPanel';
import InputTextField from '../components/InputTextField';
import MainButton from '../components/MainButton';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';
import '../style/pages/Aircraft.css'; // Ensure this CSS path is correct
import { LOCAL_STORAGE_KEYS } from '../utils/LocalStorageKeys';

const Aircraft = () => {
    const [aircrafts, setAircrafts] = useState([]);
    const [newAircraft, setNewAircraft] = useState({ model: '', capacity: '' });
    const [editingAircraft, setEditingAircraft] = useState(null);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchAircrafts = async () => {
            const companyId = localStorage.getItem(LOCAL_STORAGE_KEYS.COMPANY_ID);
            if (companyId) {
                const data = await getCompanyAircrafts(companyId);
                setAircrafts(data);
            }
        };
        fetchAircrafts();
    }, []);

    const handleInputChange = (e, setter) => {
        const { name, value } = e.target;
        setter(prev => ({ ...prev, [name]: value }));
        setError(''); // Clear error when input changes
    };

    const validateInput = (aircraft) => {
        if (!aircraft.model || aircraft.capacity < 1) {
            setError('Model must be filled and capacity must be at least 1.');
            return false;
        }
        return true;
    };

    const handleCreateAircraft = async () => {
        if (!validateInput(newAircraft)) return;

        const companyId = localStorage.getItem(LOCAL_STORAGE_KEYS.COMPANY_ID);
        if (companyId) {
            const data = await addAircraft({ ...newAircraft }, companyId);
            setAircrafts([...aircrafts, data]);
            setNewAircraft({ model: '', capacity: '' });
        }
    };

    const handleUpdateAircraft = async () => {
        if (!validateInput(editingAircraft)) return;

        const companyId = localStorage.getItem(LOCAL_STORAGE_KEYS.COMPANY_ID);
        const data = await updateAircraft(companyId, editingAircraft.id, editingAircraft);
        setAircrafts(aircrafts.map(ac => ac.id === editingAircraft.id ? editingAircraft : ac));
        setEditingAircraft(null);
    };

    const handleDeleteAircraft = async (aircraftId) => {
        const companyId = localStorage.getItem(LOCAL_STORAGE_KEYS.COMPANY_ID);
        await deleteAircraft(companyId, aircraftId);
        setAircrafts(aircrafts.filter(ac => ac.id !== aircraftId));
    };

    return (
        <>
            <MainMenu />
            <CenteredPanel containerHeight="1200">
                <h2 className="formTitle">Aircraft Management</h2>
                {error && <div className="error-message">{error}</div>}
                <InputTextField label="Model" name="model" value={newAircraft.model} onChange={(e) => handleInputChange(e, setNewAircraft)} required />
                <InputTextField label="Capacity" name="capacity" type="number" min="1" step="1" value={newAircraft.capacity} onChange={(e) => handleInputChange(e, setNewAircraft)} required />
                <MainButton type="button" text="Add Aircraft" onClick={handleCreateAircraft} />
                <div className="aircraftList">
                    {aircrafts.map((aircraft) => (
                        <div key={aircraft.id} className="aircraftCard">
                            <p>Model: {aircraft.model}</p>
                            <p>Capacity: {aircraft.capacity}</p>
                            <div className="buttonGroup">
                                <MainButton text="Edit" onClick={() => setEditingAircraft(aircraft)} />
                                <MainButton text="Delete" onClick={() => handleDeleteAircraft(aircraft.id)} />
                            </div>
                        </div>
                    ))}
                </div>
                {editingAircraft && (
                    <div className="editForm">
                        <h3>Edit Aircraft</h3>
                        <InputTextField label="Model" name="model" value={editingAircraft.model} onChange={(e) => handleInputChange(e, setEditingAircraft)} required />
                        <InputTextField label="Capacity" name="capacity" value={editingAircraft.capacity} onChange={(e) => handleInputChange(e, setEditingAircraft)} required />
                        <MainButton type="button" text="Save" onClick={handleUpdateAircraft} />
                    </div>
                )}
            </CenteredPanel>
            <LogoutButton />
        </>
    );
};

export default Aircraft;
