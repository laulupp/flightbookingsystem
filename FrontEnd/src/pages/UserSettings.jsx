import React, { useState, useEffect } from 'react';
import { getUserProfile, updateUser, changePassword } from '../services/userService';
import CenteredPanel from '../components/CenteredPanel';
import InputTextField from '../components/InputTextField';
import MainButton from '../components/MainButton';
import MainMenu from '../components/MainMenu';
import LogoutButton from '../components/LogoutButton';
import { LOCAL_STORAGE_KEYS } from '../utils/LocalStorageKeys';

const UserSettings = () => {
    const [userProfile, setUserProfile] = useState({ firstName: '', lastName: '', email: '', password: '' });
    const [passwords, setPasswords] = useState({ oldPassword: '', newPassword: '' });
    const [profileError, setProfileError] = useState('');
    const [passwordError, setPasswordError] = useState('');

    useEffect(() => {
        const fetchUserProfile = async () => {
            try {
                const data = await getUserProfile();
                setUserProfile(data);
            } catch (error) {
                console.error("Failed to fetch user profile:", error);
                setProfileError('Failed to load user data.');
            }
        };
        fetchUserProfile();
    }, []);

    const handleInputChange = (e, setter) => {
        const { name, value } = e.target;
        setter(prev => ({ ...prev, [name]: value }));
    };

    const handleProfileUpdate = async () => {
        setPasswordError('');
        setProfileError('');
        try {
            var response = await updateUser(userProfile);
            if (response.status != 200) {
                setProfileError(response.data.error)
            }
            else {
                localStorage.setItem(LOCAL_STORAGE_KEYS.FIRST_NAME, userProfile.firstName);
                localStorage.setItem(LOCAL_STORAGE_KEYS.LAST_NAME, userProfile.lastName);
                setProfileError('Profile updated successfully.');
            }
        } catch (error) {
            console.error("Profile update failed:", error);
            setProfileError('Failed to update profile.');
        }
    };

    const handleChangePassword = async () => {
        setPasswordError('');
        setProfileError('');
        if (passwords.newPassword !== passwords.oldPassword) {
            try {
                var data = await changePassword(passwords);
                if (data.error) {
                    setPasswordError(data.error)
                }
                else {
                    setPasswordError('Password changed successfully.');
                }
                setPasswords({ oldPassword: '', newPassword: '' });
            } catch (error) {
                console.error("Password change failed:", error);
                setPasswordError('Failed to change password.');
            }
        } else {
            setPasswordError('New password cannot be the same as the old password.');
        }
    };

    return (
        <>
            <MainMenu />
            <CenteredPanel containerHeight="800">
                <h2 className="formTitle">User Settings</h2>
                <InputTextField label="First Name" name="firstName" value={userProfile.firstName} onChange={(e) => handleInputChange(e, setUserProfile)} />
                <InputTextField label="Last Name" name="lastName" value={userProfile.lastName} onChange={(e) => handleInputChange(e, setUserProfile)} />
                <InputTextField label="Email" name="email" value={userProfile.email} onChange={(e) => handleInputChange(e, setUserProfile)} />
                <InputTextField label="Confirm Password" name="password" type="password" value={userProfile.password} onChange={(e) => handleInputChange(e, setUserProfile)} />
                <MainButton type="button" text="Update Profile" onClick={handleProfileUpdate} />
                {profileError && <div className="error-message">{profileError}</div>}
                <InputTextField label="Old Password" name="oldPassword" type="password" value={passwords.oldPassword} onChange={(e) => handleInputChange(e, setPasswords)} />
                <InputTextField label="New Password" name="newPassword" type="password" value={passwords.newPassword} onChange={(e) => handleInputChange(e, setPasswords)} />
                <MainButton type="button" text="Change Password" onClick={handleChangePassword} />
                {passwordError && <div className="error-message">{passwordError}</div>}
            </CenteredPanel>
            <LogoutButton />
        </>
    );
};

export default UserSettings;
