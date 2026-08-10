// ==========================================
// REACT ROUTER
// ==========================================
//
// Routes = container for all our routes.
//
// Route = defines one URL and the page
//         that should appear at that URL.
//
// Navigate = redirects the user to another
//            URL.
// ==========================================

import {
  Routes,
  Route,
  Navigate
} from "react-router-dom";


// ==========================================
// AUTHENTICATION PAGES
// ==========================================

import Login
  from "../pages/Authentication/Login";

import ForgotPassword
  from "../pages/Authentication/ForgotPassword";

import Profile
  from "../pages/Authentication/Profile";


// ==========================================
// PROTECTED ROUTE
// ==========================================
//
// This component checks whether the user
// is authenticated before allowing access
// to protected pages.
// ==========================================

import ProtectedRoute
  from "../routes/ProtectedRoute";


// ==========================================
// APP ROUTES
// ==========================================

function AppRoutes() {

  return (

    // IMPORTANT:
    // Every <Route> must be inside <Routes>.
    <Routes>


      {/* =====================================
          LOGIN
          =====================================

          URL:
          http://localhost:5173/login

          Anyone can access Login.
      */}

      <Route
        path="/login"
        element={<Login />}
      />


      {/* =====================================
          FORGOT PASSWORD
          =====================================

          URL:
          /forgot-password
      */}

      <Route
        path="/forgot-password"
        element={<ForgotPassword />}
      />


      {/* =====================================
          PROFILE
          =====================================

          URL:
          /profile

          This page requires authentication.

          ProtectedRoute decides whether the
          user is allowed to see Profile.
      */}

      <Route
        path="/profile"
        element={
          <ProtectedRoute>
            <Profile />
          </ProtectedRoute>
        }
      />


      {/* =====================================
          DEFAULT PAGE
          =====================================

          If someone visits:

          http://localhost:5173/

          redirect them to:

          /login
      */}

      <Route
        path="/"
        element={
          <Navigate
            to="/login"
            replace
          />
        }
      />


      {/* =====================================
          UNKNOWN URL
          =====================================

          If somebody types:

          /something-random

          redirect them to Login for now.
      */}

      <Route
        path="*"
        element={
          <Navigate
            to="/login"
            replace
          />
        }
      />

    </Routes>
  );
}


export default AppRoutes;