import {
    createContext,
    useContext,
    useEffect,
    useState
} from "react";

import authService
    from "../services/authService";

const AuthContext =
    createContext(null);


export const AuthProvider = ({ children }) => {

    const [user, setUser] =
        useState(
            authService.getCurrentUser()
        );

    const [loading, setLoading] =
        useState(false);


    const login = async (credentials) => {

        const data =
            await authService.login(
                credentials
            );

        const loggedInUser =
            data?.user ||
            authService.getCurrentUser();

        setUser(loggedInUser);

        return data;
    };


    const logout = () => {

        authService.logout();

        setUser(null);
    };


    const isAuthenticated =
        !!user &&
        authService.isAuthenticated();


    return (
        <AuthContext.Provider
            value={{
                user,
                loading,
                isAuthenticated,
                login,
                logout
            }}
        >
            {children}
        </AuthContext.Provider>
    );
};


export const useAuth = () => {

    const context =
        useContext(AuthContext);

    if (!context) {
        throw new Error(
            "useAuth must be used inside AuthProvider"
        );
    }

    return context;
};


export default AuthContext;