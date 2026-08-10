import api from "../constants/api";

const userManagementService = {

    //Get all users
    //used by Users.jsx to display all registered users.
    async getUsers() {
        const response = await api.get("/users");
// Return only the actual data.
        return response.data;
    },
//Get one user at a time using id.
    async getUser(id) {
        const repsonse = await api.get(`/users/${id}`);
        return response.data;
    },
//Create user: data contains the information needed to create a user.
    async createUser(data) {
        const response = await api.post("/users", data);

        return response.data;
    },

    //Update user
    async updateUser(id, data) {
const response = await api.put(`/users/${id}`, data);

return response.data;
    },

    async deleteUser(id) {
        const response= await api.delete(`/users/${id}`);

        return response.data;
    },

    //Get roles: used by roles.jsx and potentially user forms.

    async getRoles() {
        const response = await api.get("/roles");
        return response.data;
    },
    //Create roles
    async createRole(data) {
        const response = await api.post("/roles", data);

        return response.data;
    },

    async updateRole(id, data) {
        const response = await api.put("/roles/${id}, data");

        return response.data;
    },

    //Delete role
    async deleteRole(id) {
        const response = await api.delete(`/roles/${id}`);

        return response.data;
    },

};

export default userManagementService;