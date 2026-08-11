// ==========================================
// AUTHENTICATION CONTEXT
// ==========================================
//
// This file stores authentication information
// that needs to be available throughout our
// React application.
//
// For example:
//
// Login.jsx
// StudentDashboard.jsx
// Profile.jsx
// Admin pages
//
// can all access the current user through:
//
// useAuth()
// ==========================================


import {
  createContext,
  useContext,
  useEffect,
  useState
} from "react";


import authService
  from "../services/authService";


// ==========================================
// CREATE CONTEXT
// ==========================================
//
// Think of this as creating a shared
// authentication "box".
//
// The box will contain things like:
//
// user
// loading
// isAuthenticated
// login()
// logout()
// ==========================================

const AuthContext =
  createContext(null);


// ==========================================
// AUTH PROVIDER
// ==========================================
//
// AuthProvider wraps our application in App.jsx.
//
// Everything inside:
//
// <AuthProvider>
//     ...
// </AuthProvider>
//
// can use authentication information.
// ==========================================

export const AuthProvider = ({ children }) => {


  // ========================================
  // CURRENT USER
  // ========================================
  //
  // When the browser refreshes, we don't want
  // React to immediately forget the user.
  //
  // authService.getCurrentUser()
  // retrieves the user stored in localStorage.
  //
  const [user, setUser] =
    useState(
      authService.getCurrentUser()
    );


  // ========================================
  // LOADING
  // ========================================
  //
  // While authentication information is
  // being initialized, loading is true.
  //
  const [loading, setLoading] =
    useState(true);


  // ========================================
  // INITIALIZE AUTHENTICATION
  // ========================================
  //
  // useEffect runs after this provider
  // appears on the screen.
  //
  useEffect(() => {

    // Authentication information has
    // finished loading.
    setLoading(false);

  }, []);


  // ========================================
  // LOGIN
  // ========================================
  //
  // Login.jsx calls this function.
  //
  // Login.jsx
  //     ↓
  // useAuth()
  //     ↓
  // login(credentials)
  //     ↓
  // authService.login()
  //     ↓
  // backend
  // ========================================

  const login = async (credentials) => {

    // Ask authService to communicate
    // with the backend.
    const data =
      await authService.login(
        credentials
      );


    // Get the user returned by the backend.
    //
    // Different backend response structures
    // are handled for now.
    const loggedInUser =
      data?.user ||
      data?.data?.user ||
      authService.getCurrentUser();


    // Tell React that this is now
    // the logged-in user.
    setUser(loggedInUser);


    // Give Login.jsx the backend response.
    return data;
  };


  // ========================================
  // LOGOUT
  // ========================================

  const logout = () => {

    // Remove token and stored user.
    authService.logout();


    // Tell React nobody is logged in.
    setUser(null);
  };


  // ========================================
  // AUTHENTICATION STATUS
  // ========================================
  //
  // !!user converts the user value into
  // true or false.
  //
  // User exists → true
  // User doesn't exist → false
  //
  // We ALSO check authService.isAuthenticated()
  // to make sure a token exists.
  // ========================================

  const isAuthenticated =
    !!user &&
    authService.isAuthenticated();


  // ========================================
  // VALUE SHARED WITH THE APPLICATION
  // ========================================

  const value = {

    user,

    loading,

    isAuthenticated,

    login,

    logout

  };


  // ========================================
  // PROVIDER
  // ========================================
  //
  // Everything inside this provider gets
  // access to the "value" above.
  // ========================================

  return (

    <AuthContext.Provider value={value}>

      {children}

    </AuthContext.Provider>
  );
};


// ==========================================
// useAuth CUSTOM HOOK
// ==========================================
//
// Instead of every component doing:
//
// useContext(AuthContext)
//
// they can simply do:
//
// const { user } = useAuth();
//
// Much cleaner.
// ==========================================

export const useAuth = () => {

  const context =
    useContext(AuthContext);


  // If someone tries to use useAuth()
  // outside AuthProvider, show a useful error.
  if (!context) {

    throw new Error(
      "useAuth must be used inside AuthProvider"
    );
  }


  return context;
};


// Export the context itself as well.
export default AuthContext;