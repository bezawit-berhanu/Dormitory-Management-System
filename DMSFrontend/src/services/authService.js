import api from "../constants/api";

const authService = {

    // ==========================================
    // LOGIN
    // ==========================================

    async login(credentials) {

        const response = await api.post(
            "/authentication/login",
            credentials
        );

        const data = response.data;

        // Backend generates JWT
        const token =
            data.token ||
            data.accessToken;

        if (token) {
            localStorage.setItem("token", token);
        }

        // Store logged-in user
        const user =
            data.user ||
            data;

        localStorage.setItem(
            "user",
            JSON.stringify(user)
        );

        return data;
    },


    // ==========================================
    // REGISTER
    // ==========================================

    async register(accountData) {

        const response = await api.post(
            "/authentication/register",
            accountData
        );

        return response.data;
    },


    // ==========================================
    // FORGOT PASSWORD
    // ==========================================

    async forgotPassword(email) {

        const response = await api.post(
            "/authentication/forgot-password",
            { email }
        );

        return response.data;
    },


    // ==========================================
    // LOGOUT
    // ==========================================

    logout() {

        localStorage.removeItem("token");
        localStorage.removeItem("user");
    },


    // ==========================================
    // GET TOKEN
    // ==========================================

    getToken() {

        return localStorage.getItem("token");
    },


    // ==========================================
    // GET CURRENT USER
    // ==========================================

    getCurrentUser() {

        const user =
            localStorage.getItem("user");

        if (!user) {
            return null;
        }

        try {
            return JSON.parse(user);
        } catch {
            return null;
        }
    },


    // ==========================================
    // CHECK AUTHENTICATION
    // ==========================================

    isAuthenticated() {

        return !!localStorage.getItem("token");
    }

};

export default authService;