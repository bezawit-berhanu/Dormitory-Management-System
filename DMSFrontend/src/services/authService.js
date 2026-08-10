import api from "../constants/api" // Import our configured axios client

const authService = {

async login(credentials) {
// Send the email and password to our .NET login endpoint.
    const response = await api.post("/auth/login", credentials);

    //Different backends may call the JWT token or accestokern.
    const token = data.token || data.accessToken;

    //If the back gave us a token save it in the user browser.
    if(token) {
        localStorage.setItem("token", token); 
    }

    //If the backend returned userInformation save that too. 
if(data.user) {
    localStorage.setItem("user", JSON.stringify(data.user)); }

    else {
        //Fallback in case the backend retunr user directly.
        localStorage.setItem("user", JSON.stringift(data));
    }
//Give the login result back to whoever called thr login.
    return data; 
},

  async forgotPassword(email) {
// Sned the email to the backend.
    const response = await api.post("/auth/forgot-password", {email});
    return response.data;
},


logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("user");
}, 


getToken() {
    //Return the stored JWT
    return localStorage.getItem("token");
},


getCurrentUser() {
    const user = localStorage.getItem("user")

    if(!user) {
        return null;
    }
  //localstroage stores things as a text.
  //JSON.parse converts the text back into a javascript object.
    try {
        return JSON.parse(user);
    } catch {
        return null;
    }
},

isAuthenticated() {
//If there is a token, we consider the user logged in.
    return !!localStorage.getItem("token");
},
};

export default authService;