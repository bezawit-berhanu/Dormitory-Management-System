useState allows React to remember information
that can change while the page is running.
import { useState } from "react";

useNavigate allows us to move the user
to another page using JavaScript.
import { useNavigate } from "react-router-dom";

useAuth gives us access to the currently
logged-in user and the logout function.
import { useAuth } from "../../context/AuthContext";


const Profile = () => {

  Get information from our AuthContext.
  
  user = currently logged-in user
  logout = function that logs the user out
  const {
    user,
    logout
  } = useAuth();


  Used to navigate to another page.
  const navigate = useNavigate();


  ==========================================
  LOGOUT
  ==========================================

  const handleLogout = () => {

    Remove the user's authentication information.
    logout();

    Send the user back to login.
    navigate("/login");
  };


  ==========================================
  PROFILE PAGE
  ==========================================

  return (

    <div className="profile-page">

      <h1>My Profile</h1>


      {/* 
        If we have user information,
        display it.
      */}

      {user ? (

        <div className="profile-card">

          <h2>
            {user.fullName || user.name || "User"}
          </h2>


          <p>
            <strong>Email:</strong>{" "}
            {user.email || "N/A"}
          </p>


          <p>
            <strong>Phone:</strong>{" "}
            {user.phoneNumber ||
              user.phone ||
              "N/A"}
          </p>


          <p>
            <strong>Role:</strong>{" "}
            {user.roleName ||
              user.role ||
              "N/A"}
          </p>


          {/* Logout button */}
          <button
            onClick={handleLogout}
          >
            Logout
          </button>

        </div>

      ) : (

        This should normally not happen
        because Profile is protected.
        <p>
          No user information found.
        </p>

      )}

    </div>
  );
};


export default Profile;