import api from "../constants/api";

const studentService = {
    async getAllStudents() {
const response = await api.get("/student");

return response.data;
    }, 

     async getStudentById(id) {
        const response = await api.get(`/student/${id}`);

        return response.data;
    },

    async createStudent(studentData) {
        const response = await api.post("/student", studentData);

        return response.data;
    },

    async updateStudent(id, studentData) {
        const response = await api.put(`/student/${id}`, studentData);

        return response.data;

    },

    async deleteStudent(id) {
        const response = await api.delete(`/student/${id}`);

        return response.data;
    }

};

export default studentService;