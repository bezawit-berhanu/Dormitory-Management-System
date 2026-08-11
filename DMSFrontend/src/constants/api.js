import axios from "axios";

const api = axios.create({
    baseURL:"http://localhost:5000/api",  
    headers: {
        "Content-Type": "application.json" //Tells the backend that we are using json.
    },
});


/* This runs before the api request is sent. Since after login 
the backend gives us JWT token. We need to attach the token to future requests.*/

api.interceptors.request.use((config) => {
    const token = localStorage.getItem("token"); //Get the JWT token from the browser storage.
//If token exists, 
if(token) {
    config.headers.Authorization = `Bearer ${token}`; //Attach the token to 
    // request so the backend will recognize the loggedin user.

}
return config; //Send the request onward.
}, 


//If something goes wrong, reject the request.
(error) => 
    {return Promise.reject(error)} 
);




/*This runs when the backend repsondes*/
api.interceptors.response.use(
    (response) => {return response; },

    (error) => {
        if (error.response?.status === 401) {
            localStorage.removeItem("token"); //remove the old token.
            localStorage.removeItem("user"); //reomve stored user information.
        }
        return Promise.reject(error);
    }
);

export default api; //Make our axios client available to the rest application.