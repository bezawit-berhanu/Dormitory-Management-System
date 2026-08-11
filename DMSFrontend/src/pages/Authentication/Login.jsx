// useState lets us remember form values,
// loading state, and errors.
import { useState } from "react";

// React Router tools.
import {
  useLocation,
  useNavigate
} from "react-router-dom";

// Our custom authentication hook.
import { useAuth } from "../../context/AuthContext";


const Login = () => {


  // navigate() lets us change the URL
  // from JavaScript.
  const navigate = useNavigate();


  // location tells us information
  // about the current URL.
  const location = useLocation();


  // Get our login function from AuthContext.
  const { login } = useAuth();


  // ==========================================
  // FORM STATE
  // ==========================================

  // React remembers what the user types.
  const [formData, setFormData] = useState({

    email: "",

    password: "",
  });


  // Error message state.
  const [error, setError] =
    useState("");


  // Used to disable the button
  // while login is happening.
  const [loading, setLoading] =
    useState(false);


  // ==========================================
  // WHEN USER TYPES
  // ==========================================

  const handleChange = (e) => {

    // Get the input's name and value.
    const {
      name,
      value
    } = e.target;


    // Update formData.
    setFormData((previous) => ({

      // Keep the existing fields.
      ...previous,

      // Update only the field
      // the user changed.
      [name]: value,
    }));
  };


  // ==========================================
  // WHEN USER SUBMITS FORM
  // ==========================================

  const handleSubmit = async (e) => {

    // Stop normal browser form submission.
    e.preventDefault();


    // Clear previous error.
    setError("");


    // Basic frontend validation.
    if (
      !formData.email ||
      !formData.password
    ) {

      setError(
        "Email and password are required."
      );

      return;
    }


    try {

      // Show loading state.
      setLoading(true);


      // Actually call our authentication system.
      const data =
        await login(formData);


      // Get the logged-in user.
      const user =
        data?.user ||
        data?.data?.user ||
        JSON.parse(
          localStorage.getItem("user")
          || "null"
        );


      // Determine their role.
      const role =
        user?.roleName ||
        user?.role;


      // ========================================
      // REDIRECT AFTER LOGIN
      // ========================================

      // If the user was originally trying
      // to visit a protected page,
      // send them there.
      const redirectPath =
        location.state?.from?.pathname;


      if (
        redirectPath &&
        redirectPath !== "/login"
      ) {

        navigate(redirectPath);

        return;
      }


      // Otherwise redirect based on role.
      if (
        role === "Admin" ||
        role === "Administrator"
      ) {

        navigate("/admin/users");

      } else if (role === "Student") {

        navigate("/student/dashboard");

      } else {

        navigate("/profile");
      }


    } catch (err) {

      // Print the technical error
      // in the browser console.
      console.error(err);


      // Show a friendly message to the user.
      setError(
        err.response?.data?.message ||
        "Invalid email or password."
      );


    } finally {

      // Stop loading whether login
      // succeeded or failed.
      setLoading(false);
    }
  };


  // ==========================================
  // WHAT THE USER SEES
  // ==========================================

  return (

    <div className="auth-page">

      <div className="auth-card">

        <h1>Login</h1>

        <p>
          Sign in to your dormitory account.
        </p>


        {/* Show error ONLY if there is one. */}
        {error && (
          <div className="error-message">
            {error}
          </div>
        )}


        <form onSubmit={handleSubmit}>

          <div className="form-group">

            <label htmlFor="email">
              Email
            </label>

            <input
              id="email"

              // IMPORTANT:
              // This "name" must match
              // our formData property.
              name="email"

              type="email"

              // Show React's current email value.
              value={formData.email}

              // Update React when user types.
              onChange={handleChange}

              placeholder="Enter your email"
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

            // Don't allow another login
            // while one is already happening.
            disabled={loading}
          >

            {/* Change button text while loading. */}
            {loading
              ? "Signing in..."
              : "Login"}

          </button>

        </form>


        {/* Go to forgot-password page. */}
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