// ==========================================
// TEMPORARY MOCK AUTHENTICATION SERVICE
// ==========================================
//
// We don't have our database/API ready yet.
//
// So for frontend testing, this file pretends
// that we have a working authentication API.
//
// LATER:
// We will replace this with the real API call.
// ==========================================


// ==========================================
// FAKE TEST USERS
// ==========================================
//
// These users are temporary.
//
// They exist ONLY so that we can test:
// Login
// AuthContext
// ProtectedRoute
// Dashboard
//
// They are NOT stored in the database.
//
const fakeUsers = [
  {
    id: 1,
    fullName: "System Administrator",
    email: "admin@dormitory.com",
    password: "Admin123",
    role: "Admin"
  },

  {
    id: 2,
    fullName: "Test Student",
    email: "student@dormitory.com",
    password: "Student123",
    role: "Student"
  }
];


// ==========================================
// AUTHENTICATION SERVICE
// ==========================================

const authService = {

  // ========================================
  // LOGIN
  // ========================================

  async login(credentials) {

    // Find a fake user whose email and
    // password match what the user entered.
    const user = fakeUsers.find(
      (fakeUser) =>
        fakeUser.email === credentials.email &&
        fakeUser.password === credentials.password
    );


    // No matching user?
    if (!user) {

      // This behaves like a failed API request.
      throw {
        response: {
          data: {
            message: "Invalid email or password."
          }
        }
      };
    }


    // ======================================
    // CREATE FAKE TOKEN
    // ======================================
    //
    // In the real system this will be a JWT
    // returned by ASP.NET.
    //
    // For now, this simple string is enough
    // to prove that the user is authenticated.
    //
    const token = "fake-jwt-token";


    // Don't store the password!
    const userWithoutPassword = {
      id: user.id,
      fullName: user.fullName,
      email: user.email,
      role: user.role
    };


    // Save the fake token.
    localStorage.setItem(
      "token",
      token
    );


    // Save the logged-in user.
    localStorage.setItem(
      "user",
      JSON.stringify(userWithoutPassword)
    );


    // Return data in a shape similar to
    // what our real API will eventually return.
    return {
      token,
      user: userWithoutPassword
    };
  },


  // ========================================
  // GET CURRENT USER
  // ========================================

  getCurrentUser() {

    // Get the saved user from browser storage.
    const user =
      localStorage.getItem("user");


    // Nothing saved?
    if (!user) {
      return null;
    }


    // Convert JSON text back into
    // a JavaScript object.
    return JSON.parse(user);
  },


  // ========================================
  // CHECK AUTHENTICATION
  // ========================================

  isAuthenticated() {

    // If a token exists, we consider
    // the user logged in.
    return !!localStorage.getItem("token");
  },


  // ========================================
  // LOGOUT
  // ========================================

  logout() {

    // Remove authentication information.
    localStorage.removeItem("token");

    localStorage.removeItem("user");
  }

};


export default authService;