// React Router tools.
// BrowserRouter is NOT needed here because App.jsx
// already provides it.
import {
  Routes,
  Route,
  Navigate
} from "react-router-dom";

// Authentication pages
import Login from "../pages/Authentication/Login";
import ForgotPassword from "../pages/Authentication/ForgotPassword";
import Profile from "../pages/Authentication/Profile";

// Protects pages that require login
import ProtectedRoute from "./ProtectedRoute";

function AppRoutes() {
  return (
    <Routes>

      {/* =====================================
          DEFAULT PAGE
          =====================================
          When we visit:
              http://localhost:5173/

          send the user to /login.
      */}
      <Route
        path="/"
        element={<Navigate to="/login" replace />}
      />

      {/* =====================================
          LOGIN
          ===================================== */}
      <Route
        path="/login"
        element={<Login />}
      />

      {/* =====================================
          FORGOT PASSWORD
          ===================================== */}
      <Route
        path="/forgot-password"
        element={<ForgotPassword />}
      />

      {/* =====================================
          PROTECTED PAGES
          =====================================
          Anything inside this route requires
          the user to be logged in.
      */}
      <Route element={<ProtectedRoute />}>

        <Route
          path="/profile"
          element={<Profile />}
        />

      </Route>

      {/* =====================================
          UNKNOWN URL
          ===================================== */}
      <Route
        path="*"
        element={<h1>404 - Page Not Found</h1>}
      />

    </Routes>
  );
}

export default AppRoutes;