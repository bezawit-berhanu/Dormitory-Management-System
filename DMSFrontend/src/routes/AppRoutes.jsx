import {
    Routes,
    Route,
    Navigate
} from "react-router-dom";


import Login
    from "../pages/Authentication/Login";

import ForgotPassword
    from "../pages/Authentication/ForgotPassword";

import Profile
    from "../pages/Authentication/Profile";

import CreateAccount
    from "../pages/Authentication/CreateAccount";

import ProtectedRoute
    from "./ProtectedRoute";

import Users
    from "../pages/admin/Users";

import StudentDashboard
    from "../pages/Student/StudentDashboard";


import CheckInStudent
    from "../pages/CheckIn/CheckInStudent";

import CheckInHistory
    from "../pages/CheckIn/CheckInHistory";

import CheckOutStudent
    from "../pages/CheckOut/CheckOutStudent";

import CheckOutHistory
    from "../pages/CheckOut/CheckOutHistory";


function AppRoutes() {

    return (

        <Routes>

            {/* =====================================
                PUBLIC
            ===================================== */}

            <Route
                path="/"
                element={
                    <Navigate
                        to="/login"
                        replace
                    />
                }
            />


            <Route
                path="/login"
                element={<Login />}
            />


            <Route
                path="/create-account"
                element={<CreateAccount />}
            />


            <Route
                path="/forgot-password"
                element={<ForgotPassword />}
            />


            {/* =====================================
                PROTECTED
            ===================================== */}

            <Route
                element={<ProtectedRoute />}
            >

                <Route
                    path="/student/dashboard"
                    element={
                        <StudentDashboard />
                    }
                />


                <Route
                    path="/profile"
                    element={<Profile />}
                />


                <Route
                    path="/admin/users"
                    element={<Users />}
                />


                <Route
                    path="/check-in"
                    element={<CheckInStudent />}
                />


                <Route
                    path="/check-in/history"
                    element={<CheckInHistory />}
                />


                <Route
                    path="/check-out"
                    element={<CheckOutStudent />}
                />


                <Route
                    path="/check-out/history"
                    element={<CheckOutHistory />}
                />

            </Route>


            {/* =====================================
                404
            ===================================== */}

            <Route
                path="*"
                element={
                    <h1>
                        404 - Page Not Found
                    </h1>
                }
            />

        </Routes>
    );
}


export default AppRoutes;