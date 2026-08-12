

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
    useState(true);

  useEffect(() => {


    setLoading(false);

  }, []);


  const login = async (credentials) => {

    const data =
      await authService.login(
        credentials
      );


  
    const loggedInUser =
      data?.user ||
      data?.data?.user ||
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




  const value = {

    user,

    loading,

    isAuthenticated,

    login,

    logout

  };




  return (

    <AuthContext.Provider value={value}>

      {children}

    </AuthContext.Provider>
  );
};



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