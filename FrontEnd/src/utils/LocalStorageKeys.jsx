export const LOCAL_STORAGE_KEYS = {
    USERNAME: 'username',
    TOKEN: 'token',
    FIRST_NAME: 'firstName',
    LAST_NAME: 'lastName',
    ROLE: 'role',
    COMPANY_STATUS: 'companyStatus',
    COMPANY_ID: 'companyId',
    USER_ID: 'userId'
};

export const clearLocalStorage = () => {
    Object.values(LOCAL_STORAGE_KEYS).forEach(key => localStorage.removeItem(key));
};