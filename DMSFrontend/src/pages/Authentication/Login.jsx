import { useState } from "react";
import {
    useLocation,
    useNavigate
} from "react-router-dom";

import { useAuth } from "../../context/AuthContext";

const Login = () => {

    const navigate = useNavigate();
    const location = useLocation();

    const { login } = useAuth();

    const [formData, setFormData] = useState({
        identifier: "",
        password: ""
    });

    const [error, setError] = useState("");
    const [loading, setLoading] = useState(false);


    const handleChange = (e) => {

        const {
            name,
            value
        } = e.target;

        setFormData((previous) => ({
            ...previous,
            [name]: value
        }));
    };


    const handleSubmit = async (e) => {

        e.preventDefault();

        setError("");

        if (
            !formData.identifier ||
            !formData.password
        ) {
            setError(
                "Student ID/email and password are required."
            );

            return;
        }


        try {

            setLoading(true);

            const data =
                await login(formData);


            const user =
                data?.user ||
                data?.data?.user ||
                JSON.parse(
                    localStorage.getItem("user") || "null"
                );


            const role =
                user?.roleName ||
                user?.role;


            const redirectPath =
                location.state?.from?.pathname;


            if (
                redirectPath &&
                redirectPath !== "/login"
            ) {

                navigate(redirectPath);

                return;
            }


            // =====================================
            // STUDENT
            // =====================================

            if (role === "Student") {

                navigate("/student/dashboard");

                return;
            }


            // =====================================
            // ADMIN
            // =====================================

            if (
                role === "Admin" ||
                role === "Administrator"
            ) {

                navigate("/admin/users");

                return;
            }


            // =====================================
            // STAFF / OTHER USERS
            // =====================================

            navigate("/profile");

        } catch (err) {

            console.error(err);

            setError(
                err.response?.data?.message ||
                err.response?.data ||
                "Invalid Student ID/email or password."
            );

        } finally {

            setLoading(false);
        }
    };


    return (

        <div className="auth-page">

            <div className="auth-card">

                <h1>Login</h1>

                <p>
                    Sign in to your dormitory account.
                </p>


                {error && (
                    <div className="error-message">
                        {error}
                    </div>
                )}


                <form onSubmit={handleSubmit}>

                    <div className="form-group">

                        <label htmlFor="identifier">
                            Student ID / Email
                        </label>

                        <input
                            id="identifier"
                            name="identifier"
                            type="text"
                            value={formData.identifier}
                            onChange={handleChange}
                            placeholder="Enter Student ID or email"
                        />

                    </div>


                    <div className="form-group">

                        <label htmlFor="password">
                            Password
                        </label>

                        <input
                            id="password"
                            name="password"
                            type="password"
                            value={formData.password}
                            onChange={handleChange}
                            placeholder="Enter your password"
                        />

                    </div>


                    <button
                        type="submit"
                        disabled={loading}
                    >
                        {loading
                            ? "Signing in..."
                            : "Login"}
                    </button>

                </form>


                <button
                    type="button"
                    onClick={() =>
                        navigate("/create-account")
                    }
                >
                    Create Account
                </button>


                <button
                    type="button"
                    onClick={() =>
                        navigate("/forgot-password")
                    }
                >
                    Forgot Password?
                </button>

            </div>

        </div>
    );
};

export default Login;