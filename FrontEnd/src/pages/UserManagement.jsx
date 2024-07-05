import React, { useState, useEffect } from 'react';
import { getAllUsers, updateUser, deleteUser } from '../services/userService';
import CenteredPanel from '../components/CenteredPanel';
import InputTextField from '../components/InputTextField';
import MainButton from '../components/MainButton';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';

const UserManagement = () => {
    const [users, setUsers] = useState([]);
    const [editingUser, setEditingUser] = useState(null);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchUsers = async () => {
            try {
                const data = await getAllUsers();
                setUsers(data);
            } catch (error) {
                console.error("Failed to fetch users:", error);
                setError('Failed to load users.');
            }
        };
        fetchUsers();
    }, []);

    const handleInputChange = (user, field, value) => {
        setEditingUser({ ...user, [field]: value });
    };

    const handleEditUser = (user) => {
        setEditingUser(user);
    };

    const handleUpdateUser = async () => {
        try {
            const response = await updateUser(editingUser);
            if (response.status == 200) {
                setUsers(users.map(user => user.id == editingUser.id ? editingUser : user));
                setEditingUser(null);
                setError('');
            } else {
                setError('Failed to update user.');
            }
        } catch (error) {
            console.error("Error updating user:", error);
            setError('Failed to update user.');
        }
    };

    const handleDeleteUser = async (userId) => {
        try {
            await deleteUser(users.find(user => user.id === userId).username);
            setUsers(users.filter(user => user.id !== userId));
            setError('');
        } catch (error) {
            console.error("Error deleting user:", error);
            setError('Failed to delete user.');
        }
    };

    const displayRole = (role) => {
        switch (role) {
            case 0: return "User";
            case 1: return "Company Representative";
            case 2: return "Admin";
            default: return "Unknown";
        }
    };

    return (
        <>
            <MainMenu />
            <CenteredPanel containerHeight="1200">
                <h2 className="formTitle">User Management</h2>
                {error && <div className="error-message">{error}</div>}
                <div className="userList">
                    {users.map((user) => (
                        <div key={user.id} className="userItem">
                            {editingUser && editingUser.id == user.id ? (
                                <>
                                    <InputTextField label="First Name" name="firstName" value={editingUser.firstName} onChange={(e) => handleInputChange(editingUser, 'firstName', e.target.value)} />
                                    <InputTextField label="Last Name" name="lastName" value={editingUser.lastName} onChange={(e) => handleInputChange(editingUser, 'lastName', e.target.value)} />
                                    <InputTextField label="Email" name="email" value={editingUser.email} onChange={(e) => handleInputChange(editingUser, 'email', e.target.value)} />
                                    <p>Role: {displayRole(editingUser.role)}</p>
                                    <MainButton text="Save" onClick={handleUpdateUser} />
                                    <MainButton text="Cancel" onClick={() => setEditingUser(null)} />
                                </>
                            ) : (
                                <>
                                    <p>Username: {user.username}</p>
                                    <p>Name: {user.firstName} {user.lastName}</p>
                                    <p>Email: {user.email}</p>
                                    <p>Role: {displayRole(user.role)}</p>
                                    <MainButton text="Edit" onClick={() => handleEditUser(user)} />
                                    <MainButton text="Delete" onClick={() => handleDeleteUser(user.id)} />
                                </>
                            )}
                        </div>
                    ))}
                </div>
            </CenteredPanel>
            <LogoutButton />
        </>
    );
};

export default UserManagement;
