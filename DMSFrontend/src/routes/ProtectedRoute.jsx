// ==========================================
// PROTECTED ROUTE
// ==========================================
//
// This component protects pages that should
// only be available to logged-in users.
//
// Example:
//
// Profile
// Student Dashboard
// Admin pages
//
// If the user is NOT logged in:
//
// ProtectedRoute
//      ↓
// redirect to /login
//
// If the user IS logged in:
//
// ProtectedRoute
//      ↓
// show the requested page
// ==========================================


import {
  Navigate,
  useLocation
} from "react-router-dom";


import {
  useAuth
} from "../context/AuthContext";


const ProtectedRoute = ({ children }) => {


  // ========================================
  // AUTHENTICATION INFORMATION
  // ========================================

  const {
    loading,
    isAuthenticated
  } = useAuth();


  // ========================================
  // CURRENT URL
  // ========================================
  //
  // We use this so that if someone tries
  // to access /profile while logged out,
  // we can remember that they wanted
  // /profile.
  //
  const location =
    useLocation();


  // ========================================
  // AUTHENTICATION IS STILL LOADING
  // ========================================

  if (loading) {

    return (
      <div>
        Loading...
      </div>
    );
  }


  // ========================================
  // USER IS NOT LOGGED IN
  // ========================================

  if (!isAuthenticated) {

    return (

      <Navigate
        to="/login"

        // Remember where the user wanted
        // to go.
        state={{
          from: location
        }}

        // Replace the current browser history
        // entry instead of creating another one.
        replace
      />

    );
  }


  // ========================================
  // USER IS AUTHENTICATED
  // ========================================
  //
  // Display whatever component was placed
  // inside ProtectedRoute.
  // ========================================

  return children;
};


export default ProtectedRoute;